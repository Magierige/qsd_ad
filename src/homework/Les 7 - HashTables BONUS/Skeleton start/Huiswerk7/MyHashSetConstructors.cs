using System;

namespace AD
{
    public partial class MyHashSet<T>
    {
        /// <summary>
        ///    The default constructor. selected:
        ///    - Initial capacity 11 items.
        ///    - The default hash function:
        ///         java for strings,
        ///         The original number for primative nummeric values
        ///         The C# GetHashCode() for all other types.
        ///    - Quadratic probing
        ///    - Select the next prime from the prime table in HashHelp.cs
        ///    
        /// </summary>
        public MyHashSet() :
            this(
                11,
                HashFunctions.DefaultHashValue,
                ProbeFunctions.ProbeQuadratic,
                CapacityStrategies.PrimeSelect,
                0.47)
        { } //Default Load factor λ sheet 24

        public MyHashSet(uint initialCapacity, Func<uint, uint> capacityFunction) :
            this(
                initialCapacity,
                HashFunctions.DefaultHashValue,
                ProbeFunctions.ProbeQuadratic,
                capacityFunction,
                0.47)
        { }

        /// <summary>
        ///    A special constructor for the examples on slide 32 .. 64
        ///    Only the initial capacity, hash function and probe function
        ///    are required.
        ///    Capacity strategy is none this will simply doubble the capacity
        ///    when the load factor becomes 100%.
        ///    
        /// </summary>
        /// <param name="initialCapacity">Initial capacity</param>
        /// <param name="hash"> The hash function</param>
        /// <param name="probe"> The probe function</param>
        public MyHashSet(uint initialCapacity,
                         Func<T, ulong> hash,
                         Func<uint, T, ulong> probe
                         ) :
            this(
                initialCapacity,
                hash,
                probe,
                CapacityStrategies.None,
                1)
        { }

        /// <summary>
        ///     A constructor with all default values but
        ///     the initial capacity and the maximum load factor.
        ///     There is no Strategy for selecting a prime
        ///     capacity because it would change the initial
        ///     capacity directly. So resizing will result in
        ///     a none prime capacity.
        /// </summary>
        /// <param name="initialCapacity">The initial capacity</param>
        public MyHashSet(uint initialCapacity, double maxload) :
            this(initialCapacity,
                    HashFunctions.DefaultHashValue,
                    ProbeFunctions.ProbeQuadratic,
                    CapacityStrategies.None,
                  maxload)
        { } //Default Load factor λ sheet 24

        /// <summary>
        ///     A constructor with all default values but
        ///     the initial capacity.
        ///     There is no Strategy for selecting a prime
        ///     capacity because it would change the initial
        ///     capacity directly. So resizing will result in
        ///     a none prime capacity.
        /// </summary>
        /// <param name="initialCapacity">The initial capacity</param>
        public MyHashSet(uint initialCapacity) :
            this(
                initialCapacity,
                HashFunctions.DefaultHashValue,
                ProbeFunctions.ProbeQuadratic,
                CapacityStrategies.None,
                0.47)
        { } //Default Load factor λ sheet 24

        /// <summary>
        ///     This constructor builds a HashSet with a specific
        ///     probing function. You can pass one of the
        ///     ProbeFunctions in HashHelp.cs or define one yourself like this:
        ///     (k,x) => { return k; }, // for liniar probing
        ///     x is an argument of type T, you can use x for double hashing like this:
        ///     (k, x) => { return (ulong)(k * (7 - (x % 7))); },
        /// </summary>
        /// <param name="probe">An alternative probing function</param>
        public MyHashSet(Func<uint, T, ulong> probe) :
            this(
                11,
                HashFunctions.DefaultHashValue,
                probe,
                CapacityStrategies.PrimeSelect,
                0.47)
        { }  //Default Load factor λ sheet 24

        /// <summary>
        ///     The base constroctor offering all possible dependancies
        ///     
        /// </summary>
        /// <param name="initialCapacity">The initial capacity</param>
        /// <param name="hashFunction">The hash function</param>
        /// <param name="probeFunction">The probing function</param>
        /// <param name="capacityStrategy">How to select a new capacity when resizing</param>
        /// <param name="maxLoad">a max load factor λ slide 24</param>
        public MyHashSet(uint initialCapacity,
                         Func<T, ulong> hashFunction,
                         Func<uint, T, ulong> probeFunction,
                         Func<uint, uint> capacityStrategy,
                         double maxLoad)
        {
            this.HashFunction = hashFunction;
            this.ProbeFunction = probeFunction;
            this.CapcityStrategy = capacityStrategy;

            _capacity = CapcityStrategy(initialCapacity);

            CreateNewStorageState(_capacity);

            if (maxLoad > _minload && maxLoad <= 1)
                _maxload = maxLoad;
            _minload = maxLoad / 10;
        }
    }
}

