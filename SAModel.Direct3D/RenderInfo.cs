﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using SonicRetro.SAModel.Direct3D;

namespace SonicRetro.SAModel.Direct3D
{
    public class RenderInfo
    {
        public Mesh Mesh { get; private set; }
        public int Subset { get; private set; }
        public Matrix Transform { get; private set; }
        public NJS_MATERIAL Material { get; private set; }
        public Texture Texture { get; private set; }
        public FillMode FillMode { get; private set; }
        public BoundingSphere Bounds { get; private set; }

        public RenderInfo(Mesh mesh, int subset, Matrix transform, NJS_MATERIAL material, Texture texture, FillMode fillMode, BoundingSphere bounds)
        {
            Mesh = mesh;
            Subset = subset;
            Transform = transform;
            Material = material;
            Texture = texture;
            FillMode = fillMode;
            Bounds = bounds;
        }

        public void Draw(Device device)
        {
            FillMode mode = device.RenderState.FillMode;
            TextureFilter magfilter = device.SamplerState[0].MagFilter;
            TextureFilter minfilter = device.SamplerState[0].MinFilter;
            TextureFilter mipfilter = device.SamplerState[0].MipFilter;

			Material.SetDeviceStates(device, Texture, Transform, FillMode);

            if (Mesh != null)
                Mesh.DrawSubset(Subset);
            device.RenderState.Ambient = System.Drawing.Color.Black;
            device.RenderState.FillMode = mode;
            device.SamplerState[0].MagFilter = magfilter;
            device.SamplerState[0].MinFilter = minfilter;
            device.SamplerState[0].MipFilter = mipfilter;
        }

        public static void Draw(IEnumerable<RenderInfo> items, Device device, EditorCamera camera)
        {
            List<KeyValuePair<float, RenderInfo>> drawList = new List<KeyValuePair<float, RenderInfo>>();
            foreach (RenderInfo item in items)
            {
                float dist = Extensions.Distance(camera.Position, item.Bounds.Center.ToVector3()) + item.Bounds.Radius;
				if (dist > camera.DrawDistance) continue;

                if (item.Material != null && item.Material.UseAlpha)
                {
                    bool ins = false;
                    for (int i = 0; i < drawList.Count; i++)
                    {
                        if (drawList[i].Key < dist)
                        {
                            drawList.Insert(i, new KeyValuePair<float, RenderInfo>(dist, item));
                            ins = true;
                            break;
                        }
                    }
                    if (!ins)
                        drawList.Add(new KeyValuePair<float, RenderInfo>(dist, item));
                }
                else
                    drawList.Insert(0, new KeyValuePair<float, RenderInfo>(float.MaxValue, item));
            }
            foreach (KeyValuePair<float, RenderInfo> item in drawList)
                item.Value.Draw(device);
        }
    }
}