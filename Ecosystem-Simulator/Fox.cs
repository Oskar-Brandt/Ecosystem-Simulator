using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecosystem_Simulator
{
    internal class Fox : MediumAnimal
    {
        public override int MaxAge => 12;
        public override int MatureAge => 2;
        public override int MaxHunger => 8;
        public override int LitterSize => 1;
        public override int PregnancyDuration => 3;

        public Fox(int age) : base(age)
        {
        }

        public override bool canEat(IEatable foodItem)
        {
            if (base.canEat(foodItem))
            {
                if (foodItem.GetType() == typeof(SmallAnimal))
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
