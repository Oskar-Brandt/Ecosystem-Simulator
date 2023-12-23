using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Ecosystem_Simulator.Animals;
using Ecosystem_Simulator.Interfaces;
using Ecosystem_Simulator.Plants;

namespace Ecosystem_Simulator
{
    internal class Cell
    {
        public Animal AnimalInCell { get; set; }
        public Plant PlantInCell { get; set; }

        public Cell(Animal animal, Plant plant) {
            AnimalInCell = animal;
            PlantInCell = plant;
        }

        public bool hasFood()
        {
            if (AnimalInCell is IEatable)
            {
                return true;
            }
            else if (PlantInCell is IEatable)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Remove(Animal animal)
        {
            AnimalInCell = null;
        }

        public void Remove(Plant plant)
        {
            PlantInCell = null;
        }

        public Cell Copy()
        {
            //Copies the references, but the parameters still refer to the same objects!
            return new Cell(AnimalInCell, PlantInCell);
        }
    }
}
