using Ecosystem_Simulator.Animals.SmallAnimals;
using Ecosystem_Simulator.Interfaces;
using System;
using System.Collections.Generic;

namespace Ecosystem_Simulator.Animals
{
    abstract class Animal : LifeForm
    {
        public abstract int MaxAge { get; }
        public abstract int MatureAge { get; }
        public abstract int MaxHunger { get; }
        public abstract int LitterSize { get; }
        public abstract int MaxPregnancyDuration { get; }
        public abstract IEatable Diet { get; }
        public int CurrentAge { get; set; }
        public int CurrentHunger { get; set; }
        public bool IsDead { get; set; }
        public bool IsPregnant { get; set; }
        public int MovementSpeed { get; set; }
        public int PregnancyDurationCounter { get; set; }

        public Animal()
        {
            IsDead = false;
            IsPregnant = false;
            MovementSpeed = 1;
            PregnancyDurationCounter = 0;
        }
        public Animal(int age) : this()
        {
            CurrentAge = age;
            CurrentHunger = (int)(Math.Ceiling(MaxHunger * 0.75));
        }

        public Animal(int age, int parentHunger) : this()
        {
            CurrentAge = age;
            CurrentHunger = (int)(Math.Ceiling(parentHunger * 0.75));
        }

        public abstract Animal createOffspring();

        public void eat(IEatable foodItem)
        {
            CurrentHunger += foodItem.NutritionalValue;
            if (CurrentHunger > MaxHunger)
            {
                CurrentHunger = MaxHunger;
            }

        }

        public bool isHungry(IEatable foodItem)
        {
            if (CurrentHunger < MaxHunger)
            {
                if (canEatFood(foodItem))
                {
                    return true;
                }
            }
            return false;
        }

        public bool canEatFood(IEatable foodItem)
        {
            if (foodItem.GetType() == Diet.GetType())
            {
                return true;
            }
            return false;
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

        public List<Animal> giveBirth()
        {
            List<Animal> animals = new List<Animal>();
            for (int i = 0; i < LitterSize; i++)
            {
                animals.Add(createOffspring());
            }
            IsPregnant = false;
            PregnancyDurationCounter = 0;
            return animals;

        }

        public void age()
        {
            CurrentAge++;
            if (IsPregnant)
            {
                PregnancyDurationCounter++;
            }

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
            else if (CurrentAge > MaxAge - MaxPregnancyDuration)
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
