﻿using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SharpGL.TankParts;

namespace SharpGL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private readonly Vector3 defaultScenePosition = new(4, 2, -26);
        private const float MoveStepSize = 0.5f;
        private Vector3 scenePosition;

        private bool rotationMode;
        private readonly Vector3 defaultSceneRotation = new(10, 0, 0);
        private const float RotationAngle = 10;
        private Vector3 sceneRotation;

        public static string DebugText = "";

        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            var gl = openGLControl1.OpenGL;
            gl.ClearColor(.53f, .81f, .92f, 1);

            SetDefaults();
        }

        private void SetDefaults()
        {
            scenePosition = defaultScenePosition;
            sceneRotation = defaultSceneRotation;
        }

        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs args)
        {
            var gl = openGLControl1.OpenGL;

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.ResetHistory();

            AdjustScene(gl);

            gl.Translate(-13, 0, -5);

            Tank.Draw(gl);
            gl.DrawAxis(40);

            DrawUi(gl);

            gl.Flush();
        }

        private void AdjustScene(OpenGL gl)
        {
            gl.LoadIdentity();
            gl.DoTranslate(scenePosition);
            gl.Rotate(sceneRotation);
        }

        private void openGLControl1_KeyDown(object sender, KeyEventArgs e)
        {
            var moveAxis = Vector3.Zero;
            var rotationAxis = Vector3.Zero;

            switch (e.KeyCode)
            {
                case Keys.W:
                    moveAxis = Vector3.UnitY;
                    rotationAxis = -Vector3.UnitX;
                    break;
                case Keys.S:
                    moveAxis = -Vector3.UnitY;
                    rotationAxis = Vector3.UnitX;
                    break;
                case Keys.A:
                    moveAxis = -Vector3.UnitX;
                    rotationAxis = -Vector3.UnitY;
                    break;
                case Keys.D:
                    moveAxis = Vector3.UnitX;
                    rotationAxis = Vector3.UnitY;
                    break;
                case Keys.Q:
                    moveAxis = -Vector3.UnitZ;
                    rotationAxis = Vector3.UnitZ;
                    break;
                case Keys.E:
                    moveAxis = Vector3.UnitZ;
                    rotationAxis = -Vector3.UnitZ;
                    break;
                case Keys.Escape:
                    Close();
                    return;
                case Keys.R:
                    rotationMode = !rotationMode;
                    break;
                case Keys.Space:
                    SetDefaults();
                    break;
            }

            if (rotationMode)
                sceneRotation += rotationAxis * RotationAngle;
            else
                scenePosition += moveAxis * MoveStepSize;

            openGLControl1.Invalidate();
        }

        private void DrawUi(OpenGL gl)
        {
            gl.DrawText(new Point(5, 5), Color.Black, 26, DebugText);

            var height = openGLControl1.Height;

            var mode = rotationMode ? $"Rotation: {sceneRotation}" : $"Move: {scenePosition}";
            gl.DrawText(new Point(5, height - 16), Color.Black, 16, mode);
        }
    }
}