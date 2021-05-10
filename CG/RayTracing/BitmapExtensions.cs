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
    }
}