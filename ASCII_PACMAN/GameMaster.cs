using System.Collections.Generic;
using ASCII_PACMAN.Entities;

namespace ASCII_PACMAN
{
    public delegate void GameEvent();

    /// <summary>
    /// The GameMaster class contains all the entities and data
    /// needed for the game to be played.
    /// </summary>
    class GameMaster
    {
        #region Variables

        public GridNode[,] Grid { get; private set; }
        public List<Entity> Entities { get; private set; }
        public List<Entity> Enemies { get; private set; }
        public Player ThePlayer { get; private set; }
        public Counter TheCounter { get; private set; }
        public int Score { get; private set; }
        public int Collectibles { get; private set; }
        private int _collected;
        private event GameEvent _wonGame;
        private event GameEvent _lostGame;
        private event GameEvent _powerUpPickUp;
        private event GameEvent _powerUpEnd;
        
        #endregion Variables

        #region Constructor

        public GameMaster()
        {
            Grid = new GridNode[49, 27];

            Entities = new List<Entity>();
            Enemies = new List<Entity>();
            ThePlayer = new Player(this, 0, 0);
            TheCounter = new Counter(this, -1, -1);
            Score = 0;
            Collectibles = 0;
            _collected = 0;
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Adds an Entity to the Entities List
        /// </summary>
        /// <param name="entity"></param>
        public void AddEntity(Entity entity, bool collectible = false, bool wall = false)
        {
            if (collectible)
                Collectibles++;

            Entities.Add(entity);

            Grid[entity.X, entity.Y] = new GridNode(entity.X, entity.Y, entity, wall);
        }

        /// <summary>
        /// Adds an Entity to the Enemy list
        /// </summary>
        /// <param name="entity"></param>
        public void AddEnemy(Entity entity)
        {
            Enemies.Add(entity);
        }

        /// <summary>
        /// Clears out Entities.
        /// </summary>
        public void ClearEntities()
        {
            Collectibles = 0;
            _collected = 0;
            Entities.Clear();
            Enemies.Clear();
        }

        /// <summary>
        /// Adds to the game score
        /// </summary>
        /// <param name="gain"></param>
        public void AddScore(int gain)
        {
            Score += gain;
        }

        /// <summary>
        /// Sets the score variable to zero
        /// </summary>
        public void ResetScore()
        {
            Score = 0;
        }

        /// <summary>
        /// Increments collection and
        /// invokes the win game event if
        /// all colletables are collected
        /// </summary>
        public void IncrememtCollection()
        {
            _collected++;

            if (_collected >= Collectibles)
                _wonGame.Invoke();
        }

        /// <summary>
        /// Called when player loses a life
        /// invokes an event depending on
        /// amount of lives left
        /// </summary>
        public void PlayerCaught()
        {
            _lostGame.Invoke();
        }

        /// <summary>
        /// Called when player has obtain
        /// a power up
        /// </summary>
        public void PowerUpEvent()
        {
            _powerUpPickUp.Invoke();
            TheCounter.StartPowerUp();
        }

        /// <summary>
        /// Enemies subscribe to the power up event 
        /// so that they are nerfed during that duration
        /// </summary>
        public void SubPowerUp(GameEvent gameEvent)
        {
            _powerUpPickUp += gameEvent;
        }

        /// <summary>
        /// adds a method to the lost game event
        /// </summary>
        /// <param name="gameEvent"></param>
        public void SubLostGame(GameEvent gameEvent)
        {
            _lostGame += gameEvent;
        }

        /// <summary>
        /// adds a method to the power up end event
        /// </summary>
        /// <param name="gameEvent"></param>
        public void SubPowerUpEnd(GameEvent gameEvent)
        {
            _powerUpEnd += gameEvent;
        }

        /// <summary>
        /// adds a method to the win game event
        /// </summary>
        /// <param name="gameEvent"></param>
        public void SubWinGame(GameEvent gameEvent)
        {
            _wonGame += gameEvent;
        }

        /// <summary>
        /// invokes power up end event
        /// </summary>
        public void PowerUpEnd()
        {
            _powerUpEnd.Invoke();
        }

        /// <summary>
        /// checks a grid node to see if it's null
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsNodeNull(int x, int y)
        {
            if (Grid[x, y] == null)
                return true;
            return false;
        }

        #endregion Public Methods
    }
}
