using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RayTracing
{
    public class Canvas : IEnumerable<Point>
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Color BackgroundColor { get; set; }

        public Canvas(Size size)
        {
            Width = size.Width;
            Height = size.Height;
            BackgroundColor = Color.SkyBlue;
        }

        public IEnumerator<Point> GetEnumerator()
        {
            for (var y = Height / 2; y > -Height / 2; y--)
            for (var x = -Width / 2; x < Width / 2; x++)
                yield return new Point(x, y);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}