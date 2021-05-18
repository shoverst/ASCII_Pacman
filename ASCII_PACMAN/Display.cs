using System;
using System.Collections.Generic;

namespace ASCII_PACMAN
{
    /// <summary>
    /// Display is a static class containing all the methods 
    /// for the console display of the game.
    /// </summary>
    static class Display
    {
        // constants to help with displaying characters
        private const int TOP_OFFSET = 4;
        private const int LEFT_OFFSET = 2;

        #region Public Methods

        /// <summary>
        /// Method to print the menu to the console
        /// </summary>
        public static void Menu()
        {
            Console.Clear();
            Console.CursorTop = (Console.WindowHeight - 20) / 2;

            MenuHelper("@@@@@ @@@ @    @   @   @    ");
            MenuHelper("@      @  @@   @  @ @  @    ");
            MenuHelper("@@@    @  @ @  @ @   @ @    ");
            MenuHelper("@      @  @  @ @ @@@@@ @    ");
            MenuHelper("@      @  @   @@ @   @ @    ");
            MenuHelper("@     @@@ @    @ @   @ @@@@@");
            Console.WriteLine();
            MenuHelper(" @@@  @@@@   @@@     @ @@@  @@  @@@@@");
            MenuHelper("@   @ @   @ @   @    @ @   @  @   @  ");
            MenuHelper("@@@@  @@@@  @   @    @ @@  @      @  ");
            MenuHelper("@     @ @   @   @    @ @@  @      @  ");
            MenuHelper("@     @  @  @   @ @  @ @   @  @   @  ");
            MenuHelper("@     @   @  @@@   @@  @@@  @@    @  ");
            Console.WriteLine();

            MenuHelper("-----------------");
            MenuHelper("Play Game");
            MenuHelper("-Press 1-");
            MenuHelper("How To Play");
            MenuHelper("-Press 2-");
            MenuHelper("Exit Game");
            MenuHelper("-Press 3-");
        }

        /// <summary>
        /// Displays instructions to how to play the game
        /// </summary>
        public static void HowToPlay()
        {
            Console.Clear();
            Console.CursorTop = (Console.WindowHeight - 8) / 2;

            MenuHelper("- HOW TO PLAY -");
            MenuHelper("GOAL: Collect all * to win");
            MenuHelper("Avoid enemies 'M', make contact and you'll lose");
            MenuHelper("- CONTROLS  -");
            MenuHelper("Move Up : Up Arrow | W");
            MenuHelper("Move Down : Down Arrow | S");
            MenuHelper("Move Left: Left Arrow | A");
            MenuHelper("Move Right: Right Arrow | D");
            MenuHelper("Stop moving : Space bar");
            Console.CursorTop++;
            MenuHelper("Exit to menu : press Escape");
        }

        /// <summary>
        /// Method that prompts the player
        /// </summary>
        public static void ContinuePrompt()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Util.SetConsoleCursor(Console.WindowWidth / 4, Console.WindowHeight - 2);

            Console.Write("Continue Playing?\tY) Yes\tN)No");
        }

        /// <summary>
        /// Method to print the initial frame
        /// </summary>
        public static void InitialFrame(GameMaster gameMaster,
            List<Entity> walls, List<Entity> others)
        {
            Header(gameMaster);

            // print walls
            Console.ForegroundColor = walls[0].Color;
            for (int i = 0; i < walls.Count; i++)
            {
                Util.SetConsoleCursor(walls[i].X + LEFT_OFFSET, walls[i].Y + TOP_OFFSET);
                Console.Write(walls[i].DefaultChar);
            }

            // every other entity
            for (int i = 0; i < others.Count; i++)
            {
                Console.ForegroundColor = others[i].Color;
                Util.SetConsoleCursor(others[i].X + LEFT_OFFSET, others[i].Y + TOP_OFFSET);
                Console.Write(others[i].DefaultChar);
            }

            // Print player
            Console.ForegroundColor = gameMaster.ThePlayer.Color;
            Util.SetConsoleCursor(gameMaster.ThePlayer.X + LEFT_OFFSET, gameMaster.ThePlayer.Y + TOP_OFFSET);
            Console.Write(gameMaster.ThePlayer.DefaultChar);

            // this is here to help with a weird bug
            Console.CursorLeft = 0;
        }

        /// <summary>
        /// Method to refresh the frames
        /// </summary>
        /// <param name="gameMaster"></param>
        public static void RefreshFrame(GameMaster gameMaster)
        {
            Header(gameMaster);

            // print enemies
            for (int i = 0; i < gameMaster.Enemies.Count; i++)
            {
                RefreshPrevSpot(gameMaster, gameMaster.Enemies[i]);

                Console.ForegroundColor = gameMaster.Enemies[i].Color;
                Util.SetConsoleCursor(gameMaster.Enemies[i].X + LEFT_OFFSET, gameMaster.Enemies[i].Y + TOP_OFFSET);
                Console.Write(gameMaster.Enemies[i].DefaultChar);
            }

            // print player
            RefreshPrevSpot(gameMaster, gameMaster.ThePlayer);

            Console.ForegroundColor = gameMaster.ThePlayer.Color;
            Util.SetConsoleCursor(gameMaster.ThePlayer.X + LEFT_OFFSET, gameMaster.ThePlayer.Y + TOP_OFFSET);
            Console.Write(gameMaster.ThePlayer.DefaultChar);

            // this is here to help with a weird bug
            Console.CursorLeft = 0;
        }

        /// <summary>
        /// Prints a message to the center of the screen
        /// </summary>
        /// <param name="message"></param>
        public static void GameMessage(string message)
        {
            int leftPos = (Console.WindowWidth - message.Length) / 2;
            Console.CursorTop = (Console.WindowHeight - 5) / 2;
            Console.CursorLeft = leftPos;

            for (int i = 0; i < message.Length + 2; i++)
                Console.Write("-");

            Util.SetConsoleCursor(leftPos, Console.CursorTop + 1);
            Console.Write("|");
            for (int i = 0; i < message.Length; i++)
                Console.Write(" ");
            Console.Write("|");

            Util.SetConsoleCursor(leftPos, Console.CursorTop + 1);
            Console.Write("|" + message + "|");

            Util.SetConsoleCursor(leftPos, Console.CursorTop + 1);
            Console.Write("|");
            for (int i = 0; i < message.Length; i++)
                Console.Write(" ");
            Console.Write("|");

            Util.SetConsoleCursor(leftPos, Console.CursorTop + 1);
            for (int i = 0; i < message.Length + 2; i++)
                Console.Write("-");
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Prints header of the game
        /// </summary>
        /// <param name="gameMaster"></param>
        private static void Header(GameMaster gameMaster)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Util.SetConsoleCursor();

            Util.SetConsoleCursor(Console.WindowWidth / 10, 1);
            Console.Write("Score : {0}", gameMaster.Score);

            Util.SetConsoleCursor(0, 2);
            for (int i = 0; i < Console.WindowWidth - 1; i++)
                Console.Write("_");
        }

        /// <summary>
        /// Method that supports the menu method
        /// by reducing the amount of lines in the 
        /// code
        /// </summary>
        /// <param name="message"></param>
        /// <param name="hover"></param>
        private static void MenuHelper(string message, bool increment = true)
        {
            Console.CursorLeft = (Console.WindowWidth - message.Length) / 2;
            Console.Write(message);
            if (increment)
                Console.CursorTop++;
        }

        /// <summary>
        /// Helps refresh frame lower the amount of lines of code
        /// Refreshes the position that moving entities were on previously
        /// </summary>
        /// <param name="gameMaster"></param>
        /// <param name="e"></param>
        private static void RefreshPrevSpot(GameMaster gameMaster, Entity e)
        {
            Util.SetConsoleCursor(e.PrevX + LEFT_OFFSET, e.PrevY + TOP_OFFSET);
            if (!gameMaster.IsNodeNull(e.PrevX, e.PrevY) && gameMaster.Grid[e.PrevX, e.PrevY].Entity.Active)
            {
                Console.ForegroundColor = gameMaster.Grid[e.PrevX, e.PrevY].Entity.Color;
                Console.Write(gameMaster.Grid[e.PrevX, e.PrevY].Entity.DefaultChar);
            }
            else
                Console.Write(" ");
        }

        #endregion Private Methods
    }
}
