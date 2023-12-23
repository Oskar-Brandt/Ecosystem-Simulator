﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecosystem_Simulator.Animals.SmallAnimals;
using Ecosystem_Simulator.Interfaces;

namespace Ecosystem_Simulator.Animals.MediumAnimals
{
    internal class Fox : MediumAnimal
    {
        public override int MaxAge => 12;
        public override int MatureAge => 2;
        public override int MaxHunger => 8;
        public override int LitterSize => 1;
        public override int MaxPregnancyDuration => 3;

        public Fox(int age) : base(age)
        {
        }

        public override bool isHungry(IEatable foodItem)
        {
            if (base.isHungry(foodItem))
            {
                if (foodItem.GetType() == typeof(SmallAnimal))
                {
                    return true;
                }
            }
            return false;
        }

        public override List<Animal> giveBirth()
        {
            List<Animal> animals = new List<Animal>();
            for (int i = 0; i < LitterSize; i++)
            {
                animals.Add(new Fox(i));
            }
            return animals;

        }

    }
}
