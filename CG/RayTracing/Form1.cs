﻿using System;
using System.Drawing;
using System.Numerics;
using System.Threading;
using System.Windows.Forms;
using RayTracing.Light;
using RayTracing.SceneObjects;

namespace RayTracing
{
    public partial class Form1 : Form
    {
        private readonly Renderer renderer;
        private Thread rendererThread;

        public Form1()
        {
            InitializeComponent();

            var size = new Size(600, 600);
            pictureBox.Size = size + new Size(1, 1);

            var canvas = new Canvas(size);
            var scene = new Scene(canvas);
            ConfigureScene(scene);
            renderer = new Renderer(scene);

            InitFields(canvas);
        }

        private void InitFields(Canvas canvas)
        {
            sizeWidth.Text = canvas.Width.ToString();
            sizeHeight.Text = canvas.Height.ToString();
            pixelsCount.Text = (canvas.Width * canvas.Height).ToString();

            tracingDepth.Text = renderer.TracingDepth.ToString();

            saveFileDialog.DefaultExt = ".png";
            saveFileDialog.Title = "Save screen";

            var scene = renderer.Scene;
            var position = scene.CameraPosition;
            var rotation = scene.CameraRotation;

            positionX.Text = position.X.ToString();
            positionY.Text = position.Y.ToString();
            positionZ.Text = position.Z.ToString();

            rotationX.Text = rotation.X.ToString();
            rotationY.Text = rotation.Y.ToString();
            rotationZ.Text = rotation.Z.ToString();
        }

        private void ConfigureScene(Scene scene)
        {
            var mat = new Material(-1, .1f, .5f, Color.Black, Color.White);

            scene.Objects.AddRange(new[]
            {
                new Sphere(Vector3.UnitY * -1001, 1000, mat),
                new Sphere(new Vector3(-.8f, -.75f, 3.5f), .3f, Color.Red, 1, .1f),
                new Sphere(new Vector3(0, -.7f, 4.5f), .3f, Color.Green, 50, .2f),
                new Sphere(new Vector3(.8f, -.7f, 3.5f), .3f, Color.Blue, 100, .3f),
            });

            scene.LightSources.AddRange(new[]
            {
                new LightSource(LightSourceType.Ambient, .2f),
                new LightSource(LightSourceType.Point, .6f) {Position = new Vector3(2, 1, 0)},
                new LightSource(LightSourceType.Directional, .2f) {Direction = new Vector3(1, 4, 0)},
            });

            scene.CameraPosition = Vector3.UnitY * -.5f;
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

            pixelsCount.Text = "Pixels count:" + width * height;
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

            rendererThread = new Thread(() =>
            {
                var bitmap = renderer.Render();

                pictureBox.Image = bitmap;

                renderBtn.Enabled = true;
                renderBtn.Text = "Render";
                saveBtn.Enabled = true;
            });
            rendererThread.Start();
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
        }

        private void tracingDepth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
                Close();

            if (e.KeyChar != 13)
                return;

            if (!int.TryParse(tracingDepth.Text, out var depth))
            {
                MessageBox.Show("Неверный формат");
                return;
            }

            renderer.TracingDepth = depth;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            pictureBox.Image.Save(saveFileDialog.FileName);
        }

        private void ApplyTransform(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13)
                return;

            if (!float.TryParse(positionX.Text, out var posX) ||
                !float.TryParse(positionY.Text, out var posY) ||
                !float.TryParse(positionZ.Text, out var posZ) ||
                !float.TryParse(rotationX.Text, out var rotX) ||
                !float.TryParse(rotationY.Text, out var rotY) ||
                !float.TryParse(rotationZ.Text, out var rotZ))
            {
                MessageBox.Show("Неверный формат");
                return;
            }

            var scene = renderer.Scene;
            scene.CameraPosition = new Vector3(posX, posY, posZ);
            scene.CameraRotation = new Vector3(rotX, rotY, rotZ);
        }
    }
}