using System;
using System.Collections.Generic;
using System.Text;
using PearlCalculatorLib.CalculationLib;

namespace PearlCalculatorLib.General
{

    public interface IDataChecker { }

    public interface IDataChecker<T> : IDataChecker
    {
        public void Check(string dataName, ref T value, out bool isModify);
    }

    public sealed class UpdateTNTDataChecker : IDataChecker<TNT>
    {
        public void Check(string dataName , ref TNT value , out bool isModify)
        {
            isModify = true;
            if(value.InducedVector.X > 1 || value.InducedVector.Y > 1 || value.InducedVector.Z > 1)
            {
                if(value.InducedVector.X < 1 || value.InducedVector.Y < 1 || value.InducedVector.Z < 1)
                {
                    isModify = false;
                }
            }
        }
    }

    public sealed class UpdateArrayChecker : IDataChecker<TNTArray>
    {
        public void Check(string dataName , ref TNTArray value , out bool isModify)
        {
            isModify = true;
            if(value.Red.Length != 2 || value.Blue.Length != 2)
            {
                isModify = false;
            }
            else if(!(value.Red[0] == 'N' || value.Red[0] == 'S'))
            {
                isModify = false;
            }
            else if(!(value.Blue[0] == 'N' || value.Blue[0] == 'S'))
            {
                isModify = false;
            }
            else if(!(value.Red[1] == 'W' || value.Red[1] == 'E'))
            {
                isModify = false;
            }
            else if(!(value.Blue[1] == 'W' || value.Blue[1] == 'E'))
            {
                isModify = false;
            }
        }
    }

    public sealed class UpdatePearlOffsetChecker : IDataChecker<Space3D>
    {
        public void Check(string dataName , ref Space3D value , out bool isModify)
        {
            isModify = true;
            if(value.X > 1 || value.X < -1 || value.Z > 1 || value.Z < -1)
            {
                value = new Space3D(0.9375 , 0 , 0.0625);
            }
        }
    }

    public sealed class UpdatePearlChecker : IDataChecker<Pearl>
    {
        public void Check(string dataName, ref Pearl value , out bool isModify)
        {
            isModify = true;
            if(value.Position.Y < 0)
            {
                isModify = false;
            }
        }
    }
}
