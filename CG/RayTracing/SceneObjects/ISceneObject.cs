using System.Drawing;
using System.Numerics;

namespace RayTracing.SceneObjects
{
    public interface ISceneObject
    {
        public Color Color { get; }
        public int Specular { get; }
        public float Reflective { get; }

        public float[] IntersectRay(Vector3 origin, Vector3 direction);
        public Vector3 GetNormal(Vector3 intersectionPoint);
    }
}