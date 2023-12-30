using Ecosystem_Simulator.Interfaces;
using System.Collections.Generic;

namespace Ecosystem_Simulator.Animals.SmallAnimals
{
    abstract class SmallAnimal : Animal
    {

        public SmallAnimal(int age) : base(age)
        {
        }

        public SmallAnimal(int age, int parentHunger) : base(age, parentHunger)
        {

        }

    }
}
