using System;

namespace ASCII_PACMAN.Entities
{
    /// <summary>
    /// Enemy will be the entities attempting to kill the player, depleting the 
    /// player's lives
    /// </summary>
    class Enemy : Entity
    {
        private Direction prevDir;
        private int spawnX, spawnY;
        private bool vulnerable;

        public Enemy(GameMaster gameMaster, int x, int y)
            : base(gameMaster, x, y, 'M', ConsoleColor.Magenta, true)
        {
            prevDir = Direction.NO_DIR;
            vulnerable = false;

            spawnX = x;
            spawnY = y;

            _gameMasterRef.SubPowerUp(() =>
            {
                vulnerable = true;
                Color = ConsoleColor.Blue;
            });
            _gameMasterRef.SubPowerUpEnd(() =>
            {
                vulnerable = false;
                Color = ConsoleColor.Magenta;
            });
        }

        public override void Update()
        {
            DecideMovement();

            // Contact with player
            if (X == _gameMasterRef.ThePlayer.X && Y == _gameMasterRef.ThePlayer.Y)
            {
                if (vulnerable)
                    Reset();
                else
                    _gameMasterRef.PlayerCaught();
            }

            base.Update();
        }

        /// <summary>
        /// Decides movement based on where the
        /// enemy can move and where it came from
        /// </summary>
        private void DecideMovement()
        {
            // random number genrator
            Random getrandom = new Random();
            bool[] canMove = { true, true, true, true };
            int options = 0;
            int rand = 0;

            if (Movement == Direction.NO_DIR)
            {

                if (prevDir == Direction.NO_DIR)
                {
                    if ( X > Util.WIDTH / 2)
                    {
                        Movement = Direction.LEFT;
                        prevDir = Direction.RIGHT;
                    }
                    else
                    {
                        Movement = Direction.RIGHT;
                        prevDir = Direction.LEFT;
                    }
                }
                else
                {
                    canMove[(int)prevDir] = false;

                    if (canMove[0])
                        canMove[0] = CanGoUp();
                    if (canMove[1])
                        canMove[1] = CanGoDown();
                    if (canMove[2])
                        canMove[2] = CanGoLeft();
                    if (canMove[3])
                        canMove[3] = CanGoRight();

                    for (int i = 0; i < canMove.Length; i++)
                        if (canMove[i])
                            options++;

                    rand = getrandom.Next(0, options);

                    for (int i = 0, j = 0; i < canMove.Length; i++)
                        if (canMove[i])
                        {
                            if (j == rand)
                            {
                                Movement = (Direction)i;
                                break;
                            }
                            else
                                j++;
                        }

                    if (Movement == Direction.UP)
                        prevDir = Direction.DOWN;
                    else if (Movement == Direction.DOWN)
                        prevDir = Direction.UP;
                    else if (Movement == Direction.LEFT)
                        prevDir = Direction.RIGHT;
                    else if (Movement == Direction.RIGHT)
                        prevDir = Direction.LEFT;
                }
            }
        }

        /// <summary>
        /// Resets the enemy's location to 
        /// it's original location
        /// </summary>
        private void Reset()
        {
            X = spawnX;
            Y = spawnY;
            Movement = Direction.NO_DIR;
        }
    }
}
