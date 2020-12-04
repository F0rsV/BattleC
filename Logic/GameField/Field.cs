﻿using Logic.Utils;

namespace Logic.GameField
{
    public class Field
    {
        public Cell[,] CellsMatrix { get; set; }

        public Field() { }

        public Field(int height, int width)
        {
            CellsMatrix = new Cell[height, width];

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    CellsMatrix[i, j] = new Cell(j, i);
                }
            }
        }
    }
}