using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.PearlCalculationLib.SizeDataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.PearlCalculationLib.Entity
{
    public abstract class Entity
    {
        public Space3D momemtum;
        public Space3D position;

        public abstract Size GetSize();

        public abstract void Tick();
    }
}
