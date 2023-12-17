using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecosystem_Simulator
{
    abstract class MediumAnimal : Animal
    {

        public override abstract int MaxAge { get; }

        public override abstract int MatureAge { get; }

        public override abstract int MaxHunger { get; }

        public override abstract int LitterSize { get; }

        public override abstract int PregnancyDuration { get; }

        public MediumAnimal(int age) : base(age)
        {
        }

        public override bool canEat(IEatable foodItem)
        {
            return base.canEat(foodItem);
        }


        public override abstract void move();

        public override abstract void giveBirth();


    }
}

