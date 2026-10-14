using System;
using System.Collections.Generic;

namespace AD
{
    public class Integer : IComparable<Integer>
    {
        private long _value;
        private static Dictionary<long, Integer> _values = new();

        private Integer(long value)
        {
            _value = value;
            _values.Add(value, this);
        }

        public static Integer GetInteger(int value)
        {
            if (_values.ContainsKey(value))
                return _values[value];
            return new Integer(value);
        }

        public long Value => _value;

        public int CompareTo(Integer? other)
        {
            if (other == null)
                return 0;
            if (_value > other.Value)
                return 1;
            if (_value < other.Value)
                return -1;
            return 0;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            Integer IntObj = (Integer)obj;

            return (Value == IntObj.Value);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{_value}";
        }
    }
}

