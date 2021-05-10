using System.Drawing;
using System.Numerics;

namespace RayTracing.SceneObjects
{
    public interface ISceneObject
    {
        public float[] IntersectRay(Vector3 origin, Vector3 direction);
        public Color Color { get; }

        public Vector3 GetNormal(Vector3 intersectionPoint);
    }
}