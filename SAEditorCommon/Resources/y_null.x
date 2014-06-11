xof 0303txt 0032
template ColorRGBA {
 <35ff44e0-6c7c-11cf-8f52-0040333594a3>
 FLOAT red;
 FLOAT green;
 FLOAT blue;
 FLOAT alpha;
}

template ColorRGB {
 <d3e16e81-7835-11cf-8f52-0040333594a3>
 FLOAT red;
 FLOAT green;
 FLOAT blue;
}

template Material {
 <3d82ab4d-62da-11cf-ab39-0020af71e433>
 ColorRGBA faceColor;
 FLOAT power;
 ColorRGB specularColor;
 ColorRGB emissiveColor;
 [...]
}

template Frame {
 <3d82ab46-62da-11cf-ab39-0020af71e433>
 [...]
}

template Matrix4x4 {
 <f6f23f45-7686-11cf-8f52-0040333594a3>
 array FLOAT matrix[16];
}

template FrameTransformMatrix {
 <f6f23f41-7686-11cf-8f52-0040333594a3>
 Matrix4x4 frameMatrix;
}

template Vector {
 <3d82ab5e-62da-11cf-ab39-0020af71e433>
 FLOAT x;
 FLOAT y;
 FLOAT z;
}

template MeshFace {
 <3d82ab5f-62da-11cf-ab39-0020af71e433>
 DWORD nFaceVertexIndices;
 array DWORD faceVertexIndices[nFaceVertexIndices];
}

template Mesh {
 <3d82ab44-62da-11cf-ab39-0020af71e433>
 DWORD nVertices;
 array Vector vertices[nVertices];
 DWORD nFaces;
 array MeshFace faces[nFaces];
 [...]
}

template MeshMaterialList {
 <f6f23f42-7686-11cf-8f52-0040333594a3>
 DWORD nMaterials;
 DWORD nFaceIndexes;
 array DWORD faceIndexes[nFaceIndexes];
 [Material <3d82ab4d-62da-11cf-ab39-0020af71e433>]
}

template VertexElement {
 <f752461c-1e23-48f6-b9f8-8350850f336f>
 DWORD Type;
 DWORD Method;
 DWORD Usage;
 DWORD UsageIndex;
}

template DeclData {
 <bf22e553-292c-4781-9fea-62bd554bdd93>
 DWORD nElements;
 array VertexElement Elements[nElements];
 DWORD nDWords;
 array DWORD data[nDWords];
}


Material y_material {
 0.000000;1.000000;0.000000;1.000000;;
 9.999999;
 0.000000;0.000000;0.000000;;
 0.000000;0.000000;0.000000;;
}

Frame y_null {
 

 FrameTransformMatrix {
  1.000000,0.000000,0.000000,0.000000,0.000000,1.000000,0.000000,0.000000,0.000000,0.000000,1.000000,0.000000,0.000000,0.000000,0.000000,1.000000;;
 }

 Mesh y_null {
  72;
  0.000000;0.000000;-0.050890;,
  -0.017405;0.000000;-0.047821;,
  -0.017405;1.598970;-0.047821;,
  0.000000;1.598970;-0.050890;,
  -0.032711;0.000000;-0.038984;,
  -0.032711;1.598970;-0.038984;,
  -0.044072;0.000000;-0.025445;,
  -0.044072;1.598970;-0.025445;,
  -0.050117;0.000000;-0.008837;,
  -0.050117;1.598970;-0.008837;,
  -0.050117;-0.000000;0.008837;,
  -0.050117;1.598970;0.008837;,
  -0.044072;-0.000000;0.025445;,
  -0.044072;1.598970;0.025445;,
  -0.032711;-0.000000;0.038984;,
  -0.032711;1.598970;0.038984;,
  -0.017405;-0.000000;0.047821;,
  -0.017405;1.598970;0.047821;,
  -0.000000;-0.000000;0.050890;,
  0.000000;1.598970;0.050890;,
  0.017405;-0.000000;0.047821;,
  0.017405;1.598970;0.047821;,
  0.032711;-0.000000;0.038984;,
  0.032711;1.598970;0.038984;,
  0.044072;-0.000000;0.025445;,
  0.044072;1.598970;0.025445;,
  0.050117;-0.000000;0.008837;,
  0.050117;1.598970;0.008837;,
  0.050117;0.000000;-0.008837;,
  0.050117;1.598970;-0.008837;,
  0.044072;0.000000;-0.025445;,
  0.044072;1.598970;-0.025445;,
  0.032712;0.000000;-0.038984;,
  0.032712;1.598970;-0.038984;,
  0.017405;0.000000;-0.047821;,
  0.017405;1.598970;-0.047821;,
  0.032712;0.000000;-0.038984;,
  0.044072;0.000000;-0.025445;,
  0.050117;0.000000;-0.008837;,
  0.050117;-0.000000;0.008837;,
  0.044072;-0.000000;0.025445;,
  0.032711;-0.000000;0.038984;,
  0.017405;-0.000000;0.047821;,
  -0.000000;-0.000000;0.050890;,
  -0.017405;-0.000000;0.047821;,
  -0.032711;-0.000000;0.038984;,
  -0.044072;-0.000000;0.025445;,
  -0.050117;-0.000000;0.008837;,
  -0.050117;0.000000;-0.008837;,
  -0.044072;0.000000;-0.025445;,
  -0.032711;0.000000;-0.038984;,
  -0.017405;0.000000;-0.047821;,
  0.000000;0.000000;-0.050890;,
  0.017405;0.000000;-0.047821;,
  -0.017405;1.598970;-0.047821;,
  -0.032711;1.598970;-0.038984;,
  -0.044072;1.598970;-0.025445;,
  -0.050117;1.598970;-0.008837;,
  -0.050117;1.598970;0.008837;,
  -0.044072;1.598970;0.025445;,
  -0.032711;1.598970;0.038984;,
  -0.017405;1.598970;0.047821;,
  0.000000;1.598970;0.050890;,
  0.017405;1.598970;0.047821;,
  0.032711;1.598970;0.038984;,
  0.044072;1.598970;0.025445;,
  0.050117;1.598970;0.008837;,
  0.050117;1.598970;-0.008837;,
  0.044072;1.598970;-0.025445;,
  0.032712;1.598970;-0.038984;,
  0.017405;1.598970;-0.047821;,
  0.000000;1.598970;-0.050890;;
  68;
  3;0,1,2;,
  3;2,3,0;,
  3;1,4,5;,
  3;5,2,1;,
  3;4,6,7;,
  3;7,5,4;,
  3;6,8,9;,
  3;9,7,6;,
  3;8,10,11;,
  3;11,9,8;,
  3;10,12,13;,
  3;13,11,10;,
  3;12,14,15;,
  3;15,13,12;,
  3;14,16,17;,
  3;17,15,14;,
  3;16,18,19;,
  3;19,17,16;,
  3;18,20,21;,
  3;21,19,18;,
  3;20,22,23;,
  3;23,21,20;,
  3;22,24,25;,
  3;25,23,22;,
  3;24,26,27;,
  3;27,25,24;,
  3;26,28,29;,
  3;29,27,26;,
  3;28,30,31;,
  3;31,29,28;,
  3;30,32,33;,
  3;33,31,30;,
  3;32,34,35;,
  3;35,33,32;,
  3;34,0,3;,
  3;3,35,34;,
  3;36,37,38;,
  3;38,39,40;,
  3;40,41,42;,
  3;38,40,42;,
  3;42,43,44;,
  3;44,45,46;,
  3;42,44,46;,
  3;46,47,48;,
  3;48,49,50;,
  3;46,48,50;,
  3;42,46,50;,
  3;38,42,50;,
  3;50,51,52;,
  3;38,50,52;,
  3;36,38,52;,
  3;53,36,52;,
  3;54,55,56;,
  3;56,57,58;,
  3;58,59,60;,
  3;56,58,60;,
  3;60,61,62;,
  3;62,63,64;,
  3;60,62,64;,
  3;64,65,66;,
  3;66,67,68;,
  3;64,66,68;,
  3;60,64,68;,
  3;56,60,68;,
  3;68,69,70;,
  3;56,68,70;,
  3;54,56,70;,
  3;71,54,70;;

  MeshMaterialList {
   1;
   68;
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0,
   0;
   { y_material }
  }

  DeclData {
   1;
   2;0;3;0;;
   216;
   882921786,
   863365931,
   3212836864,
   3199147331,
   863328906,
   3211825074,
   3199147331,
   863537672,
   3211825074,
   882582864,
   863368271,
   3212836864,
   3206843835,
   861376223,
   3208911741,
   3206843834,
   861583820,
   3208911742,
   3210589143,
   858164803,
   3204448256,
   3210589144,
   857960044,
   3204448255,
   3212581981,
   851763618,
   3190935756,
   3212581981,
   851354098,
   3190935749,
   3212581980,
   826735100,
   1043452129,
   3212581980,
   830569672,
   1043452124,
   3210589143,
   2996205056,
   1056964608,
   3210589143,
   2995713552,
   1056964608,
   3206843837,
   3004777146,
   1061428091,
   3206843837,
   3004569550,
   1061428091,
   3199147333,
   3008736146,
   1064341426,
   3199147336,
   3008529721,
   1064341425,
   2995198761,
   3010849579,
   1065353216,
   0,
   3010851921,
   1065353216,
   1051663677,
   3010812555,
   1064341427,
   1051663678,
   3011021321,
   1064341427,
   1059360183,
   3008859875,
   1061428096,
   1059360184,
   3009067469,
   1061428096,
   1063105492,
   3005648455,
   1056964613,
   1063105492,
   3005443696,
   1056964613,
   1065098331,
   2999247283,
   1043452146,
   1065098331,
   2998837767,
   1043452151,
   1065098334,
   2974218935,
   3190935730,
   1065098334,
   2978053394,
   3190935733,
   1063105500,
   848721392,
   3204448240,
   1063105499,
   848229890,
   3204448243,
   1059360194,
   857293493,
   3208911735,
   1059360194,
   857085897,
   3208911735,
   1051663701,
   861252494,
   3211825071,
   1051663699,
   861046070,
   3211825071,
   0,
   3212836864,
   3007036719,
   0,
   3212836864,
   3007036719,
   0,
   3212836864,
   3007036719,
   0,
   3212836864,
   3007036719,
   0,
   3212836864,
   3007036719,
   0,
   3212836864,
   3007036719,
   0,
   3212836864,
   3007036719,
   0,
   3212836864,
   3007036719,
   0,
   3212836864,
   3007036719,
   0,
   3212836864,
   3007036719,
   0,
   3212836864,
   3007036719,
   0,
   3212836864,
   3007036719,
   0,
   3212836864,
   3007036719,
   0,
   3212836864,
   3007036719,
   0,
   3212836864,
   3007036719,
   0,
   3212836864,
   3007036719,
   0,
   3212836864,
   3007036719,
   0,
   3212836864,
   3007036719,
   0,
   1065353216,
   859553071,
   0,
   1065353216,
   859553071,
   0,
   1065353216,
   859553071,
   0,
   1065353216,
   859553071,
   0,
   1065353216,
   859553071,
   0,
   1065353216,
   859553071,
   0,
   1065353216,
   859553071,
   0,
   1065353216,
   859553071,
   0,
   1065353216,
   859553071,
   0,
   1065353216,
   859553071,
   0,
   1065353216,
   859553071,
   0,
   1065353216,
   859553071,
   0,
   1065353216,
   859553071,
   0,
   1065353216,
   859553071,
   0,
   1065353216,
   859553071,
   0,
   1065353216,
   859553071,
   0,
   1065353216,
   859553071,
   0,
   1065353216,
   859553071;
  }
 }
}