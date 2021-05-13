using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using RayTracing.Light;
using RayTracing.SceneObjects;

namespace RayTracing
{
    public class Scene
    {
        public Size ViewPortSize { get; set; }
        public Canvas Canvas { get; }
        public float ProjectionPlaneZ { get; set; }

        public Vector3 CameraPosition { get; set; }
        public Vector3 CameraRotation { get; set; }

        public List<ISceneObject> Objects { get; }
        public List<LightSource> LightSources { get; }

        public Scene(Canvas canvas)
        {
            Canvas = canvas;
            ViewPortSize = new Size(canvas.Width / canvas.Height, canvas.Height / canvas.Width);
            ProjectionPlaneZ = 1;
            Objects = new List<ISceneObject>();
            LightSources = new List<LightSource>();
        }

        public Vector3 CanvasToViewport(PointF p2d)
        {
            var x = p2d.X * ViewPortSize.Width / Canvas.Width;
            var y = p2d.Y * ViewPortSize.Height / Canvas.Height;

            return new Vector3(x, y, ProjectionPlaneZ);
        }
    }
}