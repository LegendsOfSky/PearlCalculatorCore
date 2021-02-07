using PearlCalculatorLib.CalculationLib;
using PearlCalculatorLib.PearlCalculationLib.AABB;
using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.PearlCalculationLib.Entity
{
    public class PearlEntity : Entity
    {
        public override Space3D Size => new Space3D(0.25, 0.25, 0.25);

        public PearlEntity(Space3D momemtum, Space3D position)
        {
            this.Momemtum = momemtum;
            this.Position = position;
        }

        public PearlEntity(PearlEntity pearl) : this(pearl.Momemtum , pearl.Position) { }

        public PearlEntity WithPosition(double x , double y , double z)
        {
            this.Position = new Space3D(x , y , z);
            return this;
        }

        public PearlEntity WithVector(double x , double y , double z)
        {
            this.Momemtum = new Space3D(x , y , z);
            return this;
        }

        public override void Tick()
        {
            Position += Momemtum;
            Momemtum *= 0.99;
            Momemtum.Y -= 0.03;
        }
    }
}
