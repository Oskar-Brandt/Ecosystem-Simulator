using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecosystem_Simulator.Interfaces;

namespace Ecosystem_Simulator.Plants
{
    internal class Dandelion : Plant, IEatable
    {
        public int NutritionalValue => 1;


        public Dandelion() : base()
        {

        }
        public override Plant spread()
        {
            return new Dandelion();
        }
    }
}
