using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTKProject.Engine;

namespace OpenTKProject.Source
{
    class Game : GameWindow
    {
        private double _time;
        private Cube _cube;
        private PhongShader _shader;

        public Game()
                        : base(800, 600, GraphicsMode.Default, "Game")
            //: base(800, 600, new GraphicsMode(32, 32, 0, 4), "Game")
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _cube = new Cube();

            GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);
            GL.Enable(EnableCap.DepthTest);

            _shader = new PhongShader();

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

            float aspectRatio = ClientSize.Width / (float)(ClientSize.Height);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, aspectRatio, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard[Key.Escape])
                Exit();

            _time += e.Time;
            _shader["time"] = (float)_time;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            _shader.Use();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            SetupCamera();

            //            _cube.Draw();

            var points = new[] { new Vector2(0, 0), new Vector2(1, 2), new Vector2(-1, 1) };
            var aBezierCurve = new BezierCurve(points);

            GL.LineWidth(4.0f);

            CatmullRomSpline a = new CatmullRomSpline();
            a.AddPoint(new Vector3(-1, -1, 0));
            a.AddPoint(new Vector3(1, 1, 0));
            a.AddPoint(new Vector3(1, -1, 0));
            a.AddPoint(new Vector3(-1, 1, 0));
            a.AddPoint(new Vector3(-1, -1, 0));

            Vector3 vec = a.GetValue((float)_time / 10);
            GL.Translate(vec);

            _cube.Draw();

            SwapBuffers();
        }

        protected void SetupCamera()
        {
            Matrix4 modelview = Matrix4.LookAt(new Vector3(0, 3, 5), Vector3.Zero, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);
        }

        [STAThread]
        static void Main()
        {
            using (var game = new Game())
            {
                game.Run(updates_per_second: 30.0, frames_per_second: 60.0);
            }
        }
    }
}