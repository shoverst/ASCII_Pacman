using System;

namespace ASCII_PACMAN.Entities
{
    /// <summary>
    /// Power-Ups are entities that will empower the player if the
    /// player comes into contact with them
    /// </summary>
    class PowerUp : Entity
    {
        public PowerUp(GameMaster gameMaster, int x, int y) 
            : base(gameMaster, x, y, '0', ConsoleColor.DarkYellow)
        { }

        public override void Update()
        {

            if (Color == ConsoleColor.DarkYellow)
                Color = ConsoleColor.Black;
            else
                Color = ConsoleColor.DarkYellow;

            if (X == _gameMasterRef.ThePlayer.X && Y == _gameMasterRef.ThePlayer.Y)
            {
                Active = false;
                _gameMasterRef.PowerUpEvent();
            }
        }
    }

}
