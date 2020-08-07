using CellularAutomatonLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CellularAutomataGame
{
    public class UniverseRenderer
    {
        private Universe _universe;
        private bool _evolving;

        private double _evolveTime = 2000; //Time between evolutions in ms
        private double _lastEvolutionTime = 0;

        Texture2D _aliveTexture;
        Texture2D _deadTexture;

        public UniverseRenderer(Universe universe)
        {
            _universe = universe;
        }

        public void Initialize()
        {
            //TODO: control this some other way
            _evolving = true;

            //TEST DATA
            _universe.SetCell(2, 2, true);
            _universe.SetCell(3, 2, true);
            _universe.SetCell(4, 2, true);
        }

        public void LoadContent(ContentManager content)
        {
            //TOOD: load some sprites to show live and dead cells
            _aliveTexture = content.Load<Texture2D>("Images/greenDot");
            _deadTexture = content.Load<Texture2D>("Images/redDot");
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - _lastEvolutionTime < _evolveTime)
            {
                return;
            }

            if(_evolving)
            {
                _universe.Evolve();

                _lastEvolutionTime = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);

            for (int y = 0; y < _universe.Height; y++)
            {
                for (int x = 0; x < _universe.Width; x++)
                {
                    Cell tempCell = _universe.GetCell(x, y);

                    if(tempCell.Alive)
                    {
                        
                        //Just drawing the Sprite
                        spriteBatch.Begin();
                        spriteBatch.Draw(_aliveTexture, new Rectangle(30 * x, 30 * y, 30, 30), Color.White);
                        spriteBatch.End();
                    }
                    else
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(_deadTexture, new Rectangle(30 * x, 30 * y, 30, 30), Color.White);
                        spriteBatch.End();
                    }
                }
            }
        }
    }
}
