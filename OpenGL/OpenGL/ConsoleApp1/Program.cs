using System;
using OpenTK;
using OpenTK.Graphics;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(800, 600, "LearnOpenTK"))
            {
                //Run takes a double, which is how many frames per second it should strive to reach.
                //You can leave that out and it'll just update as fast as the hardware will allow it.
                game.Run(60.0);
            }
        }
    }

    public class Game : GameWindow
    {
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {
        }
    }
}