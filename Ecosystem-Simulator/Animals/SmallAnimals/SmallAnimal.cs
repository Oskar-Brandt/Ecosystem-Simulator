using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecosystem_Simulator.Interfaces;

namespace Ecosystem_Simulator.Animals.SmallAnimals
{
    abstract class SmallAnimal : Animal
    {

        public override abstract int MaxAge { get; }

        public override abstract int MatureAge { get; }

        public override abstract int MaxHunger { get; }

        public override abstract int LitterSize { get; }

        public override abstract int PregnancyDuration { get; }

        public SmallAnimal(int age) : base(age)
        {
        }

        public override bool isHungry(IEatable foodItem)
        {
            return base.isHungry(foodItem);
        }

        public override abstract List<Animal> giveBirth();

    }
}
