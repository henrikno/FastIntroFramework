using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OpenTKProject.Engine;

namespace OpenTKProject.Source
{
    class PhongShader : ShaderProgram
    {
        public PhongShader()
        {
            Shader shader = new VertexShader(new FileInfo("Shaders/Phong.vert"));
            Shader fragmentShader = new FragmentShader(new FileInfo("Shaders/Phong.frag"));

            var shaders = new[] { shader, fragmentShader };

            Attach(shaders);
            Link();
            Clear();
        }
    }
}
