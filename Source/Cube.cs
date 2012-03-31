using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTKProject.Engine;

namespace OpenTKProject.Source
{
    public class Cube : Shape
    {
        public BufferObject<Vector3> VertexData { get; private set; }
        public BufferObject<Vector3> NormalData { get; private set; }
//        public BufferObject<int> IndexData { get; private set; }

        public static int ColorToRgba32(Color c)
        {
            return (c.A << 24) | (c.B << 16) | (c.G << 8) | c.R;
        }

        public Cube()
        {
            Vertices = new Vector3[]
                           {
                               new Vector3(1.0f, -1.0f, -1.0f),
                               new Vector3(1.0f, -1.0f, 1.0f),
                               new Vector3(-1.0f, -1.0f, 1.0f),
                               new Vector3(-1.0f, -1.0f, -1.0f),

                               new Vector3(-1.0f, 1.0f, -1.0f),
                               new Vector3(-1.0f, 1.0f, 1.0f),
                               new Vector3(1.0f, 1.0f, 1.0f),
                               new Vector3(1.0f, 1.0f, -1.0f),

                               new Vector3(-1.0f, -1.0f, -1.0f),
                               new Vector3(-1.0f, -1.0f, 1.0f),
                               new Vector3(-1.0f, 1.0f, 1.0f),
                               new Vector3(-1.0f, 1.0f, -1.0f),

                               new Vector3(1.0f, 1.0f, -1.0f),
                               new Vector3(1.0f, 1.0f, 1.0f),
                               new Vector3(1.0f, -1.0f, 1.0f),
                               new Vector3(1.0f, -1.0f, -1.0f),

                               new Vector3(-1.0f, -1.0f, -1.0f),
                               new Vector3(-1.0f, 1.0f, -1.0f),
                               new Vector3(1.0f, 1.0f, -1.0f),
                               new Vector3(1.0f, -1.0f, -1.0f),

                               new Vector3(1.0f, -1.0f, 1.0f),
                               new Vector3(1.0f, 1.0f, 1.0f),
                               new Vector3(-1.0f, 1.0f, 1.0f),
                               new Vector3(-1.0f, -1.0f, 1.0f)
                           };

            Normals = new Vector3[]
                          {
                              new Vector3(0.0f, -1.0f, 0.0f),
                              new Vector3(0.0f, -1.0f, 0.0f),
                              new Vector3(0.0f, -1.0f, 0.0f),
                              new Vector3(0.0f, -1.0f, 0.0f),

                              new Vector3(0.0f, 1.0f, 0.0f),
                              new Vector3(0.0f, 1.0f, 0.0f),
                              new Vector3(0.0f, 1.0f, 0.0f),
                              new Vector3(0.0f, 1.0f, 0.0f),

                              new Vector3(-1.0f, 0.0f, 0.0f),
                              new Vector3(-1.0f, 0.0f, 0.0f),
                              new Vector3(-1.0f, 0.0f, 0.0f),
                              new Vector3(-1.0f, 0.0f, 0.0f),

                              new Vector3(1.0f, 0.0f, 0.0f),
                              new Vector3(1.0f, 0.0f, 0.0f),
                              new Vector3(1.0f, 0.0f, 0.0f),
                              new Vector3(1.0f, 0.0f, 0.0f),

                              new Vector3(0.0f, 0.0f, -1.0f),
                              new Vector3(0.0f, 0.0f, -1.0f),
                              new Vector3(0.0f, 0.0f, -1.0f),
                              new Vector3(0.0f, 0.0f, -1.0f),

                              new Vector3(0.0f, 0.0f, 1.0f),
                              new Vector3(0.0f, 0.0f, 1.0f),
                              new Vector3(0.0f, 0.0f, 1.0f),
                              new Vector3(0.0f, 0.0f, 1.0f)
                          };

            Colors = new int[]
            {
                ColorToRgba32(Color.DarkRed),
                ColorToRgba32(Color.DarkRed),
                ColorToRgba32(Color.Gold),
                ColorToRgba32(Color.Gold),
                ColorToRgba32(Color.DarkRed),
                ColorToRgba32(Color.DarkRed),
                ColorToRgba32(Color.Gold),
                ColorToRgba32(Color.Gold),
            };

            Texcoords = new Vector2[]
            {
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(1, 0),
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(1, 0),
                new Vector2(0, 0),
            };

            
            VertexData = new BufferObject<Vector3>(BufferTarget.ArrayBuffer, Vertices);
            NormalData = new BufferObject<Vector3>(BufferTarget.ArrayBuffer, Normals);
//            IndexData = new BufferObject<int>(BufferTarget.ElementArrayBuffer, Indices);
        }

        public void Draw()
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.NormalArray);
            VertexData.Bind();
            GL.VertexPointer(3, VertexPointerType.Float, Vector3.SizeInBytes, IntPtr.Zero);

            NormalData.Bind();
            GL.NormalPointer(NormalPointerType.Float, 0, IntPtr.Zero);

//            IndexData.Bind();

            //            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, true, Vector3.SizeInBytes, 0);
            //            GL.BindAttribLocation(_program.ID, 0, "in_position");

            //            GL.DrawElements(BeginMode.Triangles, _cube.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
            GL.DrawArrays(BeginMode.Quads, 0, 24);

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.NormalArray);
        }
    }
}