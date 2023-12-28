using Ecosystem_Simulator.Interfaces;
using System.Collections.Generic;

namespace Ecosystem_Simulator.Animals.MediumAnimals
{
    abstract class MediumAnimal : Animal
    {

        public override abstract int MaxAge { get; }

        public override abstract int MatureAge { get; }

        public override abstract int MaxHunger { get; }

        public override abstract int LitterSize { get; }

        public override abstract int MaxPregnancyDuration { get; }

        public MediumAnimal(int age) : base(age)
        {
        }

        public MediumAnimal(int age, int parentHunger) : base(age, parentHunger)
        {

        }

        public override bool isHungry(IEatable foodItem)
        {
            return base.isHungry(foodItem);
        }

        public override abstract List<Animal> giveBirth(int currentHunger);


    }
}

