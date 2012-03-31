using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;

namespace OpenTKProject.Engine
{
    public class BufferObject<V> where V : struct
    {
        private int _vboLocation;
        private BufferTarget _bufferTarget;

        public BufferObject(BufferTarget bt, V[] data, BufferUsageHint hint = BufferUsageHint.StaticDraw)
        {
            _bufferTarget = bt;
            GL.GenBuffers(1, out _vboLocation);
            GL.BindBuffer(bt, _vboLocation);
            GL.BufferData(bt, new IntPtr(data.Length * Marshal.SizeOf(new V())), data, hint);
        }

        public void Bind()
        {
            GL.BindBuffer(_bufferTarget, _vboLocation);
        }
    }
}
