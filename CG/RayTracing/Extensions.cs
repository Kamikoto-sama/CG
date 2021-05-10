using System.Drawing;

namespace RayTracing
{
    public static class BitmapExtensions
    {
        public static void SetPixel(this Bitmap bitmap, Point point, Color color)
        {
            var x = bitmap.Width / 2 + point.X;
            var y = bitmap.Height / 2 - point.Y - 1;

            if (x < 0 || x >= bitmap.Width || y < 0 || y >= bitmap.Height)
                return;

            bitmap.SetPixel(x, y, color);
        }

        public static Color WithBrightness(this Color color, float brightness)
        {
            var r = (int) (color.R * brightness);
            var g = (int) (color.G * brightness);
            var b = (int) (color.B * brightness);

            return Color.FromArgb(255, r, g, b);
        }
    }
}