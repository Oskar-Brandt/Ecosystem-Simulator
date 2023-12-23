using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecosystem_Simulator
{
    internal class CellGrid
    {
        public Cell[,] Cells { get; set; }
        public State InitState { get; set; }
        public StateChanger StateChanger { get; set; }

        public CellGrid(int gridHeight, int gridWidth, int initialRabbit, int initialFoxes)
        {
            Cells = new Cell[gridHeight, gridWidth];
            StateChanger = new StateChanger();

            generateCells(Cells.GetLength(0), Cells.GetLength(1));

            InitState = setInitialState(initialRabbit, initialFoxes);


        }

        //A method to call during construction
        //
        //Should initialize and construct all cells to be used in the grid.
        //Activation status of the cells will then be set some other place.
        private void generateCells(int gridHeight, int gridWidth)
        {
            for (int i = 0; i < gridHeight; i++)
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    Cells[i, j] = new Cell(null, null);
                }
            }
        }

        //A method to call during construction, after cells have been generated
        //
        //Sets the initial state of the grid, or rather, which cells start out being activated
        //May be determined randomly, though a number can be passed to the param to note how many cells should be activated
        private State setInitialState(int initialRabbits, int initialFoxes)
        {
            State initState;

            initState = StateChanger.setInitialState(Cells, initialRabbits, initialFoxes);
            
            return initState;
        }

        //A method for MainWindow to call to make the next state of the grid
        //
        //Should be called each time a new state is needed for the grid
        //Should check every cell, and change its activation status if needed
        //Should probably use CellGenerator (Or a new class), which then uses some kind of pattern (New class may be needed for this too)
        public State generateNextState(State currentState)
        {
            State nextState = StateChanger.generateNextState(currentState);

            return nextState;
        }

    }
}
