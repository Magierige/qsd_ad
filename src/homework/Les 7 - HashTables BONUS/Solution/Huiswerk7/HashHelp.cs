using System;

namespace AD
{
    public partial class MyHashSet<T>
    {
        /// <summary>
        ///   Helper class for HashSets
        ///   This class provides strategies for capacity planning,
        ///   Stragies for probing and HashFunctions that can calculate
        ///   a HashValue given an argument of type T
        /// </summary>

        static uint[] Primes = {
        3u,          5u,          9u,
        17u,         37u,         67u,
        131u,        257u,        521u,
        1031u,       2053u,       4099u,
        8209u,       16411u,      32771u,
        65537u,      131101u,     262147u,
        524309u,     524309u,     1048583,
        2097169,     4194319,     8388617,
        16777259,    33554467,    67108879,
        134217757,   268435459,   536870923,
        1073741827 };

        private static bool IsStringType()
        {
            Type type = typeof(T);

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.String:
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsNumericType()
        {
            Type type = typeof(T);

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return true;
                default:
                    return false;
            }
        }

        private static ulong defaultHashOnType(T value)
        {
            if (value == null)
                throw new NullReferenceException();

            if (IsNumericType())
                return (ulong)Convert.ToInt64(value);
            if (IsStringType())
                return HashFunctions.JavaStringHashCode("" + value.ToString());
            return (ulong)value.GetHashCode();
        }

        #region Capacity strategy
        /// <summary>
        ///   A capacity strategie is a strategie for selecting the next
        ///   Capacity when resizing a HashSet.
        /// </summary>
        public static class CapacityStrategies
        {
            public static uint PrimeSelect(uint index)
            {
                uint primeIndex = (uint)Math.Log2(index);
                while (Primes[primeIndex] < index &&
                       primeIndex < Primes.Length -1) primeIndex++;
                if (Primes[primeIndex] >= index) return Primes[primeIndex];

                return FirstPrime(index);
            }

            public static uint FirstPrime(uint searchFrom)
            {
                if (searchFrom <= 11) return 11;

                while (!IsPrime(searchFrom))
                {
                    searchFrom = NextPrimeCandidate(searchFrom);
                }
                return searchFrom;
            }

            public static uint None(uint searchFrom)
            {
                return searchFrom;
            }

            private static bool IsPrime(uint number)
            {
                if (number <= 1u) return false;
                if (number == 2u) return true;
                if (number % 2u == 0) return false;

                uint boundary = (uint)Math.Floor(Math.Sqrt(number));

                for (uint i = 3; i <= boundary; i += 2)
                    if (number % i == 0)
                        return false;

                return true;
            }

            private static uint NextPrimeCandidate(uint searchFrom)
            {
                return (searchFrom + 1) | 1u;
            }

        }
        #endregion

        #region Hash functions
        /// <summary>
        /// Analyses https://www.rose-hulman.edu/class/cs/csse230/202140/Slides/19-HashTableAnalysis.pdf
        /// </summary>
        public  static partial class HashFunctions
        {
            public static ulong UlongHash(ulong value) => value;

            public static ulong HashUlong64(string value)
            {
                string input = "" + value;
                string s1 = input.Substring(0, input.Length / 2);
                string s2 = input.Substring(input.Length / 2);

                ulong x = ((uint)s1.GetHashCode()) << 0x20 | (uint)s2.GetHashCode();

                return x;
            }

            public static ulong HashGetHashCode(string value)
            {
                return (uint)value.GetHashCode();
            }

            public static ulong DefaultHashValue(T value)
            {
                return defaultHashOnType(value);
            }
        }
        #endregion

        #region Probe functions
        public static partial class ProbeFunctions
        {
            /// <summary>
            /// Analyses https://www.rose-hulman.edu/class/cs/csse230/202140/Slides/19-HashTableAnalysis.pdf
            /// </summary>

            public static ulong ProbeLineair(uint k, T value)
            {
                return LineairProbe(k);
            }

            public static ulong ProbeQuadratic(uint k, T value)
            {
                return QuadraticProbe(k);
            }
        }
        #endregion
    }
}
