using Ecosystem_Simulator.Animals.MediumAnimals;
using Ecosystem_Simulator.Interfaces;
using Ecosystem_Simulator.Plants;
using System.Collections.Generic;

namespace Ecosystem_Simulator.Animals.SmallAnimals
{
    internal class Rabbit : SmallAnimal, IEatable
    {

        public override int MaxAge => 22;
        public override int MatureAge => 4;
        public override int MaxHunger => 10;
        public override int LitterSize => 3;
        public override int MaxPregnancyDuration => 2;
        public override IEatable Diet => new Dandelion();
        //TODO: Make some kind of factory method to improve the Diet property
        public int NutritionalValue => 4;

        public Rabbit(int age) : base(age)
        {
        }

        public Rabbit(int age, int parentHunger) : base(age, parentHunger)
        {
        }

        public override Animal createOffspring()
        {
            return new Rabbit(0, CurrentHunger);
        }
    }
}
