using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.CalculationLib
{

    [Serializable]
    public class TNT
    {
        public Space3D InducedVector;
        public Space3D Position;

        public TNT WithIndecedVector(double x, double y, double z)
        {
            this.InducedVector = new Space3D(x , y , z);
            return this;
        }
    }
}
