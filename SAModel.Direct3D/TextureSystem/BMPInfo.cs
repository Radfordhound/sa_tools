﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

namespace SonicRetro.SAModel.Direct3D.TextureSystem
{
    /// <summary>
    /// Contains name and image information for a bitmap file.
    /// </summary>
    public class BMPInfo
    {
        public string Name { get; set; }
        public Bitmap Image { get; set; }

        public BMPInfo(string name, Bitmap image)
        {
            Name = name;
            Image = image;
        }
    }
}
