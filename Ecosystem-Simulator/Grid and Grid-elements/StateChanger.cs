using Ecosystem_Simulator.Animals;
using Ecosystem_Simulator.Animals.MediumAnimals;
using Ecosystem_Simulator.Animals.SmallAnimals;
using Ecosystem_Simulator.Interfaces;
using Ecosystem_Simulator.Plants;
using System;
using System.Collections.Generic;

namespace Ecosystem_Simulator
{
    internal class StateChanger
    {
        public enum NearbyCell
        {
            leftTopCorner,
            top,
            rightTopCorner,
            left,
            target,
            right,
            leftBotCorner,
            bot,
            rightBotCorner
            
        }
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

        public State generateNextState(State currentState)
        {
            State nextState = new State(currentState.Cells);

            Cell[,] currentCells = currentState.Cells;
            Cell[,] nextStateCells = nextState.Cells;

            List<Dictionary<NearbyCell, Cell>> rabbitTargets = new List<Dictionary<NearbyCell, Cell>>();
            List<Dictionary<NearbyCell, Cell>> foxTargets = new List<Dictionary<NearbyCell, Cell>>();
            List<Dictionary<NearbyCell, Cell>> plantTargets = new List<Dictionary<NearbyCell, Cell>>();

            int newRabbitCount = 0;
            int newFoxCount = 0;
            int newPlantCount = 0;

            for (int i = 0;i < currentCells.GetLength(0); i++)
            {
                for (int j = 0;j < currentCells.GetLength(1); j++)
                {
                    Cell currentCell = currentCells[i,j];
                    if (currentCell != null)
                    {
                        if (currentCell.AnimalInCell != null)
                        {
                            if(currentCell.AnimalInCell is Rabbit)
                            {
                                Dictionary<NearbyCell, Cell> rabbitTarget = getNearbyCells(currentCells, i, j);
                                rabbitTargets.Add(rabbitTarget);
                            }

                            if (currentCell.AnimalInCell is Fox)
                            {
                                Dictionary<NearbyCell, Cell> foxTarget = getNearbyCells(currentCells, i, j);
                                foxTargets.Add(foxTarget);
                            }
                        }

                        if (currentCell.PlantInCell != null)
                        {
                            Dictionary<NearbyCell, Cell> plantTarget = getNearbyCells(currentCells, i, j);
                            plantTargets.Add(plantTarget);
                        }
                    }
                }
            }

            //Using the above seems okay, BUT, the animal and plant in each cell may be removed by another animal, before its turn. This should be checked, probably in the "Thing"TakesTurn methods

            foreach(Dictionary<NearbyCell, Cell> plantTarget in plantTargets)
            {
                plantTakesTurn(plantTarget);
            }

            foreach(Dictionary<NearbyCell, Cell> rabbitTarget in rabbitTargets)
            {
                animalTakesTurn(rabbitTarget);
            }

            foreach(Dictionary<NearbyCell, Cell> foxTarget in foxTargets)
            {
                animalTakesTurn(foxTarget);
            }

            return nextState;

        }

        private bool animalTakesTurn(Dictionary<NearbyCell, Cell> animalTarget)
        {
            Cell cell = animalTarget[NearbyCell.target];
            Animal animal = cell.AnimalInCell;

            animal.age();
            animal.hungrify();

            if (animal.IsDead)
            {
                cell.Remove(animal);
                return false;
            }
            else
            {
                if(nearbyCellContainsFood(animalTarget))
                {
                    letAnimalEat(animalTarget, animal);
                }

                if(nearbyCellContainsAnimal(animalTarget))
                {
                    letAnimalMate(animalTarget, animal);
                }

                letAnimalMove(animalTarget, animal);

                return false;
            }
        }

        private bool plantTakesTurn(Dictionary<NearbyCell, Cell> plantTarget)
        {
            Cell cell = plantTarget[NearbyCell.target];
            Plant plant = cell.PlantInCell;

            if (plant.IsDead)
            {
                cell.Remove(plant);
                return false;
            }
            else
            {
                letPlantSpread(plantTarget, plant);
            }
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
            bool cellCanContainPlant = false;
            Random rand = new Random();
            int row;
            int col;
            
            while (!cellCanContainPlant)
            {
                row = rand.Next(0, cells.GetLength(0));
                col = rand.Next(0, cells.GetLength(1));
                if (cells[row, col].PlantInCell == null)
                {
                    Dictionary<NearbyCell, Cell> nearbyCells = getNearbyCells(cells, row, col);

                    if (!nearbyCellContainsAnimal(nearbyCells))
                    {
                        cells[row, col].PlantInCell = plant;
                        cellCanContainPlant = true;
                    }
                }
            }
        }

        public bool nearbyCellContainsAnimal(Dictionary<NearbyCell, Cell> cellDict)
        {
            bool isAnimalNear = false;

            foreach(Cell cell in cellDict.Values)
            {
                if(cell == null)
                {

                }
                else
                {
                    if(cell.AnimalInCell != null)
                    {
                        isAnimalNear = true;
                    }
                }
            }
            return isAnimalNear;
        }

        private bool nearbyCellContainsFood(Dictionary<NearbyCell, Cell> cellDict)
        {
            bool isFoodNear = false;

            foreach (Cell cell in cellDict.Values)
            {
                if (cell == null)
                {

                }
                else
                {
                    if (cell.hasFood())
                    {
                        isFoodNear = true;
                    }
                }
            }
            return isFoodNear;
        }

        private bool letAnimalEat(Dictionary<NearbyCell, Cell> cellDict, Animal animal)
        {
            foreach (Cell cell in cellDict.Values)
            {
                if (cell == null)
                {

                }
                else
                {
                    if (cell.AnimalInCell != null)
                    {
                        if (cell.AnimalInCell is IEatable)
                        {
                            if (animal.isHungry((IEatable)cell.AnimalInCell))
                            {
                                animal.eat((IEatable)cell.AnimalInCell);

                                cell.Remove(cell.AnimalInCell);
                                //TODO: IEatable object should die, or something, when eaten
                                return true;
                            }
                        }
                    }

                    if (cell.PlantInCell != null)
                    {
                        if (cell.PlantInCell is IEatable)
                        {
                            if (animal.isHungry((IEatable)cell.PlantInCell))
                            {
                                animal.eat((IEatable)cell.PlantInCell);
                                cell.Remove(cell.PlantInCell);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        

        //This method should be refactored at some point, as it's pretty messy to read.
        //Maybe could just not add the keys for null values to the dict??
        public Dictionary<NearbyCell, Cell> getNearbyCells(Cell[,] cells, int cellRow, int cellCol)
        {
            Dictionary<NearbyCell, Cell> nearbyCells = new Dictionary<NearbyCell, Cell>();


            if (cellRow == 0)
            {
                nearbyCells.Add(NearbyCell.leftTopCorner, null);
                nearbyCells.Add(NearbyCell.top, null);
                nearbyCells.Add(NearbyCell.rightTopCorner, null);

            }
            else if (cellRow == cells.GetLength(0) - 1)
            {
                nearbyCells.Add(NearbyCell.leftBotCorner, null);
                nearbyCells.Add(NearbyCell.bot, null);
                nearbyCells.Add(NearbyCell.rightBotCorner, null);
            }

            if (cellCol == 0)
            {
                nearbyCells.Add(NearbyCell.leftTopCorner, null);
                nearbyCells.Add(NearbyCell.left, null);
                nearbyCells.Add(NearbyCell.leftBotCorner, null);
            }
            else if (cellCol == cells.GetLength(1) - 1)
            {
                nearbyCells.Add(NearbyCell.rightTopCorner, null);
                nearbyCells.Add(NearbyCell.right, null);
                nearbyCells.Add(NearbyCell.rightBotCorner, null);
            }

            if (!nearbyCells.ContainsKey(NearbyCell.leftTopCorner))
            {
                nearbyCells.Add(NearbyCell.leftTopCorner, cells[cellRow - 1, cellCol - 1]);
            }

            if (!nearbyCells.ContainsKey(NearbyCell.top))
            {
                nearbyCells.Add(NearbyCell.leftTopCorner, cells[cellRow - 1, cellCol]);
            }

            if (!nearbyCells.ContainsKey(NearbyCell.rightTopCorner))
            {
                nearbyCells.Add(NearbyCell.rightTopCorner, cells[cellRow - 1, cellCol + 1]);
            }

            if (!nearbyCells.ContainsKey(NearbyCell.left))
            {
                nearbyCells.Add(NearbyCell.left, cells[cellRow, cellCol - 1]);
            }

            if (!nearbyCells.ContainsKey(NearbyCell.right))
            {
                nearbyCells.Add(NearbyCell.right, cells[cellRow, cellCol + 1]);
            }

            if (!nearbyCells.ContainsKey(NearbyCell.leftBotCorner))
            {
                nearbyCells.Add(NearbyCell.leftBotCorner, cells[cellRow + 1, cellCol - 1]);
            }

            if (!nearbyCells.ContainsKey(NearbyCell.bot))
            {
                nearbyCells.Add(NearbyCell.bot, cells[cellRow + 1, cellCol]);
            }

            if (!nearbyCells.ContainsKey(NearbyCell.rightBotCorner))
            {
                nearbyCells.Add(NearbyCell.rightBotCorner, cells[cellRow + 1, cellCol + 1]);
            }

            nearbyCells.Add(NearbyCell.target, cells[cellRow, cellCol]);

            return nearbyCells;
        }
    }
}
