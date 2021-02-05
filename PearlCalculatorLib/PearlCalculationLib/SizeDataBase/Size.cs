using PearlCalculatorLib.CalculationLib;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Xml.Serialization;

namespace PearlCalculatorLib.PearlCalculationLib.SizeDataBase
{
    public class Size
    {
        public Space3D AAConer;
        public Space3D BBConer;

        public Size()
        {

        }
        
        public Size(double x2 , double y2 , double z2)
        {
            AAConer = new Space3D(0 , 0 , 0);
            BBConer = new Space3D(x2 , y2 , z2);
        }

        public Size(double x1 , double y1 , double z1 , double x2 , double y2, double z2)
        {
            AAConer = new Space3D(x1 , y1 , z1);
            BBConer = new Space3D(x2 , y2 , z2);
        }

        public Size(Space3D size)
        {
            AAConer = new Space3D(0 , 0 , 0);
            BBConer = size;
        }


    }
}
