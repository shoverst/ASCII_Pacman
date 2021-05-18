using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_PACMAN.Entities
{
    /// <summary>
    /// Player will be the entity controlled by the player
    /// </summary>
    class Player : Entity
    {
        private bool _poweredUp;
        private char[] otherChars = { 'v', '^', '>', '<' };


        public Player(GameMaster gameMaster, int x, int y) 
            : base(gameMaster, x, y, 'O', ConsoleColor.Yellow, true)
        {
            _poweredUp = false;
            _gameMasterRef.SubPowerUp(() => { if (!_poweredUp)
                                                _poweredUp = true; });
            _gameMasterRef.SubPowerUpEnd(() => { _poweredUp = false;
                Color = ConsoleColor.Yellow;
            });
        }

        public override void Update()
        {
            // power up active
            if (_poweredUp)
            {
                if (Color == ConsoleColor.Yellow)
                    Color = ConsoleColor.DarkYellow;
                else
                    Color = ConsoleColor.Yellow;
            }

            // check input buffer
            if (Input.Buffer.Key == ConsoleKey.UpArrow || Input.Buffer.KeyChar == 'w')
                Movement = Direction.UP;
            else if (Input.Buffer.Key == ConsoleKey.DownArrow || Input.Buffer.KeyChar == 's')
                Movement = Direction.DOWN;
            else if (Input.Buffer.Key == ConsoleKey.LeftArrow || Input.Buffer.KeyChar == 'a')
                Movement = Direction.LEFT;
            else if (Input.Buffer.Key == ConsoleKey.RightArrow || Input.Buffer.KeyChar == 'd')
                Movement = Direction.RIGHT;
            else if (Input.Buffer.Key == ConsoleKey.Spacebar)
                Movement = Direction.NO_DIR;

            base.Update();
        }

        public void NoDirection()
        {
            Movement = Direction.NO_DIR;
        }
    }
}
