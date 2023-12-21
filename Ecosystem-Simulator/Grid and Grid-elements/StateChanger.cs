using Ecosystem_Simulator.Animals;
using Ecosystem_Simulator.Animals.MediumAnimals;
using Ecosystem_Simulator.Animals.SmallAnimals;
using Ecosystem_Simulator.Plants;
using System;
using System.Collections.Generic;

namespace Ecosystem_Simulator
{
    internal class StateChanger
    {
        public StateChanger()
        {

        }

        public State setInitialState(Cell[,] cells, int initialRabbits, int initialFoxes)
        {
            State initState = null;
            const int numOfDandelions = 10;

            for (int i = 0; i < initialRabbits; i++)
            {
                Rabbit rabbit = new Rabbit(4);

                addAnimalToCell(cells, rabbit);
            }

            for (int i = 0; i < initialFoxes; i++)
            {
                Fox fox = new Fox(2);

                addAnimalToCell(cells, fox);
            }

            for(int i = 0; i < numOfDandelions; i++)
            {
                Dandelion dandelion = new Dandelion();

                addPlantToCell(cells, dandelion);
            }

            return initState;
        }

        public void addAnimalToCell(Cell[,] cells, Animal animal)
        {

            Random rand = new Random();

            int row = rand.Next(0, cells.GetLength(0));
            int col = rand.Next(0, cells.GetLength(1));

            while (cells[row, col].AnimalInCell != null)
            {
                row = rand.Next(0, cells.GetLength(0));
                col = rand.Next(0, cells.GetLength(1));
            }
            cells[row, col].AnimalInCell = animal;
        }

        public void addPlantToCell(Cell[,] cells, Plant plant)
        {
            Random rand = new Random();

            int row = rand.Next(0, cells.GetLength(0));
            int col = rand.Next(0, cells.GetLength(1));

            // Something about checking the cell and cells around for animals.
            // If there are any, it won't be placed
            // COuld cause problems if too many animals, but reduces risk of quick or instant extinction for plant type

            while (cells[row, col].PlantInCell != null)
            {
                row = rand.Next(0, cells.GetLength(0));
                col = rand.Next(0, cells.GetLength(1));
            }

            cells[row, col].PlantInCell = plant;
        }

        public State generateNextState(State currentState)
        {
            State nextState = new State(currentState.Cells);

            Cell[,] currentCells = currentState.Cells;
            Cell[,] nextStateCells = nextState.Cells;




            for (int i = 0; i < currentCells.GetLength(0); i++)
            {
                for (int j = 0; j < currentCells.GetLength(1); j++)
                {
                    bool isCurrentCellActive = currentCells[i, j].Activated;
                    int activeNeighbours = 0;

                    if (i != 0)
                    {
                        if (currentCells[i - 1, j].Activated)
                        {
                            activeNeighbours++;
                        }

                        if (j != 0)
                        {
                            if (currentCells[i - 1, j - 1].Activated)
                            {
                                activeNeighbours++;
                            }
                        }

                        if (j != currentCells.GetLength(1) - 1)
                        {
                            if (currentCells[i - 1, j + 1].Activated)
                            {
                                activeNeighbours++;
                            }
                        }

                    }




                    if (i != currentCells.GetLength(0) - 1)
                    {
                        if (currentCells[i + 1, j].Activated)
                        {
                            activeNeighbours++;
                        }

                        if (j != 0)
                        {
                            if (currentCells[i + 1, j - 1].Activated)
                            {
                                activeNeighbours++;
                            }
                        }

                        if (j != currentCells.GetLength(1) - 1)
                        {
                            if (currentCells[i + 1, j + 1].Activated)
                            {
                                activeNeighbours++;
                            }
                        }
                    }


                    if (j != 0)
                    {
                        if (currentCells[i, j - 1].Activated)
                        {
                            activeNeighbours++;
                        }


                    }



                    if (j != currentCells.GetLength(1) - 1)
                    {
                        if (currentCells[i, j + 1].Activated)
                        {
                            activeNeighbours++;
                        }
                    }

                    bool currentCellNextState = patternChecker(isCurrentCellActive, activeNeighbours);

                    nextStateCells[i, j].Activated = currentCellNextState;
                }
            }

            return nextState;
        }

        public bool patternChecker(bool isCellActive, int activeNeighbours)
        {
            if (isCellActive)
            {
                if (activeNeighbours < 2)
                {
                    return false;
                }

                if (activeNeighbours <= 3)
                {
                    return true;
                }

                if (activeNeighbours > 3)
                {
                    return false;
                }
            }

            else if (activeNeighbours == 3)
            {
                return true;
            }

            return false;

        }
    }
}
