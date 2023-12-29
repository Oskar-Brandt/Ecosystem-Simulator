using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecosystem_Simulator.Animals.SmallAnimals;
using Ecosystem_Simulator.Interfaces;

namespace Ecosystem_Simulator.Animals.MediumAnimals
{
    internal class Fox : MediumAnimal
    {
        public override int MaxAge => 22;
        public override int MatureAge => 3;
        public override int MaxHunger => 12;
        public override int LitterSize => 3;
        public override int MaxPregnancyDuration => 3;

        public Fox(int age) : base(age)
        {
        }

        public Fox(int age, int parentHunger) : base(age, parentHunger)
        {
        }

        public override bool isHungry(IEatable foodItem)
        {
            if (base.isHungry(foodItem))
            {
                if (foodItem.GetType() == typeof(Rabbit))
                {
                    return true;
                }
            }
            return false;
        }

        public override List<Animal> giveBirth(int currentHunger)
        {
            List<Animal> animals = new List<Animal>();
            for (int i = 0; i < LitterSize; i++)
            {
                animals.Add(new Fox(0, currentHunger));
            }
            IsPregnant = false;
            PregnancyDurationCounter = 0;
            return animals;

        }

    }
}
