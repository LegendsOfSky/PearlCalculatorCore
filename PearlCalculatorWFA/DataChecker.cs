using System;
using System.Collections.Generic;
using System.Text;

namespace PearlCalculatorWFA
{
    public interface IDataChecker<T>
    {
        bool check(T data);
    }

    public sealed class DoubleChecker : IDataChecker<string>
    {
        public static DoubleChecker Checker = new DoubleChecker();

        public bool check(string data)
        {

            if(string.IsNullOrEmpty(data))
                return true;

            if(string.IsNullOrWhiteSpace(data))
                return false;

            return Double.TryParse(data , out var s);
        }
    }

    public sealed class DirectionChecker : IDataChecker<string>
    {
        public static DirectionChecker Checker = new DirectionChecker();

        public bool check(string data)
        {
            if(string.IsNullOrEmpty(data))
                return true;

            if(string.IsNullOrWhiteSpace(data) || data?.Length > 2)
                return false;

            //NW NE SW SE

            bool result = true;

            result &= data.Length >= 1 && (data[0] == 'N' || data[0] == 'S');
            result &= data.Length == 2 && (data[1] == 'W' || data[1] == 'E');

            return result;
        }
    }
}
