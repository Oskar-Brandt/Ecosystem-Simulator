using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecosystem_Simulator
{
    internal class State
    {
        public Cell[,] Cells { get; set; }

        public State(Cell[,] currentCells)
        {
            Cells = new Cell[currentCells.GetLength(0), currentCells.GetLength(1)];

            for (int i = 0; i < currentCells.GetLength(0); i++)
            {
                for (int j = 0; j < currentCells.GetLength(1); j++)
                {
                    Cells[i, j] = currentCells[i, j].Copy();
                }
            }
        }
    }
}
