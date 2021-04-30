using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace OpenTKApp.Primitives
{
    public static class Axis
    {
        public static void Draw(int lenght = 50, bool bothDirections = false)
        {
            GL.Begin(PrimitiveType.Lines);
            // X
            GL.Color3(Color.Red);
            GL.Vertex3(lenght, 0, 0);
            if (bothDirections)
                GL.Vertex3(-lenght, 0, 0);
            else
                GL.Vertex3(0, 0, 0);

            // Y
            GL.Color3(Color.Green);
            GL.Vertex3(0, lenght, 0);
            if (bothDirections)
                GL.Vertex3(0, -lenght, 0);
            else
                GL.Vertex3(0, 0, 0);

            // Z
            GL.Color3(Color.Blue);
            GL.Vertex3(0, 0, lenght);
            if (bothDirections)
                GL.Vertex3(0, 0, -lenght);
            else
                GL.Vertex3(0, 0, 0);
            GL.End();
        }

    }
}