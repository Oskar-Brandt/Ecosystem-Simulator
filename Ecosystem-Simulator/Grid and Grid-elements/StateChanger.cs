﻿using Ecosystem_Simulator.Animals;
using Ecosystem_Simulator.Animals.MediumAnimals;
using Ecosystem_Simulator.Animals.SmallAnimals;
using Ecosystem_Simulator.Interfaces;
using Ecosystem_Simulator.Plants;
using System;
using System.Collections.Generic;
using System.Linq;
using static Ecosystem_Simulator.StateChanger;

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

        public State setInitialState(Cell[,] cells, int initialRabbits, int initialFoxes, int initialDandelions)
        {
            

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

            for (int i = 0; i < initialDandelions; i++)
            {
                Dandelion dandelion = new Dandelion();

                addPlantToCell(cells, dandelion);
            }
            State initState = new State(cells);

            return initState;
        }

        public State generateNextState(State currentState)
        {

            Cell[,] currentCells = currentState.Cells;

            List<Dictionary<NearbyCell, Cell>> rabbitTargets = new List<Dictionary<NearbyCell, Cell>>();
            List<Dictionary<NearbyCell, Cell>> foxTargets = new List<Dictionary<NearbyCell, Cell>>();
            List<Dictionary<NearbyCell, Cell>> plantTargets = new List<Dictionary<NearbyCell, Cell>>();

            int newRabbitCount = 0;
            int newFoxCount = 0;
            int newPlantCount = 0;

            for (int i = 0; i < currentCells.GetLength(0); i++)
            {
                for (int j = 0; j < currentCells.GetLength(1); j++)
                {
                    Cell currentCell = currentCells[i, j];
                    if (currentCell != null)
                    {
                        if (currentCell.AnimalInCell != null)
                        {
                            if (currentCell.AnimalInCell is Rabbit)
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

            foreach (Dictionary<NearbyCell, Cell> plantTarget in plantTargets)
            {
                plantTakesTurn(plantTarget);
            }

            foreach (Dictionary<NearbyCell, Cell> rabbitTarget in rabbitTargets)
            {
                animalTakesTurn(rabbitTarget);
            }

            foreach (Dictionary<NearbyCell, Cell> foxTarget in foxTargets)
            {
                animalTakesTurn(foxTarget);
            }

            return currentState;

        }

        private bool animalTakesTurn(Dictionary<NearbyCell, Cell> animalTarget)
        {
            Cell cell = animalTarget[NearbyCell.target];
            Animal animal = cell.AnimalInCell;
            if (animal == null)
            {
                return false;
            }

            animal.age();
            animal.hungrify();

            if (animal.IsDead)
            {
                cell.Remove(animal);
                return false;
            }
            else
            {
                if (nearbyCellContainsFood(animalTarget))
                {
                    letAnimalEat(animalTarget, animal);
                }

                if (nearbyCellContainsAnimal(animalTarget))
                {
                    letAnimalMate(animalTarget, animal);
                }

                if (animal.IsPregnant)
                {
                    if (animal.PregnancyDurationCounter == animal.MaxPregnancyDuration)
                    {
                        List<Animal> animalOffspring = animal.giveBirth();

                        foreach (Animal offSpring in animalOffspring)
                        {
                            addOffspring(offSpring, animalTarget);
                        }
                    }
                }

                letAnimalMove(animalTarget, animal);

                return true;
            }
        }

        private bool plantTakesTurn(Dictionary<NearbyCell, Cell> plantTarget)
        {
            Cell cell = plantTarget[NearbyCell.target];
            Plant plant = cell.PlantInCell;

            if (plant == null)
            {
                return false;
            }

            if (plant.IsDead)
            {
                cell.Remove(plant);
                return false;
            }
            else
            {
                return letPlantSpread(plantTarget, plant);
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

        private bool addOffspring(Animal offSpring, Dictionary<NearbyCell, Cell> nearbyCells)
        {
            foreach (Cell nearbyCell in nearbyCells.Values)
            {
                if(nearbyCell == null)
                {

                }
                else if (nearbyCell.AnimalInCell == null)
                {
                    nearbyCell.AnimalInCell = offSpring;
                    return true;
                }
            }
            return false;
        }

        private bool letPlantSpread(Dictionary<NearbyCell, Cell> nearbyCells, Plant spreadingPlant)
        {
            Plant newPlant = spreadingPlant.spread();
            if(newPlant != null){

                foreach (Cell nearbyCell in nearbyCells.Values)
                {
                    if (nearbyCell == null)
                    {

                    }
                    else if (nearbyCell.PlantInCell == null)
                    {
                        nearbyCell.PlantInCell = newPlant;
                        return true;
                    }
                }
            }

            return false;
        }

        public bool nearbyCellContainsAnimal(Dictionary<NearbyCell, Cell> cellDict)
        {
            bool isAnimalNear = false;

            foreach (Cell cell in cellDict.Values)
            {
                if (cell == null)
                {

                }
                else
                {
                    if (cell.AnimalInCell != null)
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

        private bool letAnimalMate(Dictionary<NearbyCell, Cell> cellDict, Animal animal)
        {
            if (animal.canMate())
            {
                foreach (Cell cell in cellDict.Values)
                {
                    if (cell == null)
                    {

                    }
                    else if (cell.AnimalInCell == null)
                    {

                    }
                    else
                    {
                        if (cell.AnimalInCell.Equals(animal))
                        {

                        }
                        else if (cell.AnimalInCell != null)
                        {
                            if (animal.GetType() == cell.AnimalInCell.GetType() && cell.AnimalInCell.canMate())
                            {
                                animal.mate(cell.AnimalInCell);
                                return true;
                            }

                        }

                    }
                }
            }

            return false;

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

        private bool letAnimalMove(Dictionary<NearbyCell, Cell> cellDict, Animal animal)
        {
            List<Cell> movableCells = new List<Cell>();
            foreach (Cell cell in cellDict.Values)
            {
                if (cell == null)
                {

                }
                else
                {
                    movableCells.Add(cell);
                }
                
            }

            Random rnd = new Random();

            List<Cell> randomizedList = movableCells.OrderBy(item => rnd.Next()).ToList();

            foreach (Cell cell in randomizedList)
            {
                if (cell.AnimalInCell == null)
                {
                    cellDict[NearbyCell.target].Remove(animal);
                    cell.AnimalInCell = animal;
                    return true;
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
                nearbyCells.TryAdd(NearbyCell.leftTopCorner, null);
                nearbyCells.TryAdd(NearbyCell.top, null);
                nearbyCells.TryAdd(NearbyCell.rightTopCorner, null);

            }
            else if (cellRow == cells.GetLength(0) - 1)
            {
                nearbyCells.TryAdd(NearbyCell.leftBotCorner, null);
                nearbyCells.TryAdd(NearbyCell.bot, null);
                nearbyCells.TryAdd(NearbyCell.rightBotCorner, null);
            }

            if (cellCol == 0)
            {
                nearbyCells.TryAdd(NearbyCell.leftTopCorner, null);
                nearbyCells.TryAdd(NearbyCell.left, null);
                nearbyCells.TryAdd(NearbyCell.leftBotCorner, null);
            }
            else if (cellCol == cells.GetLength(1) - 1)
            {
                nearbyCells.TryAdd(NearbyCell.rightTopCorner, null);
                nearbyCells.TryAdd(NearbyCell.right, null);
                nearbyCells.TryAdd(NearbyCell.rightBotCorner, null);
            }

            if (!nearbyCells.ContainsKey(NearbyCell.leftTopCorner))
            {
                nearbyCells.TryAdd(NearbyCell.leftTopCorner, cells[cellRow - 1, cellCol - 1]);
            }

            if (!nearbyCells.ContainsKey(NearbyCell.top))
            {
                nearbyCells.TryAdd(NearbyCell.top, cells[cellRow - 1, cellCol]);
            }

            if (!nearbyCells.ContainsKey(NearbyCell.rightTopCorner))
            {
                nearbyCells.TryAdd(NearbyCell.rightTopCorner, cells[cellRow - 1, cellCol + 1]);
            }

            if (!nearbyCells.ContainsKey(NearbyCell.left))
            {
                nearbyCells.TryAdd(NearbyCell.left, cells[cellRow, cellCol - 1]);
            }

            if (!nearbyCells.ContainsKey(NearbyCell.right))
            {
                nearbyCells.TryAdd(NearbyCell.right, cells[cellRow, cellCol + 1]);
            }

            if (!nearbyCells.ContainsKey(NearbyCell.leftBotCorner))
            {
                nearbyCells.TryAdd(NearbyCell.leftBotCorner, cells[cellRow + 1, cellCol - 1]);
            }

            if (!nearbyCells.ContainsKey(NearbyCell.bot))
            {
                nearbyCells.TryAdd(NearbyCell.bot, cells[cellRow + 1, cellCol]);
            }

            if (!nearbyCells.ContainsKey(NearbyCell.rightBotCorner))
            {
                nearbyCells.TryAdd(NearbyCell.rightBotCorner, cells[cellRow + 1, cellCol + 1]);
            }

            nearbyCells.TryAdd(NearbyCell.target, cells[cellRow, cellCol]);

            return nearbyCells;
        }
    }
}
