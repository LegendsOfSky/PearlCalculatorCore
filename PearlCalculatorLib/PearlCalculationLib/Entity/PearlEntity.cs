using PearlCalculatorLib.PearlCalculationLib.World;
using System;

namespace PearlCalculatorLib.PearlCalculationLib.Entity
{
    [Serializable]
    public class PearlEntity : Entity, IDeepCloneable<PearlEntity>
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

        public override void Tick()
        {
            Position += Motion;
            Motion *= 0.99;
            Motion.Y -= 0.03;
        }

        public PearlEntity DeepClone() => new PearlEntity
        {
            Position = Position ,
            Motion = Motion
        };

        object IDeepCloneable.DeepClone() => DeepClone();
    }
}
