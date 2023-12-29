using Ecosystem_Simulator.Interfaces;
using Ecosystem_Simulator.Plants;
using System.Collections.Generic;

namespace Ecosystem_Simulator.Animals.SmallAnimals
{
    internal class Rabbit : SmallAnimal, IEatable
    {

        public override int MaxAge => 22;
        public override int MatureAge => 4;
        public override int MaxHunger => 10;
        public override int LitterSize => 3;
        public override int MaxPregnancyDuration => 2;
        public int NutritionalValue => 4;

        public Rabbit(int age) : base(age)
        {
        }

        public Rabbit(int age, int parentHunger) : base(age, parentHunger)
        {
        }
        public override bool isHungry(IEatable foodItem)
        {
            if (base.isHungry(foodItem))
            {
                if (foodItem.GetType() == typeof(Dandelion))
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
                animals.Add(new Rabbit(0, currentHunger));
            }
            IsPregnant = false;
            PregnancyDurationCounter = 0;
            return animals;

        }
    }
}
