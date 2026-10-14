using System;

namespace AD
{
    /// <summary>
    /// A selfmade HashSet over type T
    /// </summary>
    /// <typeparam name="T">The type of items</typeparam>
    public partial class MyHashSet<T>
    {
        protected uint _capacity;
        private double _maxload = 0.75;
        private double _minload = 0.1;

        private T[] _values = new T[0];
        private bool[] _occupied;
        private bool[] _deleted;
        private uint _count;
        private uint _deleteCount;

        // Delegates for the diverse functions
        // Some example implementations in HashHelp.cs
        private Func<T, ulong> HashFunction;
        private Func<uint, T, ulong> ProbeFunction;
        private Func<uint, uint> CapcityStrategy;

        // Lazy deletion by registering a boolean in
        // the _deleted array.
        private bool IsDeleted(ulong there) => _deleted[there];

        // If the type T is primitive we can't check for null
        // to seperate occupied positions from free positions
        // in the _values array. Occupation is stored in
        // the _occupied array of booleans.
        public bool IsOccupied(ulong there) => _occupied[there];

        public ulong GetCount() => _count;

        public T GetValue(ulong index) => _values[index];

        public uint Capacity => _capacity;

        // A rehash can also be triggerd by many deleted items that still
        // cause many probe's in lookups.
        // To prevent integer calculation multiply 1.0 * ()
        public double MaxLoadFactor =>
            1.0 * (_count + _deleteCount) / _capacity;

        // When many elements have been deleted, the MaxLoadFactor is still
        // high, but after a rehash only the non deleted item (_count) remain.
        // To prevent integer calculation multiply 1.0 * ()
        public double MinLoadFactor =>
            1.0 * (_count) / _capacity;

        /// <summary>
        ///    slide 32, 44, 55
        ///    Calculate the index of the location where to store the value
        ///    
        ///    Index = (H(x) + P(k)) % tablesize
        ///    
        /// </summary>
        /// <param name="value">The value to store</param>
        /// <param name="k">Attempt</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        private ulong GetIndexAt(T value, uint k)
        {
            if (value == null)
                throw new NullReferenceException();
            return (HashFunction(value) + ProbeFunction(k, value)) % _capacity;
        }

        /// <summary>
        ///   Returns the location of this value in the HashSet
        ///   Returns -1 if the value is null or not found.
        ///
        ///   Pre condition: return -1 if value = null\
        ///   
        ///   keep trying to find an equal value by increasing k
        ///   untill we hit a free spot (IsOccupied(index) == false)
        ///   or we do find the equal value.
        ///
        ///   If the value is not deleted (IsDeleted(index) == false)
        ///   and the value is equal return the index in all other cases
        ///   return -1
        /// </summary>
        /// <param name="value">search value</param>
        /// <returns>The location or -1</returns>
        public long WhereIs(T value)
        {
            if (value == null) return -1;

            uint k = 0;
            ulong there = GetIndexAt(value, k++);
            while (IsOccupied(there) && !value.Equals(_values[there]))
                there = GetIndexAt(value, k++);

            if (!IsDeleted(there) && value.Equals(_values[there]))
                return (long)there;

            return -1;
        }

        /// <summary>
        ///    Return true if we can find the value in this HashSet.
        ///    Return false if not.
        /// </summary>
        /// <param name="value">Looking for this value</param>
        /// <returns>true if the value is found</returns>
        public bool Contains(T value)
        {
            long where = WhereIs(value);
            return (where > -1 && !_deleted[where]);
        }

        /// <summary>
        ///   Find a free spot to store this value
        ///   increase k, the probing parameter, while:
        ///   - The current index is not a deleted value, if so overwrite
        ///   - And the current index is occupied, if not, this would be a good spot
        ///   - And the value at the current index is not equal to the value itself
        ///     if so, thats where we should be.
        /// </summary>
        /// <param name="value">What we want to store</param>
        /// <returns>the location to store it</returns>
        /// <exception cref="NullReferenceException">We can't store Null</exception>
        private ulong WhereToStore(T value)
        {
            if (value == null)
                throw new NullReferenceException();
            uint k = 0;
            ulong there = GetIndexAt(value, k++);
            while (!IsDeleted(there) && IsOccupied(there) && !value.Equals(_values[there]))
                there = GetIndexAt(value, k++);
            return there;
        }

        /// <summary>
        ///    Add a value in this HashSet
        ///    Precondition: If the value is already in the set, do nothing
        ///
        ///    Find the free spot where to store this value
        ///    Store the value and mark the position as occupied
        ///    Increase the counter.
        ///
        ///     If we are storing a previously deleted value, remove the
        ///     deleted mark and decrease the deleteditems counter
        ///
        ///     If the MaxLoadFactor is bigger than or equal to the _maxLoad factor
        ///     call Resize.
        ///
        ///     For an explanation of MinLoad and MaxLoad see the top of this file
        ///     Or some new slide :-)
        ///     
        /// </summary>
        /// <param name="value"></param>
        public void Add(T value)
        {
            if (Contains(value)) return;

            ulong there = WhereToStore(value);

            _values[there] = value;
            _occupied[there] = true;
            _count++;

            if (_deleted[there])
            {
                _deleted[there] = false;
                _deleteCount--;
            }
            if (MaxLoadFactor >= _maxload) Resize();
        }

        /// <summary>
        ///     Delete the value if this set contains it.
        ///     Lazy delete Slide 32!
        ///     Increase the delete count decrease the count.
        ///
        ///     If the MinLoadFactor is lower than or equal to the _minload factor
        ///     call Resize.
        ///
        ///     For an explanation of MinLoad and MaxLoad see the top of this file
        ///     Or some new slide :-)
        ///    
        /// </summary>
        /// <param name="value">The value to remove from this set</param>
        public void Delete(T value)
        {
            long where = WhereIs(value);
            if (where > -1)
            {
                _deleted[where] = true;
                _deleteCount++;
                _count--;
            }
            if (MinLoadFactor <= _minload)
                Resize();
        }

        public static partial class HashFunctions
        {
            /// <summary>
            ///    sheet 20 Java HashCode() for strings:
            ///    https://www.w3schools.com/java/ref_string_hashcode.asp
            /// 
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public static ulong JavaStringHashCode(string value)
            {
                int hash = 0;

                for (int i = 0; i < value.Length; i++)
                {
                    hash = 31 * hash + value[i];
                }
                return (ulong)hash;
            }

        }

        #region Probe functions
        public static partial class ProbeFunctions
        {
            /// We are creating function P in this formula: 
            ///    
            /// Index = (H(x) + P(k)) % tablesize
            /// 
            /// Implement both the Lineair and Qudratic Probing functions
            ///
            public static ulong LineairProbe(uint k) => (ulong)(k);
            public static ulong QuadraticProbe(uint k) => (ulong)(k * k);
        }
        #endregion

        // ==================== The rest is already implemented for you ===========

        /// <summary>
        ///   Resize and Rehash the HashSet
        ///   Use the Capacity strategy to increase the size:
        ///      _capacity = CapcityStrategy((2 * count) * (1/_maxload));
        ///   (Double the current element count) * (1 / maximum load factor)
        ///
        ///   Assume initial cap = 100 elements:
        ///   if _maxload, count, ==> 4 * count == 200
        ///           0.5     50  ==> (2 * 50) * (1/0.5) ==> 100 * 2 ==> 200
        ///
        ///   Assume initial cap = 100 elements:
        ///   if _maxload, count, ==> 2.666 * count = 200
        ///          0.75     75  ==> (2 * 75) * (1/0.75) ==> 150 * 1.333 ==> 200 
        /// 
        ///   Only store values in occupied spots that are not deleted.
        ///   Use the function WhereToStore(T value) to determine where
        ///   a value should be stored:
        ///   ulong there = WhereToStore(oldList[i]);
        ///   Don't forget to set the occupied boolean.
        /// </summary>
        protected virtual void Resize()
        {
            // calculate new size
            _capacity = CapcityStrategy((uint)(1.0 / _maxload * _count * 2));

            // temp reference to previous state
            T[] oldList = _values;
            bool[] oldOccupied = _occupied;
            bool[] olddeleted = _deleted;

            // create new state, 3 arrays
            CreateNewStorageState(_capacity);

            // copy non deleted values
            for (uint i = 0; i < oldList.Length; i++)
            {
                if (oldOccupied[i] && !olddeleted[i])
                {
                    placeValueAtNewLocation(oldList[i], i);
                }
            }
        }

        /// <summary>
        ///    Recreate new storrage arrays.
        /// </summary>
        /// <param name="capacity">New capacity</param>
        protected virtual void CreateNewStorageState(uint capacity)
        {
            _values = new T[capacity];
            _occupied = new bool[capacity];
            _deleted = new bool[capacity];
        }

        /// <summary>
        ///    Shortcut to place a value at a specific location during resize
        ///    
        /// </summary>
        /// <param name="oldvalue"> The value we need to add</param>
        /// <param name="fromIndex"> The previous location of the value</param>
        protected virtual void placeValueAtNewLocation(T oldvalue, uint fromIndex)
        {
            ulong there = WhereToStore(oldvalue);
            _values[there] = oldvalue;
            _occupied[there] = true;
        }
    }
}