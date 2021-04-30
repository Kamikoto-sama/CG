using System.Drawing;
using System.Linq;
using OpenTK.Graphics.OpenGL;

namespace OpenTKApp.Primitives
{
    public static class Cylinder
    {
        public static void Draw(float radius, float height, Color color, bool covered = false)
        {
            const int pointsCount = 50;
            var center = new PointF(radius, radius);
            var contourPoints = Common.GetRegularPolygonPoints(pointsCount, center, radius).ToArray();

            GL.Color3(color);

            // Стенки
            GL.Begin(PrimitiveType.QuadStrip);
            foreach (var point in contourPoints)
            {
                GL.Vertex3(point.X, 0, point.Y);
                GL.Vertex3(point.X, height, point.Y);
            }
            GL.End();

            if (!covered)
                return;

            // Крышки
            for (var y = 0f; y <= radius; y += radius)
            {
                GL.Begin(PrimitiveType.Polygon);
                foreach (var point in contourPoints)
                    GL.Vertex3(point.X, y, point.Y);
                GL.End();
            }
        }

    }
}