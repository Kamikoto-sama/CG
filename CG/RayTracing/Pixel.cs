using System.Drawing;

namespace RayTracing
{
    public struct Pixel
    {
        public Point Coordinates { get; }
        public Color Color { get; }

        public Pixel(Point coordinates, Color color)
        {
            Coordinates = coordinates;
            Color = color;
        }
    }
}