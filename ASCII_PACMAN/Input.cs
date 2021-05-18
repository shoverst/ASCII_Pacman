using System;
using System.Threading;

namespace ASCII_PACMAN
{
    /// <summary>
    /// class to keep track of user inputs.
    /// Meant to operate on seperate thread.
    /// </summary>
    static class Input
    {
        public static ConsoleKeyInfo Buffer { get; private set;}
        private static bool _running = false;
        private static Thread _inputThread = new Thread(InputLoop);

        /// <summary>
        /// Method that starts the input thread
        /// </summary>
        public static void StartThread()
        {
            if (!_running)
            {
                _running = true;
                _inputThread.Start();
                _inputThread.IsBackground = true;
            }
        }

        /// <summary>
        /// Method that loops and keeps track of keyboard input
        /// </summary>
        private static void InputLoop()
        {
            while (true)
            {
                Buffer = Console.ReadKey(true);
            }
        }
    }
}
