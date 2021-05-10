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

        public Sphere(Vector3 center, float radius, Color color)
        {
            Center = center;
            Radius = radius;
            Color = color;
        }

        public float[] IntersectRay(Vector3 origin, Vector3 direction)
        {
            var oc = origin - Center; //Subtract(origin, sphere.center);

            var k1 = Vector3.Dot(direction, direction); //DotProduct(direction, direction);
            var k2 = 2 * Vector3.Dot(oc, direction); //2*DotProduct(oc, direction);
            var k3 = Vector3.Dot(oc, oc) - Radius * Radius; //DotProduct(oc, oc) - sphere.radius * sphere.radius;

            var discriminant = k2 * k2 - 4 * k1 * k3;
            if (discriminant < 0)
                return null;

            var t1 = (float) (-k2 + Math.Sqrt(discriminant)) / (2 * k1);
            var t2 = (float) (-k2 - Math.Sqrt(discriminant)) / (2 * k1);

            return new[] {t1, t2};
        }
    }
}