using System;
using System.Collections.Generic;
using System.Text;

namespace CellularAutomatonLibrary
{
    public struct Cell
    {
        public bool Alive;

        public Cell(bool alive)
        {
            Alive = alive;
        }
    }
}
