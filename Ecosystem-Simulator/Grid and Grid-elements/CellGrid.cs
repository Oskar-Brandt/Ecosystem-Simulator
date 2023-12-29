using Ecosystem_Simulator.Animals;
using Ecosystem_Simulator.Animals.MediumAnimals;
using Ecosystem_Simulator.Animals.SmallAnimals;
using Ecosystem_Simulator.Plants;
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
        public State currentState { get; set; }

        public CellGrid(int gridHeight, int gridWidth, int initialRabbit, int initialFoxes, int initialDandelions)
        {
            Cells = new Cell[gridHeight, gridWidth];
            StateChanger = new StateChanger();

            generateCells(Cells.GetLength(0), Cells.GetLength(1));

            InitState = setInitialState(initialRabbit, initialFoxes, initialDandelions);
            currentState = InitState;

        }

        
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

        
        private State setInitialState(int initialRabbits, int initialFoxes, int initialDandelions)
        {
            State initState = StateChanger.setInitialState(Cells, initialRabbits, initialFoxes, initialDandelions);
            
            return initState;
        }

        
        public State generateNextState(State currentState)
        {
            State nextState = StateChanger.generateNextState(currentState);

            currentState = nextState;
            Cells = currentState.Cells;
            return currentState;
        }

        public int getAnimalCount(Animal animal)
        {
            int animalCount = 0;

            Type animalType = animal.GetType();

            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    if (animalType.IsInstanceOfType(Cells[i, j].AnimalInCell))
                    {
                        animalCount++;
                    }
                }
            }

            return animalCount;
        }

        public int getPlantCount(Plant plant)
        {
            int plantCount = 0;

            Type plantType = plant.GetType();

            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    if (plantType.IsInstanceOfType(Cells[i, j].PlantInCell))
                    {
                        plantCount++;
                    }
                }
            }

            return plantCount;
        }

        
    }
}
