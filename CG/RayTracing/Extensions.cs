using System;
using System.Drawing;
using System.Numerics;

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
            var r = ((int) (color.R * brightness)).Clamp(0, 255);
            var g = ((int) (color.G * brightness)).Clamp(0, 255);
            var b = ((int) (color.B * brightness)).Clamp(0, 255);

            return Color.FromArgb(color.A, r, g, b);
        }

        public static int Clamp(this int value, int min, int max) => Math.Min(max, Math.Max(min, value));

        public static Color Add(this Color color, Color otherColor)
        {
            var r = (color.R + otherColor.R).Clamp(0, 255);
            var g = (color.G + otherColor.G).Clamp(0, 255);
            var b = (color.B + otherColor.B).Clamp(0, 255);

            return Color.FromArgb(color.A, r, g, b);
        }

        public static Vector3 MultiplyMatrix(this Vector3 vector, float[,] matrix)
        {
            var result = new float[3];
            var vectorMatrix = new[] {vector.X, vector.Y, vector.Z};

            for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
                result[i] += vectorMatrix[j] * matrix[i, j];

            return new Vector3(result[0], result[1], result[2]);
        }

        public static Vector3 Rotate(this Vector3 vector, Vector3 rotation)
        {
            rotation *= MathF.PI / 180;

            if (rotation.X != 0)
                vector = vector.RotateX(rotation.X);
            if (rotation.Y != 0)
                vector = vector.RotateY(rotation.Y);
            if (rotation.Z != 0)
                vector = vector.RotateZ(rotation.Z);

            return vector;
        }

        public static Vector3 RotateX(this Vector3 vector, float angle)
        {
            var rotationMatrix = new[,]
            {
                {1, 0, 0},
                {0, MathF.Cos(angle), -MathF.Sin(angle)},
                {0, MathF.Sin(angle), MathF.Cos(angle)}
            };

            return vector.MultiplyMatrix(rotationMatrix);
        }

        public static Vector3 RotateY(this Vector3 vector, float angle)
        {
            var rotationMatrix = new[,]
            {
                {MathF.Cos(angle), 0, MathF.Sin(angle)},
                {0, 1, 0},
                {-MathF.Sin(angle), 0, MathF.Cos(angle)}
            };

            return vector.MultiplyMatrix(rotationMatrix);
        }

        public static Vector3 RotateZ(this Vector3 vector, float angle)
        {
            var rotationMatrix = new[,]
            {
                {MathF.Cos(angle), -MathF.Sin(angle), 0},
                {MathF.Sin(angle), MathF.Cos(angle), 0},
                {0, 0, 1}
            };

            return vector.MultiplyMatrix(rotationMatrix);
        }

        public static float[] ToArray(this Vector3 vector) => new[] {vector.X, vector.Y, vector.Z};
    }
}