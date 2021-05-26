using System;
using System.Drawing;
using System.Numerics;

namespace RayTracing.SceneObjects
{
    public class Sphere : ISceneObject
    {
        private readonly Vector3 center;
        private readonly float radius;
        private readonly Color color;

        private readonly Material material;
        private readonly Color[] colors;
        private readonly Vector3 basis;

        public int Specular { get; }
        public float Reflective { get; }

        public Sphere(Vector3 center, float radius, Color color, int specular, float reflective)
        {
            this.color = color;
            this.center = center;
            this.radius = radius;
            Specular = specular;
            Reflective = reflective;
        }

        public Sphere(Vector3 center, float radius, Material material)
        {
            this.center = center;
            this.radius = radius;
            this.material = material;
            Specular = material.Specular;
            Reflective = material.Reflective;
            colors = new[] {material.Color1, material.Color2};

            basis = center + Vector3.UnitX * radius;
        }

        public float[] IntersectRay(Vector3 origin, Vector3 direction)
        {
            var originToCenter = origin - center;

            var k1 = Vector3.Dot(direction, direction);
            var k2 = 2 * Vector3.Dot(originToCenter, direction);
            var k3 = Vector3.Dot(originToCenter, originToCenter) - radius * radius;

            var discriminant = k2 * k2 - 4 * k1 * k3;
            if (discriminant < 0)
                return null;

            var intersection1 = (float) (-k2 + Math.Sqrt(discriminant)) / (2 * k1);
            var intersection2 = (float) (-k2 - Math.Sqrt(discriminant)) / (2 * k1);

            return new[] {intersection1, intersection2};
        }

        public Vector3 GetNormal(Vector3 intersectionPoint) => Vector3.Normalize(intersectionPoint - center);

        public Color GetColor(Vector3 intersectionPoint)
        {
            if (material == null)
                return color;

            var angleVector = intersectionPoint - center;

            var angleX = basis.GetAngle(new Vector3(angleVector.X, angleVector.Y, center.Z));
            var angleY = basis.GetAngle(new Vector3(angleVector.X, center.Y, angleVector.Z));

            var x = radius * angleX * (angleVector.Y < center.Y ? -1 : 1);
            var y = radius * angleY * (angleVector.Z < center.Z ? -1 : 1);

            return GetMatColor(x, y);
        }

        private Color GetMatColor(float x, float y)
        {
            var colorXIndex = (int) (x / material.SectorSize) % 2;
            var colorYIndex = (int) (y / material.SectorSize) % 2;

            var indexIsEven = Math.Abs(colorXIndex) == Math.Abs(colorYIndex);
            if (indexIsEven && (y >= 0 && x >= 0 || y < 0 && x < 0) ||
                !indexIsEven && (y >= 0 && x < 0 || y < 0 && x >= 0))
                return colors[0];
            return colors[1];
        }
    }
}