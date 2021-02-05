using PearlCalculatorLib.PearlCalculationLib.SizeDataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.PearlCalculationLib.Entity
{
    public class TNTEntity : Entity
    {
        public override Size GetSize()
        {
            return EntitySize.TNT;
        }

        public override void Tick()
        {
            throw new NotImplementedException();
        }
    }
}
