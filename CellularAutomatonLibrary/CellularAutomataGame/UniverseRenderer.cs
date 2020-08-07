using CellularAutomatonLibrary;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CellularAutomataGame
{
    public class UniverseRenderer
    {
        private Universe _universe;
        private bool _evolving;

        public UniverseRenderer(Universe universe)
        {
            _universe = universe;
        }

        public void LoadContent()
        {
            //TOOD: load some sprites to show live and dead cells
        }

        public void Update(GameTime gameTime)
        {
            //TODO: based on some time period, only evolve when it's time to
            if(_evolving)
            {
                _universe.Evolve();
            }
        }

        public void Draw(GameTime gameTime)
        {
            //TODO: draw the grid
        }
    }
}
