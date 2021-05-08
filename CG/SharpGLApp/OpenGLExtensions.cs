using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using OpenTKApp;
using SharpGL.Enumerations;

namespace SharpGL
{
    public static class OpenGlExtensions
    {
        private static Color currentColor = System.Drawing.Color.Black;
        private static Color edgesColor = System.Drawing.Color.Black;
        private static Stack<Vector3> translations = new Stack<Vector3>();
        private static Stack<Vector3> rotations = new Stack<Vector3>();

        private static (float, float, float) ConvertColor(Color color)
        {
            return (color.R / 255f, color.G / 255f, color.B / 255f);
        }

        public static void Color(this OpenGL gl, Color color)
        {
            gl.Color(color.R, color.G, color.B);
        }

        public static void Vertex(this OpenGL gl, Vector3 v) => gl.Vertex(v.X, v.Y, v.Z);

        public static void DoTranslate(this OpenGL gl, Vector3 xyz, bool save = false)
        {
            gl.Translate(xyz.X, xyz.Y, xyz.Z);
            if (save)
                translations.Push(xyz);
        }

        public static void DoTranslate(this OpenGL gl, float x, float y, float z, bool save = false)
        {
            var diff = new Vector3(x, y, z);
            DoTranslate(gl, diff, save);
        }

        public static void Rotate(this OpenGL gl, Vector3 angle, bool save = false)
        {
            gl.Rotate(angle.X, angle.Y, angle.Z);
            if (save)
                rotations.Push(angle);
        }

        public static void DrawText(this OpenGL gl, Point position, Color color, float fontSize, string text)
        {
            var cl = ConvertColor(color);
            gl.DrawText(position.X, position.Y, cl.Item1, cl.Item2, cl.Item3, "FACE", fontSize, text);
        }

        public static void DrawAxis(this OpenGL gl, int lenght, bool bothDirections = false)
        {
            gl.Begin(BeginMode.Lines);
            // X
            Color(gl, System.Drawing.Color.Red);
            gl.Vertex(lenght, 0, 0);
            if (bothDirections)
                gl.Vertex(-lenght, 0, 0);
            else
                gl.Vertex(0, 0, 0);

            // Y
            Color(gl, System.Drawing.Color.Green);
            gl.Vertex(0, lenght, 0);
            if (bothDirections)
                gl.Vertex(0, -lenght, 0);
            else
                gl.Vertex(0, 0, 0);

            // Z
            Color(gl, System.Drawing.Color.Blue);
            gl.Vertex(0, 0, lenght);
            if (bothDirections)
                gl.Vertex(0, 0, -lenght);
            else
                gl.Vertex(0, 0, 0);
            gl.End();
        }

        public static void DrawParallelepiped(this OpenGL gl, Vector3 edges, bool withEdges = false)
        {
            // грани
            gl.Draw(BeginMode.Quads, () =>
            {
                /*задняя*/
                gl.Vertex(0, 0, 0);
                gl.Vertex(edges.X, 0, 0);
                gl.Vertex(edges.X, edges.Y, 0);
                gl.Vertex(0, edges.Y, 0);

                /*левая*/
                gl.Vertex(0, 0, 0);
                gl.Vertex(0, 0, edges.Z);
                gl.Vertex(0, edges.Y, edges.Z);
                gl.Vertex(0, edges.Y, 0);

                /*нижняя*/
                gl.Vertex(0, 0, 0);
                gl.Vertex(0, 0, edges.Z);
                gl.Vertex(edges.X, 0, edges.Z);
                gl.Vertex(edges.X, 0, 0);

                /*верхняя*/
                gl.Vertex(0, edges.Y, 0);
                gl.Vertex(0, edges.Y, edges.Z);
                gl.Vertex(edges.X, edges.Y, edges.Z);
                gl.Vertex(edges.X, edges.Y, 0);

                /*передняя*/
                gl.Vertex(0, 0, edges.Z);
                gl.Vertex(edges.X, 0, edges.Z);
                gl.Vertex(edges.X, edges.Y, edges.Z);
                gl.Vertex(0, edges.Y, edges.Z);

                /*правая*/
                gl.Vertex(edges.X, 0, 0);
                gl.Vertex(edges.X, 0, edges.Z);
                gl.Vertex(edges.X, edges.Y, edges.Z);
                gl.Vertex(edges.X, edges.Y, 0);
            }, !withEdges);
        }

        public static void DrawCylinder(
            this OpenGL gl,
            float radius,
            float height,
            bool covered = false,
            int partsCount = 50,
            bool withEdges = false)
        {
            var center = PointF.Empty;
            var contourPoints = GetRegularPolygonPoints(partsCount, center, radius).ToArray();

            // Стенки
            gl.Draw(BeginMode.Quads, () =>
            {
                foreach (var (point1, point2) in contourPoints.Pairs())
                {
                    gl.Vertex(point1.X, point1.Y, 0);
                    gl.Vertex(point1.X, point1.Y, height);
                    gl.Vertex(point2.X, point2.Y, height);
                    gl.Vertex(point2.X, point2.Y, 0);
                }
            }, !withEdges);

            if (!covered)
                return;

            // Крышки
            for (var y = 0f; y <= radius; y += radius)
            {
                gl.Begin(BeginMode.Polygon);
                foreach (var point in contourPoints)
                    gl.Vertex(point.X, y, point.Y);
                gl.End();
            }
        }

        public static void DrawCircle(this OpenGL gl, float radius, int partsCount = 10)
        {
            var points = GetRegularPolygonPoints(partsCount, PointF.Empty, radius);

            gl.Draw(BeginMode.Polygon, () =>
            {
                foreach (var point in points)
                    gl.Vertex(point.X, point.Y);
            }, true);

            gl.Color(System.Drawing.Color.Black);
            gl.Draw(BeginMode.LineLoop, () =>
            {
                foreach (var point in points)
                    gl.Vertex(point.X, point.Y);
            }, true);
            gl.Color(currentColor);
        }

        public static IEnumerable<PointF> GetRegularPolygonPoints(int pointsCount, PointF center, float radius)
        {
            var angle = Math.PI * 2 / pointsCount;

            if (pointsCount <= 0)
                return Array.Empty<PointF>();

            return Enumerable.Range(0, pointsCount + 1)
                .Select(i =>
                {
                    var offsetX = (float) Math.Sin(i * angle) * radius;
                    var offsetY = (float) Math.Cos(i * angle);
                    var offset = new SizeF(offsetX, offsetY * radius);
                    return PointF.Add(center, offset);
                });
        }

        public static void Repeat(
            this OpenGL gl,
            Action action,
            int count,
            Vector3 posDiff,
            Vector3 rotationDiff,
            bool saveTransform = false)
        {
            for (var i = 0; i < count; i++)
            {
                action();
                gl.DoTranslate(posDiff);
                gl.Rotate(rotationDiff);
            }

            if (saveTransform)
                return;

            for (var i = 0; i < count; i++)
            {
                gl.Rotate(-rotationDiff);
                gl.DoTranslate(-posDiff);
            }
        }

        public static void SetColor(this OpenGL gl, Color color, bool forEdges = false)
        {
            if (forEdges)
            {
                edgesColor = color;
                return;
            }

            currentColor = color;
            Color(gl, color);
        }

        public static void SetColor(this OpenGL gl, int r, int g, int b, bool forEdges = false)
        {
            gl.SetColor(System.Drawing.Color.FromArgb(1, r, g, b), forEdges);
        }

        public static void Draw(this OpenGL gl, BeginMode beginMode, Action vertex, bool edgeless = false)
        {
            gl.Begin(beginMode);
            vertex();
            gl.End();

            if (edgeless)
                return;

            Color(gl, edgesColor);
            gl.Begin(BeginMode.LineLoop);
            vertex();
            gl.End();
            Color(gl, currentColor);
        }

        public static void Draw(this OpenGL gl, BeginMode beginMode, Action vertex, Vector3 posDiff,
            Vector3 rotationDiff)
        {
            gl.DoTranslate(posDiff);
            gl.Rotate(rotationDiff);
            gl.Draw(beginMode, vertex);
            gl.Rotate(-rotationDiff);
            gl.DoTranslate(-posDiff);
        }

        public static void UndoTranslation(this OpenGL gl, int count = 1)
        {
            var totalTranslationsCount = translations.Count;
            for (var i = 0; i < count && i <= totalTranslationsCount; i++)
                gl.DoTranslate(-translations.Pop());
        }

        public static void ResetTranslations(this OpenGL gl)
        {
            UndoTranslation(gl, translations.Count);
        }

        public static void UndoRotation(this OpenGL gl, int count = 1)
        {
            var totalRotationsCount = rotations.Count;
            for (var i = 0; i < count && i <= totalRotationsCount; i++)
                gl.Rotate(-rotations.Pop());
        }

        public static void ResetHistory(this OpenGL gl)
        {
            translations = new Stack<Vector3>();
            rotations = new Stack<Vector3>();
        }

        public static void DrawDisk(
            this OpenGL gl,
            float innerRadius,
            float outerRadius,
            int partsCount = 10)
        {
            var outerPoints = GetRegularPolygonPoints(partsCount, PointF.Empty, outerRadius);
            var innerPoints = GetRegularPolygonPoints(partsCount, PointF.Empty, innerRadius);
            var points = innerPoints.Zip(outerPoints, (p1, p2) => (p1, p2)).Pairs().ToArray();

            foreach (var ((innerPoint1, innerPoint2), (outerPoint1, outerPoint2)) in points)
            {
                gl.Draw(BeginMode.Quads, () =>
                {
                    gl.Vertex(innerPoint1.X, innerPoint1.Y, 0);
                    gl.Vertex(outerPoint1.X, outerPoint1.Y, 0);
                    gl.Vertex(outerPoint2.X, outerPoint2.Y, 0);
                    gl.Vertex(innerPoint2.X, innerPoint2.Y, 0);
                }, true);
            }

            gl.Color(System.Drawing.Color.Black);
            foreach (var ((innerPoint1, innerPoint2), (outerPoint1, outerPoint2)) in points)
            {
                gl.Draw(BeginMode.Lines, () =>
                {
                    gl.Vertex(innerPoint1.X, innerPoint1.Y);
                    gl.Vertex(outerPoint1.X, outerPoint1.Y);
                    gl.Vertex(innerPoint2.X, innerPoint2.Y);
                    gl.Vertex(outerPoint2.X, outerPoint2.Y);
                }, true);
            }

            gl.Color(currentColor);
        }
    }
}