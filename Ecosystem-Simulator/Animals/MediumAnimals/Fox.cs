using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecosystem_Simulator.Animals.SmallAnimals;
using Ecosystem_Simulator.Interfaces;
using Ecosystem_Simulator.Plants;

namespace Ecosystem_Simulator.Animals.MediumAnimals
{
    internal class Fox : MediumAnimal
    {
        public override int MaxAge => 28;
        public override int MatureAge => 5;
        public override int MaxHunger => 14;
        public override int LitterSize => 3;
        public override int MaxPregnancyDuration => 3;
        public override IEatable Diet => new Rabbit(0);
        //TODO: Make some kind of factory method to improve the Diet property
        public Fox(int age) : base(age)
        {
        }

        public Fox(int age, int parentHunger) : base(age, parentHunger)
        {
        }

        public override Animal createOffspring()
        {
            return new Fox(0, CurrentHunger);
        }

    }
}
