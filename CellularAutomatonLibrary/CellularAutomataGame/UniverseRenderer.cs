using CellularAutomatonLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CellularAutomataGame
{
    public class UniverseRenderer
    {
        private const int CELL_SIZE = 16;

        private Universe _universe;
        private bool _evolving;

        private double _evolveTime = 150; //Time between evolutions in ms
        private double _lastEvolutionTime = 0;

        Texture2D _aliveTexture;
        Texture2D _deadTexture;        

        KeyboardState previousKeyboardState;
        MouseState mouseState;

        public UniverseRenderer(Universe universe)
        {
            _universe = universe;
        }

        public void Initialize()
        {
            _evolving = false;

            //TEST DATA

            //Blinker
            _universe.SetCell(14, 15, true);
            _universe.SetCell(15, 15, true);
            _universe.SetCell(16, 15, true);

            //Glider
            _universe.SetCell(2, 2, true);
            _universe.SetCell(3, 3, true);
            _universe.SetCell(4, 3, true);
            _universe.SetCell(2, 4, true);
            _universe.SetCell(3, 4, true);
        }

        public void LoadContent(ContentManager content)
        {
            //load some sprites to show live and dead cells
            _aliveTexture = content.Load<Texture2D>("Images/greenDot");
            _deadTexture = content.Load<Texture2D>("Images/redDot");
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space))
            {
                _evolving = !_evolving; 
            }

            if (keyboardState.IsKeyDown(Keys.C) && !previousKeyboardState.IsKeyDown(Keys.C))
            {                
                Clear();
            }

            if (keyboardState.IsKeyDown(Keys.R) && !previousKeyboardState.IsKeyDown(Keys.R))
            {
                Randomize();
            }

            previousKeyboardState = keyboardState;

            mouseState = Mouse.GetState();
            if(mouseState.LeftButton == ButtonState.Pressed)
            {
                Cell pickedCell = _universe.GetCell(mouseState.X / CELL_SIZE, mouseState.Y / CELL_SIZE);
                _universe.SetCell(mouseState.X / CELL_SIZE, mouseState.Y / CELL_SIZE, true);
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                Cell pickedCell = _universe.GetCell(mouseState.X / CELL_SIZE, mouseState.Y / CELL_SIZE);
                _universe.SetCell(mouseState.X / CELL_SIZE, mouseState.Y / CELL_SIZE, false);
            }

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

        private void Randomize()
        {
            Random rand = new Random();
            for (int y = 0; y < _universe.Height; y++)
            {
                for (int x = 0; x < _universe.Width; x++)
                {
                    _universe.SetCell(x, y, Convert.ToBoolean(rand.Next(0,2)));
                }
            }
        }

        private void Clear()
        {
            for (int y = 0; y < _universe.Height; y++)
            {
                for (int x = 0; x < _universe.Width; x++)
                {
                    _universe.SetCell(x, y, false);
                }
            }
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            for (int y = 0; y < _universe.Height; y++)
            {
                for (int x = 0; x < _universe.Width; x++)
                {
                    Cell tempCell = _universe.GetCell(x, y);

                    if(tempCell.Alive)
                    {
                        spriteBatch.Draw(_aliveTexture, new Rectangle(CELL_SIZE * x, CELL_SIZE * y, CELL_SIZE, CELL_SIZE), Color.White);
                    }
                    else
                    {
                        //spriteBatch.Draw(_deadTexture, new Rectangle(CELL_SIZE * x, CELL_SIZE * y, CELL_SIZE, CELL_SIZE), new Color(Color.White, 0.15f));
                    }
                }
            }

            //draw a transparent tile where it would select
            spriteBatch.Draw(_aliveTexture, new Rectangle((int)Math.Round((double)(mouseState.X / CELL_SIZE)) * CELL_SIZE,
                (int)Math.Round((double)(mouseState.Y / CELL_SIZE)) * CELL_SIZE, 
                CELL_SIZE, 
                CELL_SIZE), new Color(Color.White, 0.35f));
            spriteBatch.End();
        }
    }
}
