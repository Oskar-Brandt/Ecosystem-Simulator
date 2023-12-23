using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Ecosystem_Simulator.Interfaces;
using Ecosystem_Simulator.Plants;

namespace Ecosystem_Simulator.Animals.SmallAnimals
{
    internal class Rabbit : SmallAnimal, IEatable
    {

        public override int MaxAge => 16;
        public override int MatureAge => 4;
        public override int MaxHunger => 6;
        public override int LitterSize => 3;
        public override int MaxPregnancyDuration => 2;
        public int NutritionalValue => 2;

        public Rabbit(int age) : base(age)
        {
        }

        public override bool isHungry(IEatable foodItem)
        {
            if (base.isHungry(foodItem))
            {
                if (foodItem.GetType() == typeof(Plant))
                {
                    return true;
                }
            }

            return false;
        }

        public override List<Animal> giveBirth()
        {
            List<Animal> animals = new List<Animal>();
            for (int i = 0; i < LitterSize; i++)
            {
                animals.Add(new Rabbit(i));
            }
            IsPregnant = false;
            PregnancyDurationCounter = 0;
            return animals;

        }
    }
}
