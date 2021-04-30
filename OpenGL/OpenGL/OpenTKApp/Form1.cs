using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTKApp.Primitives;
using OpenTKApp.TankParts;

namespace OpenTKApp
{
    public partial class Form1 : Form
    {
        private const int CamRotationAngle = 10;
        private bool loaded;
        private Vector3 camPosition = Vector3.One * 60;

        private float camRotationDirection;
        private Vector3 camRotationAxis;

        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;

            GL.ClearColor(Color.SkyBlue);
            GL.Enable(EnableCap.DepthTest);

            // Matrix4 p = Matrix4.CreatePerspectiveFieldOfView((float) (90 * Math.PI / 180), 1, 1, 500);
            // GL.MatrixMode(MatrixMode.Projection);
            // GL.LoadMatrix(ref p);
            //
            // UpdateCam();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!loaded)
                return;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);

            Axis.Draw(100);

            GL.Color3(Color.Red);
            GL.Rect(new RectangleF(-.1f, -.1f, .5f, .5f));



            glControl1.SwapBuffers();
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!loaded) return;

            var direction = 0;
            var axis = Vector3.Zero;

            switch (e.KeyCode)
            {
                case Keys.A:
                    direction = 1;
                    axis = Vector3.UnitZ;
                    break;
                case Keys.D:
                    direction = -1;
                    axis = Vector3.UnitZ;
                    break;
                case Keys.W:
                    direction = -1;
                    axis = Vector3.UnitX;
                    break;
                case Keys.S:
                    direction = 1;
                    axis = Vector3.UnitX;
                    break;
                case Keys.Q:
                    direction = -1;
                    axis = Vector3.UnitY;
                    break;
                case Keys.E:
                    direction = 1;
                    axis = Vector3.UnitY;
                    break;
                case Keys.Escape:
                    Close();
                    return;
            }

            camRotationDirection = direction;
            camRotationAxis = axis;

            glControl1.Invalidate();
        }

        private void UpdateCam()
        {
            Matrix4 modelview = Matrix4.LookAt(camPosition, Vector3.Zero, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);
        }
    }
}