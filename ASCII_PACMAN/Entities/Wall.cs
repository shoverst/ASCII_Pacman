using System;

namespace ASCII_PACMAN.Entities
{
    /// <summary>
    /// Wall will be a space that the player will not be able to move through
    /// and will primarly be used to create the play space
    /// </summary>
    class Wall : Entity
    {
        public Wall(GameMaster gameMaster, int x, int y) 
            : base(gameMaster, x, y, '#', ConsoleColor.Cyan, false) { }
    }
}
