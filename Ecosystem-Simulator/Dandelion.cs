using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecosystem_Simulator
{
    internal class Dandelion : Plant, IEatable
    {
        public int NutritionalValue => 1;

        public override void spread()
        {
            throw new NotImplementedException();
        }

        public override void die()
        {
            throw new NotImplementedException();
        }
    }
}
