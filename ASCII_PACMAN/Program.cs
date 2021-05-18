using System;

namespace ASCII_PACMAN
{
    /// <summary>
    /// Class containing main
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.WindowHeight = 33;
            Console.WindowWidth = 53;
            Input.StartThread();
            Game game = new Game();
            game.GameMenu();
        }
    }
}
