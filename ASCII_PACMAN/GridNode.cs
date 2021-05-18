using System;
using System.Collections.Generic;

namespace ASCII_PACMAN
{
    /// <summary>
    /// Grid node that contains all the entities and their positions
    /// </summary>
    class GridNode
    {
        public Entity Entity { get; protected set; }
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public bool Wall { get; protected set; }

        public GridNode(int x, int y, Entity entity, bool wall = false)
        {
            Entity = entity;
            X = x;
            Y = y;
            Wall = wall;
        }
    }
}
