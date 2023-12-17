using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ecosystem_Simulator
{
    internal class Rabbit : SmallAnimal, IEatable
    {

        public override int MaxAge => 16;
        public override int MatureAge => 4;
        public override int MaxHunger => 6;
        public override int LitterSize => 3;
        public override int PregnancyDuration => 2;
        public int NutritionalValue => 2;

        public Rabbit(int age) : base(age)
        {
        }

        public override bool canEat(IEatable foodItem)
        {
            if (base.canEat(foodItem))
            {
                if (foodItem.GetType() == typeof(Plant))
                {
                    return true;
                }
            }
 
            return false;
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
