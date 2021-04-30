using System.Drawing;
using OpenTK;

namespace OpenTKApp.Primitives
{
    public static class Cube
    {
        public static void DrawCube(float size, Color color)
        {
            Parallelepiped.Draw(Vector3.One * size, color);
        }
    }
}