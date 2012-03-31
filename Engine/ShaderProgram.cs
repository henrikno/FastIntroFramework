using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System.Drawing;
using OpenTKProject.Engine;

namespace OpenTKProject.Source
{
    public class ShaderProgram : IDisposable
    {
        public ShaderProgram(params Shader[] shaders)
        {
            program_id = GL.CreateProgram();
            this.shaders = new List<Shader>(shaders.Length);
            Attach(shaders);
        }

        public IDisposable Use()
        {
            IDisposable r = new ShaderProgram.Handle(_currentProgram);
            if (_currentProgram != ID)
            {
                GL.UseProgram(ID);
                _currentProgram = ID;
            }
            return r;
        }

        public static void Clear()
        {
            GL.UseProgram(_currentProgram = 0);
        }

        public void Link()
        {
            _uniformMap = new Dictionary<string, int>();
            GL.LinkProgram(ID);

            int status;
            GL.GetProgram(ID, ProgramParameter.LinkStatus, out status);
            if (status == 0)
                throw new GraphicsException(
                    String.Format("Error linking program: {0}", GL.GetProgramInfoLog(ID)));
        }

        public void DisposeShaders()
        {
            foreach (Shader s in Shaders)
                s.Dispose();
            Shaders.Clear();
        }

        public void BindAttribLocation(int index, string name)
        {
            GL.BindAttribLocation(ID, index, name);
        }

        public void BindAttribLocation(params Tuple<int, string>[] attribs)
        {
            foreach (var t in attribs)
            {
                BindAttribLocation(t.Item1, t.Item2);
            }
        }

        public void BindFragDataLocation(int color, string name)
        {
            GL.BindFragDataLocation(ID, color, name);
        }

        public void BindFragDataLocation(params Tuple<int, string>[] attribs)
        {
            foreach (var t in attribs)
            {
                BindFragDataLocation(t.Item1, t.Item2);
            }
        }

        public void TransformFeedbackVaryings(TransformFeedbackMode mode, params string[] varyings)
        {
            GL.TransformFeedbackVaryings(ID, varyings.Length, varyings, mode);
        }

        public void Detach(params Shader[] shaders)
        {
            foreach (Shader s in shaders)
            {
                Shaders.Remove(s);
                GL.DetachShader(ID, s.ID);
            }
        }

        public void Attach(params Shader[] shaders)
        {
            foreach (Shader s in shaders)
            {
                Shaders.Add(s);
                GL.AttachShader(ID, s.ID);
            }
        }

        private int program_id;
        public int ID
        {
            get
            {
                return program_id;
            }
        }

        private List<Shader> shaders;
        public List<Shader> Shaders
        {
            get
            {
                return shaders;
            }
        }

        private static int _currentProgram = 0;
        private Dictionary<string, int> _uniformMap;
        public object this[string uniform]
        {
            set
            {
                Use();
                int uid;
                if (_uniformMap.ContainsKey(uniform))
                    uid = _uniformMap[uniform];
                else
                    uid = _uniformMap[uniform] = GL.GetUniformLocation(ID, uniform);
               
                int len = 0;

                Type t = value.GetType();
                if (t.IsArray)
                    len = ((Array)value).Length;

                if (     t == typeof(int))
                	GL.Uniform1(uid, (int)value);
                else if (t == typeof(int))
                    GL.Uniform1(uid, (int)value);
                else if (t == typeof(uint))
                    GL.Uniform1(uid, (uint)value);
                else if (t == typeof(float))
                    GL.Uniform1(uid, (float)value);
                else if (t == typeof(Vector2))
                    GL.Uniform2(uid, (Vector2)value);
                else if (t == typeof(Vector3))
                    GL.Uniform3(uid, (Vector3)value);
                else if (t == typeof(Vector4))
                    GL.Uniform4(uid, (Vector4)value);
                else if (t == typeof(Quaternion))
                    GL.Uniform4(uid, (Quaternion)value);
                else if (t == typeof(Color4))
                    GL.Uniform4(uid, (Color4)value);
                else if (t == typeof(int[]))
                    GL.Uniform1(uid, len, (int[])value);
                else if (t == typeof(uint[]))
                    GL.Uniform1(uid, len, (uint[])value);
                else if (t == typeof(float[]))
                    GL.Uniform1(uid, len, (float[])value);
                else if (t == typeof(Color))
                {
                    Color c = (Color)value; // why must I do this? OpenTK--
                    GL.Uniform4(uid, new Color4(c.R, c.G, c.B, c.A));
                }
                else
                    Console.Error.WriteLine("Unhandled type {0} for glUniform", t.FullName);
            }
        }

        public void Dispose()
        {
            if (ID >= 0)
                GL.DeleteProgram(ID);
            Shaders.Clear();
            program_id = -1;
        }

        private class Handle : IDisposable
        {
            private int prev;

            public Handle(int prev)
            {
                this.prev = prev;
            }
        
            public void Dispose()
            {
                if (ShaderProgram._currentProgram != prev)
                {
                    ShaderProgram._currentProgram = prev;
                    GL.UseProgram(prev);
                }
            }
        }
    }
}