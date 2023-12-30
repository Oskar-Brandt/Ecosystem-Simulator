using Ecosystem_Simulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecosystem_Simulator.Plants
{
    abstract class Plant : LifeForm
    {
        public bool IsDead { get; set; }

        public Plant()
        {
            IsDead = false;
        }

        public abstract Plant spread();

        public void die()
        {
            IsDead = true;
        }
    }
}
