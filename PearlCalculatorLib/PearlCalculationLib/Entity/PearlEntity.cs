using PearlCalculatorLib.PearlCalculationLib.MathLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.AABB;
using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.PearlCalculationLib.Entity
{
    [Serializable]
    public class PearlEntity : Entity, ICloneable
    {
        public override Space3D Size => new Space3D(0.25 , 0.25 , 0.25);



        public PearlEntity(Space3D momemtum , Space3D position)
        {
            Motion = momemtum;
            Position = position;
        }

        public PearlEntity(PearlEntity pearl) : this(pearl.Motion , pearl.Position) { }

        public PearlEntity()
        {

        }

        public PearlEntity WithPosition(double x , double y , double z)
        {
            Position = new Space3D(x , y , z);
            return this;
        }

        public PearlEntity WithVector(double x , double y , double z)
        {
            Motion = new Space3D(x , y , z);
            return this;
        }

        public override void Tick()
        {
            Position += Motion;
            Motion *= 0.99;
            Motion.Y -= 0.03;
        }

        public object Clone()
        {
            PearlEntity pearl = new PearlEntity
            {
                Position = Position ,
                Motion = Motion
            };
            return pearl;
        }
    }
}
