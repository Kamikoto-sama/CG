using System.Drawing;

namespace RayTracing
{
    public static class GraphicsExtensions
    {
        public static void DrawPoint(this Graphics graphics, float x, float y, Color color)
        {
            var brush = new SolidBrush(color);
            const float size = 1;
            graphics.FillRectangle(brush, x, y, size, size);
        }
    }
}