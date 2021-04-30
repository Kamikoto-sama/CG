using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTKApp
{
    public static class Common
    {

        public static IEnumerable<PointF> GetRegularPolygonPoints(int pointsCount, PointF center, float radius)
        {
            var angle = Math.PI * 2 / pointsCount;

            if (pointsCount <= 0)
                return Array.Empty<PointF>();

            return Enumerable.Range(0, pointsCount + 1)
                .Select(i =>
                {
                    var offsetX = (float) Math.Sin(i * angle) * radius;
                    var offsetY = (float) Math.Cos(i * angle);
                    var offset = new SizeF(offsetX, offsetY * radius);
                    return PointF.Add(center, offset);
                });
        }
    }
}