﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SA_Tools;
using SonicRetro.SAModel;

namespace DLLModGenerator
{
	public partial class MainForm : Form
	{
		private Properties.Settings Settings;

		public MainForm()
		{
			InitializeComponent();
		}

		static readonly Dictionary<string, string> typemap = new Dictionary<string, string>() {
			{ "landtable", "LandTable *" },
			{ "landtablearray", "LandTable **" },
			{ "model", "NJS_OBJECT *" },
			{ "modelarray", "NJS_OBJECT **" },
			{ "basicmodel", "NJS_OBJECT *" },
			{ "basicmodelarray", "NJS_OBJECT **" },
			{ "basicdxmodel", "NJS_OBJECT *" },
			{ "basicdxmodelarray", "NJS_OBJECT **" },
			{ "chunkmodel", "NJS_OBJECT *" },
			{ "chunkmodelarray", "NJS_OBJECT **" },
			{ "actionarray", "NJS_ACTION **" }
		};

		DllIniData IniData;

		private void MainForm_Load(object sender, EventArgs e)
		{
			Settings = Properties.Settings.Default;
			if (Settings.MRUList == null)
				Settings.MRUList = new StringCollection();
			StringCollection mru = new StringCollection();
			foreach (string item in Settings.MRUList)
				if (File.Exists(item))
				{
					mru.Add(item);
					recentProjectsToolStripMenuItem.DropDownItems.Add(item.Replace("&", "&&"));
				}
			Settings.MRUList = mru;
			if (Program.Arguments.Length > 0)
				LoadINI(Program.Arguments[0]);
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog a = new OpenFileDialog()
			{
				DefaultExt = "ini",
				Filter = "INI Files|*.ini|All Files|*.*"
			})
				if (a.ShowDialog(this) == DialogResult.OK)
					LoadINI(a.FileName);
		}

		private void recentProjectsToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			fileToolStripMenuItem.DropDown.Close();
			LoadINI(Settings.MRUList[recentProjectsToolStripMenuItem.DropDownItems.IndexOf(e.ClickedItem)]);
		}

		private void LoadINI(string filename)
		{
			IniData = IniSerializer.Deserialize<DllIniData>(filename);
			if (Settings.MRUList.Contains(filename))
			{
				recentProjectsToolStripMenuItem.DropDownItems.RemoveAt(Settings.MRUList.IndexOf(filename));
				Settings.MRUList.Remove(filename);
			}
			Settings.MRUList.Insert(0, filename);
			recentProjectsToolStripMenuItem.DropDownItems.Insert(0, new ToolStripMenuItem(filename));
			Environment.CurrentDirectory = Path.GetDirectoryName(filename);
			listView1.BeginUpdate();
			listView1.Items.Clear();
			foreach (KeyValuePair<string, FileTypeHash> item in IniData.Files)
			{
				bool modified = HelperFunctions.FileHash(item.Key) != item.Value.Hash;
				listView1.Items.Add(new ListViewItem(new[] { item.Key, modified ? "Yes" : "No" }) { Checked = modified });
			}
			listView1.EndUpdate();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Settings.Save();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			listView1.BeginUpdate();
			foreach (ListViewItem item in listView1.Items)
				item.Checked = true;
			listView1.EndUpdate();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			listView1.BeginUpdate();
			foreach (ListViewItem item in listView1.Items)
				item.Checked = item.SubItems[2].Text != "No";
			listView1.EndUpdate();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			listView1.BeginUpdate();
			foreach (ListViewItem item in listView1.Items)
				item.Checked = false;
			listView1.EndUpdate();
		}

		private List<string> ExportCPP(TextWriter writer, bool SA2)
		{
			ModelFormat modelfmt = SA2 ? ModelFormat.Chunk : ModelFormat.BasicDX;
			LandTableFormat landfmt = SA2 ? LandTableFormat.SA2 : LandTableFormat.SADX;
			writer.WriteLine("// Generated by SA Tools DLL Mod Generator");
			writer.WriteLine();
			if (SA2)
				writer.WriteLine("#include \"SA2ModLoader.h\"");
			else
				writer.WriteLine("#include \"SADXModLoader.h\"");
			writer.WriteLine();
			List<string> labels = new List<string>();
			foreach (KeyValuePair<string, FileTypeHash> item in IniData.Files.Where((a, i) => listView1.CheckedIndices.Contains(i)))
				switch (item.Value.Type)
				{
					case "landtable":
						LandTable tbl = LandTable.LoadFromFile(item.Key);
						writer.WriteLine(tbl.ToStructVariables(landfmt, new List<string>()));
						labels.AddRange(tbl.GetLabels());
						break;
					case "model":
						SonicRetro.SAModel.NJS_OBJECT mdl = new ModelFile(item.Key).Model;
						writer.WriteLine(mdl.ToStructVariables(modelfmt == ModelFormat.BasicDX, new List<string>()));
						labels.AddRange(mdl.GetLabels());
						break;
					case "basicmodel":
					case "chunkmodel":
						mdl = new ModelFile(item.Key).Model;
						writer.WriteLine(mdl.ToStructVariables(false, new List<string>()));
						labels.AddRange(mdl.GetLabels());
						break;
					case "basicdxmodel":
						mdl = new ModelFile(item.Key).Model;
						writer.WriteLine(mdl.ToStructVariables(true, new List<string>()));
						labels.AddRange(mdl.GetLabels());
						break;
					case "animation":
						Animation ani = Animation.Load(item.Key);
						writer.WriteLine(ani.ToStructVariables());
						labels.Add(ani.Name);
						break;
				}
			return labels;
		}

		private void button4_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog fd = new SaveFileDialog() { DefaultExt = "cpp", Filter = "C++ source files|*.cpp", InitialDirectory = Environment.CurrentDirectory, RestoreDirectory = true })
				if (fd.ShowDialog(this) == DialogResult.OK)
					using (TextWriter writer = File.CreateText(fd.FileName))
					{
						bool SA2 = IniData.Game == Game.SA2B;
						List<string> labels = ExportCPP(writer, SA2);
						writer.WriteLine("void __cdecl Init(const char *path, const HelperFunctions &helperFunctions)");
						writer.WriteLine("{");
						writer.WriteLine("\tHMODULE handle = GetModuleHandle(L\"{0}\");", IniData.Name);
						List<string> exports = new List<string>(IniData.Items.Where(item => labels.Contains(item.Label)).Select(item => item.Export));
						foreach (KeyValuePair<string, string> item in IniData.Exports.Where(item => exports.Contains(item.Key)))
							writer.WriteLine("\t{0}{1} = ({0})GetProcAddress(handle, \"{1}\");", typemap[item.Value], item.Key);
						foreach (DllItemInfo item in IniData.Items.Where(item => labels.Contains(item.Label)))
							writer.WriteLine("\t{0} = &{1};", item.ToString(), item.Label);
						writer.WriteLine("}");
						writer.WriteLine();
						writer.WriteLine("extern \"C\" __declspec(dllexport) const ModInfo {0}ModInfo = {{ ModLoaderVer, Init, NULL, 0, NULL, 0, NULL, 0, NULL, 0 }};", SA2 ? "SA2" : "SADX");
					}
		}

		private void button5_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog fd = new SaveFileDialog() { DefaultExt = "cpp", Filter = "C++ source files|*.cpp", InitialDirectory = Environment.CurrentDirectory, RestoreDirectory = true })
				if (fd.ShowDialog(this) == DialogResult.OK)
					using (TextWriter writer = File.CreateText(fd.FileName))
					{
						bool SA2 = IniData.Game == Game.SA2B;
						List<string> labels = ExportCPP(writer, SA2);
						writer.WriteLine("extern \"C\" __declspec(dllexport) void __cdecl Init(const char *path, const HelperFunctions &helperFunctions)");
						writer.WriteLine("{");
						writer.WriteLine("\tHMODULE handle = GetModuleHandle(L\"{0}\");", IniData.Name);
						List<string> exports = new List<string>(IniData.Items.Where(item => labels.Contains(item.Label)).Select(item => item.Export));
						foreach (KeyValuePair<string, string> item in IniData.Exports.Where(item => exports.Contains(item.Key)))
							writer.WriteLine("\t{0}{1} = ({0})GetProcAddress(handle, \"{1}\");", typemap[item.Value], item.Key);
						foreach (DllItemInfo item in IniData.Items.Where(item => labels.Contains(item.Label)))
							writer.WriteLine("\t{0} = &{1};", item.ToString(), item.Label);
						writer.WriteLine("}");
						writer.WriteLine();
						writer.WriteLine("extern \"C\" __declspec(dllexport) const ModInfo {0}ModInfo = {{ ModLoaderVer }};", SA2 ? "SA2" : "SADX");
					}
		}

		private void button6_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog fd = new SaveFileDialog() { DefaultExt = "ini", Filter = "INI files|*.ini", InitialDirectory = Environment.CurrentDirectory, RestoreDirectory = true })
				if (fd.ShowDialog(this) == DialogResult.OK)
				{
					string dstfol = Path.GetDirectoryName(fd.FileName);
					DllIniData output = new DllIniData();
					output.Name = IniData.Name;
					output.Game = IniData.Game;
					output.Exports = IniData.Exports;
					output.Files = new DictionaryContainer<FileTypeHash>();
					List<string> labels = new List<string>();
					foreach (KeyValuePair<string, FileTypeHash> item in IniData.Files.Where((a, i) => listView1.CheckedIndices.Contains(i)))
					{
						Directory.CreateDirectory(Path.GetDirectoryName(Path.Combine(dstfol, item.Key)));
						File.Copy(item.Key, Path.Combine(dstfol, item.Key), true);
						switch (item.Value.Type)
						{
							case "landtable":
								LandTable tbl = LandTable.LoadFromFile(item.Key);
								labels.AddRange(tbl.GetLabels());
								break;
							case "model":
							case "basicmodel":
							case "chunkmodel":
							case "basicdxmodel":
								SonicRetro.SAModel.NJS_OBJECT mdl = new ModelFile(item.Key).Model;
								labels.AddRange(mdl.GetLabels());
								break;
							case "animation":
								Animation ani = Animation.Load(item.Key);
								labels.Add(ani.Name);
								break;
						}
						output.Files.Add(item.Key, new FileTypeHash(item.Value.Type, null));
					}
					output.Items = new List<DllItemInfo>(IniData.Items.Where(a => labels.Contains(a.Label)));
					IniSerializer.Serialize(output, fd.FileName);
				}
		}
	}

	static class Extensions
	{
		internal static List<string> GetLabels(this LandTable land)
		{
			List<string> labels = new List<string>() { land.Name };
			if (land.COLName != null)
			{
				labels.Add(land.COLName);
				foreach (COL col in land.COL)
					if (col.Model != null)
						labels.AddRange(col.Model.GetLabels());
			}
			if (land.AnimName != null)
			{
				labels.Add(land.AnimName);
				foreach (GeoAnimData gan in land.Anim)
				{
					if (gan.Model != null)
						labels.AddRange(gan.Model.GetLabels());
					if (gan.Animation != null)
						labels.Add(gan.Animation.Name);
				}
			}
			return labels;
		}

		internal static List<string> GetLabels(this SonicRetro.SAModel.NJS_OBJECT obj)
		{
			List<string> labels = new List<string>() { obj.Name };
			if (obj.Attach != null)
				labels.AddRange(obj.Attach.GetLabels());
			if (obj.Children != null)
				foreach (SonicRetro.SAModel.NJS_OBJECT o in obj.Children)
					labels.AddRange(o.GetLabels());
			return labels;
		}

		internal static List<string> GetLabels(this Attach att)
		{
			List<string> labels = new List<string>() { att.Name };
			if (att is BasicAttach)
			{
				BasicAttach bas = (BasicAttach)att;
				if (bas.VertexName != null)
					labels.Add(bas.VertexName);
				if (bas.NormalName != null)
					labels.Add(bas.NormalName);
				if (bas.MaterialName != null)
					labels.Add(bas.MaterialName);
				if (bas.MeshName != null)
					labels.Add(bas.MeshName);
			}
			else if (att is ChunkAttach)
			{
				ChunkAttach cnk = (ChunkAttach)att;
				if (cnk.VertexName != null)
					labels.Add(cnk.VertexName);
				if (cnk.PolyName != null)
					labels.Add(cnk.PolyName);
			}
			return labels;
		}
	}
}