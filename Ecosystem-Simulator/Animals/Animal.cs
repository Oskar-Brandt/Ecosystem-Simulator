using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using Ecosystem_Simulator.Interfaces;

namespace Ecosystem_Simulator.Animals
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
        public bool IsPregnant { get; set; }
        public int MovementSpeed { get; set; }

        public Animal(int age)
        {
            CurrentAge = age;
            CurrentHunger = (int)(Math.Ceiling(MaxHunger * 0.75));
            IsDead = false;
            IsPregnant = false;
            MovementSpeed = 1;
        }

        public void eat(IEatable foodItem)
        {
            CurrentHunger += foodItem.NutritionalValue;
        }

        public virtual bool isHungry(IEatable foodItem)
        {
            return CurrentHunger < MaxHunger;
        }

        public void mate(Animal matingPartner)
        {
            hungrify();
            matingPartner.hungrify();

            // 50/50 chance of either this animal becoming pregnant, or the other animal becoming pregnant
            Random rand = new Random();
            bool pregnant = rand.Next(2) == 1;

            if (pregnant)
            {
                IsPregnant = true;
            }
            else
            {
                matingPartner.IsPregnant = true;
            }
        }

        public int move()
        {
            return MovementSpeed;
        }

        public abstract List<Animal> giveBirth();

        public void age()
        {
            CurrentAge++;
            if (CurrentAge > MaxAge)
            {
                die();

            }
        }

        public void hungrify()
        {
            CurrentHunger--;
            if (CurrentHunger <= 0)
            {
                die();
            }
        }

        public void die()
        {
            IsDead = true;
        }

        public bool canMate()
        {
            bool canMate = true;

            //Animal cannot mate if too hungry (needs to conserve energy). Represented by its currentHunger being less than 1/4 of maxHunger
            if (CurrentHunger <= MaxHunger / 4)
            {
                canMate = false;
            }

            //Animal cannot mate if too old (infertility due to old age). Represented by its currentAge being more than maxAge - pregnancyDuration (It will die before giving birth)
            else if (CurrentAge > MaxAge - PregnancyDuration)
            {
                canMate = false;
            }

            //Animal cannot mate if it is already pregnant.
            else if (IsPregnant)
            {
                canMate = false;
            }

            //Animal cannot mate if it has not reached maturity
            else if (MatureAge > CurrentAge)
            {
                canMate = false;
            }

            return canMate;
        }

    }
}
