using System;

namespace ASCII_PACMAN.Entities
{
    /// <summary>
    /// Collectibles are the goal of the game and increase the players score
    /// </summary>
    class Collectible : Entity
    {
        private const int BASE_SCORE = 10;

        public Collectible(GameMaster gameMaster, int x, int y) 
            : base(gameMaster, x, y, '*', ConsoleColor.White) { }

        public override void Update()
        {
            if (X == _gameMasterRef.ThePlayer.X && Y == _gameMasterRef.ThePlayer.Y)
            {
                _gameMasterRef.AddScore(BASE_SCORE);
                _gameMasterRef.IncrememtCollection();
                Active = false;
            }
        }
    }
}
