using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorLib.General
{

    [Serializable]
    public class TNTArray
    {
        public string Red;
        public string Blue;

        public TNTArray()
        {

        }

        public TNTArray(string red, string blue)
        {
            Red = red;
            Blue = blue;
        }

    }
}
