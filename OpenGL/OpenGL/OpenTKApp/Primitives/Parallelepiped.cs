using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTKApp.Primitives
{
    public static class Parallelepiped
    {
        public static void Draw(Vector3 edges, Color color)
        {
            // грани
            GL.Color3(color);
            GL.Begin(PrimitiveType.Quads);

            /*задняя*/
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(edges.X, 0, 0);
            GL.Vertex3(edges.X, edges.Y, 0);
            GL.Vertex3(0, edges.Y, 0);

            /*левая*/
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, edges.Z);
            GL.Vertex3(0, edges.Y, edges.Z);
            GL.Vertex3(0, edges.Y, 0);

            /*нижняя*/
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, edges.Z);
            GL.Vertex3(0, 0, edges.Z);
            GL.Vertex3(edges.X, 0, 0);

            /*верхняя*/
            GL.Vertex3(0, edges.Y, 0);
            GL.Vertex3(0, edges.Y, edges.Z);
            GL.Vertex3(edges.X, edges.Y, edges.Z);
            GL.Vertex3(edges.X, edges.Y, 0);

            /*передняя*/
            GL.Vertex3(0, 0, edges.Z);
            GL.Vertex3(edges.X, 0, edges.Z);
            GL.Vertex3(edges.X, edges.Y, edges.Z);
            GL.Vertex3(0, edges.Y, edges.Z);

            /*правая*/
            GL.Vertex3(edges.X, 0, 0);
            GL.Vertex3(edges.X, 0, edges.Z);
            GL.Vertex3(edges.X, edges.Y, edges.Z);
            GL.Vertex3(edges.X, edges.Y, 0);
            GL.End();
        }
    }
}