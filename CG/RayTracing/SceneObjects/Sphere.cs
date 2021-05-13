using System;
using System.Drawing;
using System.Numerics;

namespace RayTracing.SceneObjects
{
    public class Sphere : ISceneObject
    {
        public Vector3 Center { get; }
        public float Radius { get; }
        public Color Color { get; }
        public int Specular { get; }
        public float Reflective { get; }

        public Sphere(Vector3 center, float radius, Color color, int specular, float reflective)
        {
            Center = center;
            Radius = radius;
            Color = color;
            Specular = specular;
            Reflective = reflective;
        }

        public float[] IntersectRay(Vector3 origin, Vector3 direction)
        {
            var originToCenter = origin - Center;

            var k1 = Vector3.Dot(direction, direction);
            var k2 = 2 * Vector3.Dot(originToCenter, direction);
            var k3 = Vector3.Dot(originToCenter, originToCenter) - Radius * Radius;

            var discriminant = k2 * k2 - 4 * k1 * k3;
            if (discriminant < 0)
                return null;

            var intersection1 = (float) (-k2 + Math.Sqrt(discriminant)) / (2 * k1);
            var intersection2 = (float) (-k2 - Math.Sqrt(discriminant)) / (2 * k1);

            return new[] {intersection1, intersection2};
        }

        public Vector3 GetNormal(Vector3 intersectionPoint) => Vector3.Normalize(intersectionPoint - Center);
    }
}