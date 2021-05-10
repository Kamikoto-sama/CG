using System.Drawing;
using System.Numerics;
using RayTracing.SceneObjects;

namespace RayTracing
{
    public class Renderer
    {
        public Scene Scene { get; }

        public Renderer(Scene scene) => Scene = scene;

        public Bitmap Render()
        {
            var canvas = Scene.Canvas;
            var resultBitmap = new Bitmap(canvas.Width, canvas.Height);

            foreach (var pixel in canvas)
            {
                var direction = Scene.CanvasToViewport(pixel);
                var color = TraceRay(Scene.CameraPosition, direction, 1, float.MaxValue);
                resultBitmap.SetPixel(pixel, color);
            }

            return resultBitmap;
        }

        private Color TraceRay(Vector3 origin, Vector3 direction, float minDistance, float maxDistance)
        {
            var closestIntersection = float.MaxValue;
            ISceneObject closestSphere = null;

            foreach (var sceneObject in Scene.Objects)
            {
                var intersects = sceneObject.IntersectRay(origin, direction);

                if (intersects == null)
                    continue;

                if (intersects[0] < closestIntersection && minDistance < intersects[0] && intersects[0] < maxDistance)
                {
                    closestIntersection = intersects[0];
                    closestSphere = sceneObject;
                }

                if (intersects[1] < closestIntersection && minDistance < intersects[1] && intersects[1] < maxDistance)
                {
                    closestIntersection = intersects[1];
                    closestSphere = sceneObject;
                }
            }

            return closestSphere?.Color ?? Scene.Canvas.BackgroundColor;
        }
    }
}