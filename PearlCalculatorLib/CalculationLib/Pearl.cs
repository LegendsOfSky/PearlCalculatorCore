using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.CalculationLib
{
    [Serializable]
    public struct Pearl
    {
        public Space3D Vector;
        public Space3D Position;

        public Pearl(Space3D vector, Space3D postion) 
        {
            this.Vector   = vector;
            this.Position = postion;
        }

        public Pearl(Pearl pearl):this(pearl.Vector, pearl.Position) { }

        public Pearl WithPosition(double x, double y, double z)
        {
            this.Position = new Space3D(x , y , z);
            return this;
        }

        public Pearl WithVector(double x, double y, double z)
        {
            this.Vector = new Space3D(x , y , z);
            return this;
        }
    }
}
