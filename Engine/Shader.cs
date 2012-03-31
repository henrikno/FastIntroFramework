using System;
using System.IO;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTKProject.Engine
{
    public abstract class Shader : IDisposable
    {
        public Shader(ShaderType type, params string[] source)
        {
            CompileShader(type, source);
        }

		public Shader(ShaderType type, string source)
        {
            CompileShader(type, source);
        }

        public Shader(ShaderType type, FileInfo f)
            : this(type, File.ReadAllText(f.FullName, Encoding.UTF8))
        { }

        private int _shaderId;
        public int ID
        {
            get
            {
                return _shaderId;
            }
        }
		
		protected void CompileShader(ShaderType type, string source)
        {
            _shaderId = GL.CreateShader(type);
            
            GL.ShaderSource(ID, source);
            GL.CompileShader(ID);

            int status;
            GL.GetShader(ID, ShaderParameter.CompileStatus, out status);
            if (status == 0)
                throw new GraphicsException(
                    String.Format("Error compiling {0} shader: {1}", type.ToString(), GL.GetShaderInfoLog(ID)));
        }

        protected void CompileShader(ShaderType type, string[] source)
        {
            _shaderId = GL.CreateShader(type);
            int[] lens = new int[source.Length];
            for (int i = 0; i < lens.Length; i++)
                lens[i] = -1;
            GL.ShaderSource(ID, source.Length, source, ref lens[0]);
            GL.CompileShader(ID);

            int status;
            GL.GetShader(ID, ShaderParameter.CompileStatus, out status);
            if (status == 0)
                throw new GraphicsException(
                    String.Format("Error compiling {0} shader: {1}", type.ToString(), GL.GetShaderInfoLog(ID)));
        }

        public void Dispose()
        {
            if (_shaderId >= 0)
                GL.DeleteShader(ID);
            _shaderId = -1;
        }
    }

    public class VertexShader : Shader
    {
        public VertexShader(params string[] source)
            : base(ShaderType.VertexShader, source)
        { }
		
		        public VertexShader(string source)
            : base(ShaderType.VertexShader, source)
        { }

        public VertexShader(FileInfo f)
            : base(ShaderType.VertexShader, f)
        { }
    }

    public class FragmentShader : Shader
    {
        public FragmentShader(params string[] source)
            : base(ShaderType.FragmentShader, source)
        { }
		
		public FragmentShader(string source)
            : base(ShaderType.FragmentShader, source)
        { }

        public FragmentShader(FileInfo f)
            : base(ShaderType.FragmentShader, f)
        { }
    }

    public class GeometryShader : Shader
    {
        public GeometryShader(params string[] source)
            : base(ShaderType.GeometryShader, source)
        { }
		
		        public GeometryShader(string source)
            : base(ShaderType.GeometryShader, source)
        { }

        public GeometryShader(FileInfo f)
            : base(ShaderType.GeometryShader, f)
        { }
    }
}
