using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecosystem_Simulator
{
    abstract class Plant
    {
        public bool IsDead { get; set; }

        public Plant() {
            IsDead = false;
        }

        public abstract void spread();

        public abstract void die();
    }
}
