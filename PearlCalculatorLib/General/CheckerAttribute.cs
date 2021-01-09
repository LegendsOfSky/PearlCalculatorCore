using System;
using System.Collections.Generic;

namespace PearlCalculatorLib.General
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class CheckerAttribute : Attribute
    {
        static Dictionary<Type , IDataChecker> CheckerDict = new Dictionary<Type , IDataChecker>();

        public readonly Type CheckerType;

        public CheckerAttribute(Type checker)
        {
            if(checker == null)
                throw new NullReferenceException();

            var inters = checker.GetInterfaces();

            bool isChecker = false;

            foreach(var type in inters)
            {
                if(type == typeof(IDataChecker))
                {
                    if(!CheckerDict.TryGetValue(checker , out var _))
                        CheckerDict.Add(checker , (IDataChecker)Activator.CreateInstance(checker));

                    isChecker = true;
                    break;
                }
            }

            if(isChecker)
                CheckerType = checker;
            else
                throw new Exception();
        }

        public static IDataChecker<T> GetChecker<T>(Type checkerType)
        {
            if(CheckerDict.TryGetValue(checkerType, out var checker))
            {
                return checker as IDataChecker<T>;
            }

            return null;
        }
    }
}