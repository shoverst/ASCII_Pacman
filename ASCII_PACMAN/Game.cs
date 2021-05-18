using System;
using System.Threading;
using System.Collections.Generic;

using ASCII_PACMAN.Entities;

namespace ASCII_PACMAN
{
    /// <summary>
    /// Class that plays the game
    /// </summary>
    class Game
    {
        public enum GameCondition { PLAYING, WIN, LOSS };
        private GameMaster gameMaster;
        private GameCondition gameCondition;
        private List<Entity> LevelWalls;
        private List<Entity> UpdateEntities;
        private string[] Level1 = { "#######################   #######################",
                                    "# 0 * * * * * * * * * #   # * * * * * * * * * 0 #",
                                    "# * ##### * ####### * #   # * ####### * ##### * #",
                                    "# * ##### * ####### * ##### * ####### * ##### * #",
                                    "# * * * * * * * * * * * * * * * * * * * * * * * #",
                                    "# * ##### * ### * ############# * ### * ##### * #",
                                    "# * * * * * # # * * * #   # * * * # # * * * * * #",
                                    "######### * #  ####   #   #   ####  # * #########",
                                    "        # * #  ####   #####   ####  # * #        ",
                                    "        # * # #                   # # * #        ",
                                    "######### * ###   #############   ### * #########",
                                    "# M       *       #           #       *       M #",
                                    "######### * ###   #           #   ### * #########",
                                    "        # * ###   #############   ### * #        ",
                                    "        # * ###                   ### * #        ",
                                    "######### * ###   #############   ### * #########",
                                    "# * * * * * * * * * * #   # * * * * * * * * * * #",
                                    "# * ##### * ####### * #   # * ####### * ##### * #",
                                    "# * ##### * ####### * ##### * ####### * ##### * #",
                                    "# 0 * * # * * * * * *   O   * * * * * * # * * 0 #",
                                    "##### * # * ### * ############# * ### * # * #####",
                                    "##### * # * ### * ############# * ### * # * #####",
                                    "# * * * * * ### * * * #   # * * * ### * * * * * #",
                                    "# * ############### * #   # * ############### * #",
                                    "# * ############### * ##### * ############### * #",
                                    "# * * * * * * * * * * * * * * * * * * * * * * * #",
                                    "#################################################" };

        public Game()
        {
            gameMaster = new GameMaster();
            LevelWalls = new List<Entity>();
            UpdateEntities = new List<Entity>();
            gameMaster.SubWinGame(() => { gameCondition = GameCondition.WIN; });
            gameMaster.SubLostGame(() => { gameCondition = GameCondition.LOSS; });
        }

        #region Public Method

        /// <summary>
        /// Menu of the game
        /// </summary>
        public void GameMenu()
        {
            int option = 0;
            bool running = true;

            while (running)
            {

                Display.Menu();

                while (option == 0)
                {
                    if (Input.Buffer.Key == ConsoleKey.D1)
                        option = 1;
                    else if (Input.Buffer.Key == ConsoleKey.D2)
                        option = 2;
                    else if (Input.Buffer.Key == ConsoleKey.D3)
                        option = 3;
                }

                if (option == 1)
                {
                    Console.Clear();
                    LoadLevel1();
                    Play();
                    option = 0;
                }
                else if (option == 2)
                {
                    Display.HowToPlay();
                    while (Input.Buffer.Key != ConsoleKey.Escape) ;
                    Console.Clear();
                    option = 0;
                }
                else
                    running = false;
            }
            Console.Clear();
        }

        #endregion Public Method

        #region Private Methods
        /// <summary>
        /// Method containing the game loop.
        /// </summary>
        private void Play()
        {
            bool keepPlaying = true;
            gameCondition = GameCondition.PLAYING;

            do
            {
                Display.InitialFrame(gameMaster, LevelWalls, UpdateEntities);

                while (gameCondition == GameCondition.PLAYING)
                {
                    Thread.Sleep(125);

                    gameMaster.TheCounter.Update();
                    gameMaster.ThePlayer.Update();

                    for (int i = 0; i < UpdateEntities.Count; i++)
                        if (UpdateEntities[i].Active)
                            UpdateEntities[i].Update();

                    for (int i = 0; i < gameMaster.Enemies.Count; i++)
                        gameMaster.Enemies[i].Update();

                    Display.RefreshFrame(gameMaster);
                }

                if (gameCondition == GameCondition.LOSS)
                {
                    Display.GameMessage("YOU LOST");
                    gameMaster.ResetScore();
                }
                else
                    Display.GameMessage("YOU WON");
                
                Display.ContinuePrompt();

                // loop till correct input is recieved
                while (Input.Buffer.Key != ConsoleKey.Y && Input.Buffer.Key != ConsoleKey.N) ;

                    if (Input.Buffer.Key == ConsoleKey.Y)
                        RestartGame();
                    else if (Input.Buffer.Key == ConsoleKey.N)
                        keepPlaying = false;

            } while (keepPlaying);

            GameMenu();
        }

        /// <summary>
        /// Restarts the game for the player
        /// </summary>
        private void RestartGame()
        {
            Console.Clear();
            gameMaster.ClearEntities();
            gameMaster.ThePlayer.NoDirection();
            gameMaster.TheCounter.RefreshPowerUp();
            LoadLevel1();
            gameCondition = GameCondition.PLAYING;
        }

        /// <summary>
        /// Loads level one based on the string array Level1
        /// </summary>
        private void LoadLevel1()
        {
            LevelWalls.Clear();
            UpdateEntities.Clear();

            // add all entities in the level to the gameMaster
            for (int i = 0; i < Level1.Length; i++)
            {
                for (int j = 0; j < Level1[i].Length; j++)
                {
                    if (Level1[i][j] == '#')
                        gameMaster.AddEntity(new Wall(gameMaster, j, i), false, true);
                    else if (Level1[i][j] == '*')
                        gameMaster.AddEntity(new Collectible(gameMaster, j, i), true);
                    else if (Level1[i][j] == '0')
                        gameMaster.AddEntity(new PowerUp(gameMaster, j, i));
                    else if (Level1[i][j] == 'M')
                        gameMaster.AddEnemy(new Enemy(gameMaster, j, i));
                    else if (Level1[i][j] == 'O')
                        gameMaster.ThePlayer.ChangePos(j, i);
                }
            }

            // find all walls and enities needed to update
            LevelWalls.Clear();
            UpdateEntities.Clear();
            for (int i = 0; i < Util.WIDTH; i++)
                for (int j = 0; j < Util.HEIGHT; j++)
                {
                    if (gameMaster.Grid[i, j] != null)
                        if (gameMaster.Grid[i, j].Wall)
                            LevelWalls.Add(gameMaster.Grid[i, j].Entity);
                        else
                            UpdateEntities.Add(gameMaster.Grid[i, j].Entity);
                        
                }
        }

        #endregion Private Methods

    }
}
