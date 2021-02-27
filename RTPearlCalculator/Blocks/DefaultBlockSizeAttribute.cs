using System;

namespace RTPearlCalculatorLib.Blocks
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
    public class DefaultBlockSizeAttribute : Attribute
    {
        public readonly string Name;

        public DefaultBlockSizeAttribute(string name)
        {
            this.Name = name;
        }
    }
}