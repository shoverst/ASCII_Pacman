using System;

namespace ASCII_PACMAN.Entities
{
    /// <summary>
    /// Entity to assist the game master
    /// with task such as counting the frames
    /// during the power up event
    /// </summary>
    class Counter : Entity
    {
        private const int COUNTER_LIMIT = 40;
        private int _puCounter;
        private bool _puActive;

        public Counter(GameMaster gameMaster, int x, int y) 
            : base(gameMaster, x, y)
        {
            _puActive = false;
            _puCounter = 0;
        }

        public override void Update()
        {
            if (_puActive)
            {
                _puCounter++;

                if (_puCounter == COUNTER_LIMIT)
                {
                    _puActive = false;
                    _gameMasterRef.PowerUpEnd();
                }
            }
        }

        public void StartPowerUp()
        {
            _puActive = true;
            _puCounter = 0;
        }

        public void RefreshPowerUp()
        {
            _puActive = true;
            _puCounter = 0;
        }
    }
}
