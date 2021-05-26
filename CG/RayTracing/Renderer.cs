using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using RayTracing.Light;
using RayTracing.SceneObjects;

namespace RayTracing
{
    public class Renderer
    {
        public Scene Scene { get; }
        public int TracingDepth { get; set; } = 3;

        public Renderer(Scene scene) => Scene = scene;

        private const float EpsilonDistance = 1e-8f;

        public Bitmap Render()
        {
            var canvas = Scene.Canvas;
            var resultPixels = new ConcurrentBag<Pixel>();

            // var tasks = canvas.Select(pixel => Task.Run(() =>
            // {
            //     var direction = Scene.CanvasToViewport(pixel);
            //     direction = direction.Rotate(Scene.CameraRotation);
            //
            //     var color = TraceRay(Scene.CameraPosition, direction, 1, float.MaxValue, TracingDepth);
            //
            //     resultPixels.Add(new Pixel(pixel, color));
            // })).ToArray();
            //
            // Task.WaitAll(tasks);

            foreach (var pixel in canvas)
            {
                var direction = Scene.CanvasToViewport(pixel);
                direction = direction.Rotate(Scene.CameraRotation);
                var color = TraceRay(Scene.CameraPosition, direction, 1, float.MaxValue, TracingDepth);
                resultPixels.Add(new Pixel(pixel, color));
            }

            var resultBitmap = new Bitmap(canvas.Width, canvas.Height);
            foreach (var pixel in resultPixels)
                resultBitmap.SetPixel(pixel.Coordinates, pixel.Color);

            return resultBitmap;
        }

        private Color TraceRay(
            Vector3 origin,
            Vector3 direction,
            float minDistance,
            float maxDistance,
            int tracingDepth)
        {
            var (closestIntersection, closestObject) =
                GetClosestIntersection(origin, direction, minDistance, maxDistance);

            if (closestObject == null)
                return Scene.Canvas.BackgroundColor;

            var intersectionPoint = origin + closestIntersection * direction;
            var normal = closestObject.GetNormal(intersectionPoint);

            var originDirection = -direction;
            var lighting = ComputeLighting(intersectionPoint, normal, originDirection, closestObject.Specular);
            var objColor = closestObject.GetColor(intersectionPoint);
            var currentColor = objColor.WithBrightness(lighting);

            if (closestObject.Reflective <= 0 || tracingDepth <= 0)
                return currentColor;

            var reflectedRay = ReflectRay(originDirection, normal);
            var reflectedColor = TraceRay(
                intersectionPoint,
                reflectedRay,
                EpsilonDistance,
                float.MaxValue,
                tracingDepth - 1);

            return currentColor.WithBrightness(1 - closestObject.Reflective)
                .Add(reflectedColor.WithBrightness(closestObject.Reflective));
        }

        private (float, ISceneObject) GetClosestIntersection(
            Vector3 origin,
            Vector3 direction,
            float minDistance,
            float maxDistance)
        {
            var closestIntersection = float.MaxValue;
            ISceneObject closestObject = null;

            foreach (var sceneObject in Scene.Objects)
            {
                var intersects = sceneObject.IntersectRay(origin, direction);

                if (intersects == null)
                    continue;

                var closest = intersects.Min();
                if (closest < closestIntersection && closest > minDistance && closest < maxDistance)
                {
                    closestIntersection = closest;
                    closestObject = sceneObject;
                }
            }

            return (closestIntersection, closestObject);
        }

        private float ComputeLighting(Vector3 intersectionPoint, Vector3 normal, Vector3 originDirection, int specular)
        {
            var resultIntensity = 0f;
            var normalLength = normal.Length();

            foreach (var light in Scene.LightSources)
            {
                Vector3 lightRay;
                float maxDistance;

                switch (light.Type)
                {
                    case LightSourceType.Ambient:
                        resultIntensity += light.Intensity;
                        continue;
                    case LightSourceType.Point:
                        lightRay = light.Position - intersectionPoint;
                        maxDistance = 1;
                        break;
                    case LightSourceType.Directional:
                        lightRay = light.Direction;
                        maxDistance = float.MaxValue;
                        break;
                    default:
                        continue;
                }

                // Рассчет теней
                var (_, sceneObject) = GetClosestIntersection(
                    intersectionPoint,
                    lightRay,
                    EpsilonDistance,
                    maxDistance);

                if (sceneObject != null)
                    continue;

                // Рассчет диффузии (рассеивания)
                var incidenceAngle = Vector3.Dot(normal, lightRay);
                if (incidenceAngle > 0)
                    resultIntensity += light.Intensity * incidenceAngle / (normalLength * lightRay.Length());

                if (specular < 0)
                    continue;

                // Рассчет отраженного освещения (бликов)
                var reflectedLightRay = ReflectRay(lightRay, normal);
                var scatteringAngle = Vector3.Dot(reflectedLightRay, originDirection);
                if (scatteringAngle > 0)
                    resultIntensity += light.Intensity *
                                       MathF.Pow(
                                           scatteringAngle / (reflectedLightRay.Length() * originDirection.Length()),
                                           specular);
            }

            return resultIntensity;
        }

        private Vector3 ReflectRay(Vector3 ray, Vector3 normal) => 2 * Vector3.Dot(normal, ray) * normal - ray;
    }
}