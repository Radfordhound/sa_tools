﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SonicRetro.SAModel
{
    public class MeshInfo
    {
        public Material Material { get; private set; }
        public VertexData[] Vertices { get; private set; }

        public MeshInfo(Material material, VertexData[] vertices)
        {
            Material = material;
            Vertices = vertices;
        }
    }

    public struct VertexData
    {
        public Vertex Position;
        public Vertex Normal;
        public Color Color;
        public UV UV;

        public VertexData(Vertex position)
            : this(position, null, null, null)
        { }

        public VertexData(Vertex position, Vertex normal)
            : this(position, normal, null, null)
        { }

        public VertexData(Vertex position, Vertex normal, Color? color, UV uv)
        {
            Position = position;
            Normal = normal ?? Vertex.UpNormal;
            Color = color ?? Color.White;
            UV = uv ?? new UV();
        }
    }
}