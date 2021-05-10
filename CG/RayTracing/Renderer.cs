using System.Drawing;
using System.Linq;
using System.Numerics;
using RayTracing.Light;
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
            ISceneObject closestObject = null;

            foreach (var sceneObject in Scene.Objects)
            {
                var intersects = sceneObject.IntersectRay(origin, direction);

                if (intersects == null)
                    continue;

                var min = intersects.Min();
                if (min < closestIntersection && min > minDistance && min < maxDistance)
                {
                    closestIntersection = min;
                    closestObject = sceneObject;
                }
            }

            if (closestObject == null)
                return Scene.Canvas.BackgroundColor;

            // var point = Add(origin, Multiply(closest_t, direction));
            // normal = Multiply(1.0 / Length(normal), normal);

            var intersectionPoint = origin + closestIntersection * direction;
            var normal = closestObject.GetNormal(intersectionPoint);
            var intensity = ComputeLighting(intersectionPoint, normal);

            return closestObject.Color.WithBrightness(intensity);

            // return Multiply(ComputeLighting(point, normal), closest_sphere.color);
        }

        private float ComputeLighting(Vector3 intersectionPoint, Vector3 normal)
        {
            var resultIntensity = 0f;
            var normalLength = normal.Length();

            foreach (var light in Scene.LightSources)
            {
                // if (light.ltype == Light.AMBIENT) {
                //     resultIntensity += light.intensity;
                // } else {
                //     var vec_l;
                //     if (light.ltype == Light.POINT) {
                //         vec_l = Subtract(light.position, point);
                //     } else {  // Light.DIRECTIONAL
                //         vec_l = light.position;
                //     }
                //
                //     var n_dot_l = DotProduct(normal, vec_l);
                //     if (n_dot_l > 0) {
                //         resultIntensity += light.intensity * n_dot_l / (normalLength * Length(vec_l));
                //     }
                // }
                Vector3 lightDirection;

                switch (light.Type)
                {
                    case LightSourceType.Ambient:
                        resultIntensity += light.Intensity;
                        continue;
                    case LightSourceType.Point:
                        lightDirection = light.Position - intersectionPoint;
                        break;
                    case LightSourceType.Directional:
                        lightDirection = light.Direction;
                        break;
                    default:
                        continue;
                }

                var incidenceAngle = Vector3.Dot(normal, lightDirection);
                if (incidenceAngle > 0)
                    resultIntensity += light.Intensity * incidenceAngle / (normalLength * lightDirection.Length());
            }

            return resultIntensity;
        }
    }
}