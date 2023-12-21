using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Ecosystem_Simulator.Animals;
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
            return new Cell(AnimalInCell, PlantInCell);
        }
    }
}
