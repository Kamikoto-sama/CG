using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using RayTracing.SceneObjects;

namespace RayTracing
{
    public class Scene
    {
        public Size ViewPortSize { get; set; } = new Size(1, 1);
        public Canvas Canvas { get; }
        public float ProjectionPlaneZ { get; set; } = 1;
        public Vector3 CameraPosition { get; set; }

        public List<ISceneObject> Objects { get; }

        public Scene(Canvas canvas)
        {
            Canvas = canvas;
            Objects = new List<ISceneObject>();
        }

        public Vector3 CanvasToViewport(PointF p2d)
        {
            var x = p2d.X * ViewPortSize.Width / Canvas.Width;
            var y = p2d.Y * ViewPortSize.Height / Canvas.Height;

            return new Vector3(x, y, ProjectionPlaneZ);
        }
    }
}