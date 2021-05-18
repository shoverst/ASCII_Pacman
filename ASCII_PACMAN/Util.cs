using System;

namespace ASCII_PACMAN
{
    /// <summary>
    /// Utilities class for project
    /// </summary>
    static class Util
    {
        public const int WIDTH = 49;
        public const int HEIGHT = 27;

        /// <summary>
        /// Utility method to set the cursor position
        /// Helps make code less cluttered
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        public static void SetConsoleCursor(int left = 0, int top = 0)
        {
            Console.CursorTop = top;
            Console.CursorLeft = left;
        }
    }
}
