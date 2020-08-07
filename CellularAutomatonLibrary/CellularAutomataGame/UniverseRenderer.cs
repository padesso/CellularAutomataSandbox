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

        private double _evolveTime = 1000; //Time between evolutions in ms
        private double _lastEvolutionTime = 0;

        public UniverseRenderer(Universe universe)
        {
            _universe = universe;
        }

        public void Initialize()
        {
            //TODO: control this some other way
            _evolving = true;
        }

        public void LoadContent()
        {
            //TOOD: load some sprites to show live and dead cells
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.ElapsedGameTime.TotalMilliseconds - _lastEvolutionTime < _evolveTime)
            {
                return;
            }

            if(_evolving)
            {
                _universe.Evolve();

                _lastEvolutionTime = gameTime.ElapsedGameTime.Milliseconds;
            }
        }

        public void Draw(GameTime gameTime)
        {
            //TODO: draw the grid
        }
    }
}
