using System;
using System.Drawing;
using System.Numerics;

namespace RayTracing.SceneObjects
{
    public class Rectangle : ISceneObject
    {
        private readonly Vector3 minPoint;
        private readonly Vector3 maxPoint;
        private readonly Vector3 normal = -Vector3.UnitZ;

        public Color Color { get; }
        public int Specular { get; }
        public float Reflective { get; }

        public Rectangle(Vector3 position, Size size, Color color, int specular, float reflective)
        {
            Color = color;
            Specular = specular;
            Reflective = reflective;

            minPoint = position;
            maxPoint = position + new Vector3(size.Width, size.Height, position.Z);
        }

        public float[] IntersectRay(Vector3 origin, Vector3 direction)
        {
            var t0 = -(Vector3.Dot(origin, normal) + 1) / Vector3.Dot(direction, normal);

            return t0 > 0 ? new[] {t0} : null;
        }

        public Vector3 GetNormal(Vector3 intersectionPoint) => normal;
    }
}