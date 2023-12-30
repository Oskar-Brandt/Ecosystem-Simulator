using Ecosystem_Simulator.Interfaces;
using System.Collections.Generic;

namespace Ecosystem_Simulator.Animals.MediumAnimals
{
    abstract class MediumAnimal : Animal
    {

        public MediumAnimal(int age) : base(age)
        {
        }

        public MediumAnimal(int age, int parentHunger) : base(age, parentHunger)
        {

        }

    }
}

