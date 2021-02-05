using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.PearlCalculationLib.SizeDataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.PearlCalculationLib.Entity
{
    public class PearlEntity : Entity
    {
        public PearlEntity(Space3D momemtum, Space3D position)
        {
            this.momemtum = momemtum;
            this.position = position;
        }

        public PearlEntity(PearlEntity pearl) : this(pearl.momemtum , pearl.position) { }

        public PearlEntity WithPosition(double x , double y , double z)
        {
            this.position = new Space3D(x , y , z);
            return this;
        }

        public PearlEntity WithVector(double x , double y , double z)
        {
            this.momemtum = new Space3D(x , y , z);
            return this;
        }



        public override Size GetSize()
        {
            return EntitySize.EnderPearl;
        }

        public override void Tick()
        {
            position += momemtum;
            momemtum *= 0.99;
            momemtum.Y -= 0.03;
        }
    }
}
