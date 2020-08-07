using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CellularAutomatonLibrary
{
    public class Universe
    {
        private Cell[] _cellGrid;
        private Cell[] _tempGrid;
        private int _width;
        private int _height;

        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }

        public Universe(int width, int height)
        {
            Width = width;
            Height = height;

            _cellGrid = new Cell[width * height];
            _tempGrid = new Cell[width * height];
        }

        public Cell GetCell(int x, int y)
        {
            return _cellGrid[x + _width * y];
        }

        public void SetCell(int x, int y, bool alive)
        {
            _cellGrid[x + _width * y].Alive = alive;
        }

        private void SetTempCell(int x, int y, bool alive)
        {
            _tempGrid[x + _width * y].Alive = alive;
        }

        public void Evolve()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Cell currentCell = GetCell(x, y);
                    SetTempCell(x, y, currentCell.Alive); //sync the swap grid
                    List<Cell> currentNeighbors = GetNeighbors(x, y);
 
                    if (currentCell.Alive)
                    {
                        //Any live cell with fewer than two live neighbours dies, as if by underpopulation.
                        if (currentNeighbors.Where(n => n.Alive).Count() < 2)
                        {
                            SetTempCell(x, y, false);
                        }

                        //Any live cell with two or three live neighbours lives on to the next generation.
                        if (currentNeighbors.Where(n => n.Alive).Count() == 2 ||
                            currentNeighbors.Where(n => n.Alive).Count() == 3)
                        {
                            SetTempCell(x, y, true);
                        }

                        //Any live cell with more than three live neighbours dies, as if by overpopulation.
                        if (currentNeighbors.Where(n => n.Alive).Count() > 3)
                        {
                            SetTempCell(x, y, false);
                        }
                    }
                    else
                    {
                        //Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
                        if (currentNeighbors.Where(n => n.Alive).Count() == 3)
                        {
                            SetTempCell(x, y, true);
                        }
                    }
                }
            }

            _cellGrid = (Cell[])_tempGrid.Clone();
        }

        //Note, this only fires if we debug the tests
        public void Print()
        {  
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (x < Width - 1)
                    {
                        Debug.Write(Convert.ToInt32(GetCell(x, y).Alive) + " ");                        
                    }
                    else
                    {
                        Debug.WriteLine(Convert.ToInt32(GetCell(x, y).Alive));
                    }
                }
            }

            Debug.WriteLine("    ");
        }
        
        public List<Cell> GetNeighbors(int x, int y)
        {
            List<Cell> neighbors = new List<Cell>();

            if(x > 0 && x < _width - 1 && y > 0 && y < _height - 1) //non edge
            {
                neighbors.Add(GetCell(x - 1, y - 1));
                neighbors.Add(GetCell(x, y - 1));
                neighbors.Add(GetCell(x + 1, y - 1));
                neighbors.Add(GetCell(x - 1, y));
                neighbors.Add(GetCell(x + 1, y));
                neighbors.Add(GetCell(x - 1, y + 1));
                neighbors.Add(GetCell(x, y + 1));
                neighbors.Add(GetCell(x + 1, y + 1));

                return neighbors;
            }

            if(x == 0) //left edge
            {
                if (y == 0) //top left corner
                {
                    neighbors.Add(GetCell(x + 1, y));
                    neighbors.Add(GetCell(x + 1, y + 1));
                    neighbors.Add(GetCell(x, y + 1));
                }
                else if(y == _height - 1) // bottom left corner
                {
                    neighbors.Add(GetCell(x, y - 1));
                    neighbors.Add(GetCell(x + 1, y - 1));
                    neighbors.Add(GetCell(x + 1, y));
                }
                else //left edge, not corner
                {
                    neighbors.Add(GetCell(x, y - 1));
                    neighbors.Add(GetCell(x + 1, y - 1));
                    neighbors.Add(GetCell(x + 1, y));
                    neighbors.Add(GetCell(x + 1, y + 1));
                    neighbors.Add(GetCell(x, y + 1));
                }
            }

            if (x == _width - 1) //right edge
            {
                if (y == 0) //top right corner
                {
                    neighbors.Add(GetCell(x - 1, y));
                    neighbors.Add(GetCell(x - 1, y + 1));
                    neighbors.Add(GetCell(x, y + 1));
                }
                else if (y == _height - 1) // bottom right corner
                {
                    neighbors.Add(GetCell(x, y - 1));
                    neighbors.Add(GetCell(x - 1, y - 1));
                    neighbors.Add(GetCell(x - 1, y));
                }
                else //right edge, not corner
                {
                    neighbors.Add(GetCell(x, y - 1));
                    neighbors.Add(GetCell(x - 1, y - 1));
                    neighbors.Add(GetCell(x - 1, y));
                    neighbors.Add(GetCell(x - 1, y + 1));
                    neighbors.Add(GetCell(x, y + 1));
                }
            }

            if(y == 0) //top row
            {
                if(x > 0 && x < _width - 1) //skip corners, already got them above
                {
                    neighbors.Add(GetCell(x - 1, y));
                    neighbors.Add(GetCell(x - 1, y + 1));
                    neighbors.Add(GetCell(x, y + 1));
                    neighbors.Add(GetCell(x + 1, y + 1));
                    neighbors.Add(GetCell(x + 1, y));
                }
            }

            if (y == _height - 1) //bottom row
            {
                if (x > 0 && x < _width - 1) //skip corners, already got them above
                {
                    neighbors.Add(GetCell(x - 1, y));
                    neighbors.Add(GetCell(x - 1, y - 1));
                    neighbors.Add(GetCell(x, y - 1));
                    neighbors.Add(GetCell(x + 1, y - 1));
                    neighbors.Add(GetCell(x + 1, y));
                }
            }

            return neighbors;
        }
    }
}
