using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecosystem_Simulator
{
    internal class Fox : Animal
    {
        public override int MaxAge => 10;

        public override int MatureAge => 2;

        public override int MaxHunger => 6;

        public override int LitterSize => 1;

        public override int PregnancyDuration => 3;

        public Fox(int age) : base(age)
        {
        }

        public override void mate()
        {
            throw new NotImplementedException();
        }

        public override void eat()
        {
            throw new NotImplementedException();
        }

        public override void move()
        {
            throw new NotImplementedException();
        }

        public override void giveBirth()
        {
            throw new NotImplementedException();
        }

    }
}
