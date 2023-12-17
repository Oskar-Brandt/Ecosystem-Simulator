using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace Ecosystem_Simulator
{
    abstract class Animal
    {
        public abstract int MaxAge { get; }
        public abstract int MatureAge { get; }
        public abstract int MaxHunger { get; }
        public abstract int LitterSize { get; }
        public abstract int PregnancyDuration { get; }
        public int CurrentAge { get; set; }
        public int CurrentHunger { get; set; }
        public bool IsDead { get; set; }
        public int MovementSpeed { get; set; }


        public Animal(int age)
        {
            CurrentAge = age;
            CurrentHunger = MaxHunger;
            IsDead = false;
            MovementSpeed = 1;
        }

        public abstract void eat();

        public abstract void mate();

        public abstract void move();

        public abstract void giveBirth();

        public void age()
        {
            CurrentAge++;
        }

        public void hungrify()
        {
            CurrentHunger--;
        }

    }
}
