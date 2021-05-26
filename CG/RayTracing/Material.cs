using System.Drawing;

namespace RayTracing
{
    public class Material
    {
        public int Specular { get; }
        public float Reflective { get; }
        public float SectorSize { get; }
        public Color Color1 { get; }
        public Color Color2 { get; }

        public Material(int specular, float reflective, float sectorSize, Color color1, Color color2)
        {
            Specular = specular;
            Reflective = reflective;
            SectorSize = sectorSize;
            Color1 = color1;
            Color2 = color2;
        }
    }
}