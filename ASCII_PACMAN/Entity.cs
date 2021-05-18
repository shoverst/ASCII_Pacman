using System;

namespace ASCII_PACMAN
{
    /// <summary>
    /// Entity is the base class for all entities in
    /// the game world.  Contains variables used by all 
    /// entities.
    /// </summary>
    abstract class Entity
    {
        public enum Direction { UP, DOWN, LEFT, RIGHT, NO_DIR };

        #region Variables

        public char DefaultChar { get; protected set; }
        public int X { get; protected set; }
        public int PrevX { get; protected set; }
        public int PrevY { get; protected set; }
        public int Y { get; protected set; }
        public bool Active { get; protected set; }
        public bool Moves { get; protected set; }
        public ConsoleColor Color { get; protected set; }
        public Direction Movement { get; protected set; }
        protected GameMaster _gameMasterRef;

        #endregion Variables

        #region Constructor

        public Entity(GameMaster gameMasterRef, int x = 0, int y = 0, char defaultChar = '!',
            ConsoleColor color = ConsoleColor.White, bool moves = false)
        {
            _gameMasterRef = gameMasterRef;
            X = x; Y = y;
            PrevX = X;
            PrevY = Y;
            DefaultChar = defaultChar;
            Moves = moves;
            Color = color;
            Movement = Direction.NO_DIR;
            Active = true;
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Updates properties of the entity
        /// </summary>
        public virtual void Update()
        {
            if (Moves && Movement != Direction.NO_DIR)
            {
                PrevY = Y;
                PrevX = X;

                if (Movement == Direction.UP && CanGoUp())
                {
                    Y--;
                }
                else if (Movement == Direction.DOWN && CanGoDown())
                {
                    Y++;
                }
                else if (Movement == Direction.RIGHT && CanGoRight())
                {
                    X += 2;
                }
                else if (Movement == Direction.LEFT && CanGoLeft())
                {
                    X -= 2;
                }
                else
                    Movement = Direction.NO_DIR;
            }
        }

        /// <summary>
        /// Changes the entities position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ChangePos(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Checks to see if moving entity can move up
        /// </summary>
        /// <returns></returns>
        protected bool CanGoUp()
        {
            if (_gameMasterRef.Grid[X, Y - 1] != null && _gameMasterRef.Grid[X, Y - 1].Wall)
                return false;

            return true;
        }

        /// <summary>
        /// Checks to see if moving entity can move down
        /// </summary>
        /// <returns></returns>
        protected bool CanGoDown()
        {
            if (_gameMasterRef.Grid[X, Y + 1] != null && _gameMasterRef.Grid[X, Y + 1].Wall)
                return false;

            return true;
        }

        /// <summary>
        /// Checks to see if moving entity can move left
        /// </summary>
        /// <returns></returns>
        protected bool CanGoLeft()
        {
            if (_gameMasterRef.Grid[X - 2, Y] != null && _gameMasterRef.Grid[X - 2, Y].Wall)
                return false;

            return true;
        }

        /// <summary>
        /// Checks to see if moving entity can move right
        /// </summary>
        /// <returns></returns>
        protected bool CanGoRight()
        {
            if (_gameMasterRef.Grid[X + 2, Y] != null && _gameMasterRef.Grid[X + 2, Y].Wall)
                return false;

            return true;
        }

        #endregion Private Methods
    }
}
