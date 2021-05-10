using System;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Threading;
using System.Windows.Forms;
using RayTracing.SceneObjects;

namespace RayTracing
{
    public partial class Form1 : Form
    {
        private readonly Renderer renderer;

        public Form1()
        {
            InitializeComponent();

            var size = new Size(300, 300);
            pictureBox.Size = size + new Size(1, 1);

            var canvas = new Canvas(size);
            var scene = new Scene(canvas);
            FillScene(scene);
            renderer = new Renderer(scene);

            sizeWidth.Text = canvas.Width.ToString();
            sizeHeight.Text = canvas.Height.ToString();
            pixelsCount.Text = (canvas.Width * canvas.Height).ToString();
        }

        private void FillScene(Scene scene)
        {
            scene.Objects.AddRange(new[]
            {
                new Sphere(new Vector3(0, -1, 3), 1, Color.Red),
                new Sphere(new Vector3(2, 0, 4), 1, Color.Green),
                new Sphere(new Vector3(-2, 0, 4), 1, Color.Blue)
            });
        }

        private void size_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
                Close();

            if (e.KeyChar != 13)
                return;

            var widthText = sizeWidth.Text;
            var heightText = sizeHeight.Text;
            if (!int.TryParse(widthText, out var width) || !int.TryParse(heightText, out var height))
            {
                MessageBox.Show("Неверный формат");
                return;
            }

            pictureBox.Size = new Size(width + 1, height + 1);
            renderer.Scene.Canvas.Width = width;
            renderer.Scene.Canvas.Height = height;

            pixelsCount.Text = (width * height).ToString();
        }

        private void renderBtn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
                Close();
        }

        private void renderBtn_Click(object sender, EventArgs e)
        {
            renderBtn.Enabled = false;
            renderBtn.Text = "Rendering";

            var bitmap = renderer.Render();
            pictureBox.Image = bitmap;

            renderBtn.Enabled = true;
            renderBtn.Text = "Render";
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}