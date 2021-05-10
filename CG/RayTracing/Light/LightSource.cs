using System.Numerics;

namespace RayTracing.Light
{
    public class LightSource
    {
        public LightSourceType Type { get; }
        public float Intensity { get; }
        public Vector3 Position { get; set; }
        public Vector3 Direction { get; set; }

        public LightSource(LightSourceType type, float intensity)
        {
            Type = type;
            Intensity = intensity;
        }
    }
}