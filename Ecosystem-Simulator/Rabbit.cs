using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ecosystem_Simulator
{
    internal class Rabbit : Animal
    {

        public override int MaxAge => 16;
        public override int MatureAge => 4;
        public override int MaxHunger => 5;
        public override int LitterSize => 3;
        public override int PregnancyDuration => 2;

        public Rabbit(int age) : base(age)
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
