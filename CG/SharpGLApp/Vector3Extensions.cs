using System.Numerics;

namespace SharpGL
{
    public static class Vector3Extensions
    {
        public static Vector3 Normalize(this Vector3 vector3)
        {
            var length = vector3.Length();
            var x = vector3.X / length;
            var y = vector3.Y / length;
            var z = vector3.Z / length;

            return new Vector3(x, y, z);
        }
    }
}