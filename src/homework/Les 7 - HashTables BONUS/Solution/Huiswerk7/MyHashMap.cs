namespace AD
{
    /// <summary>
    ///    An extention on MyHashSet that creates a <Key,Value> HashMap.
    ///    The HashSet<K> is extended with an Array of the same size containing
    ///    values of type V.
    /// </summary>
    /// <typeparam name="K">Key Type</typeparam>
    /// <typeparam name="V">Value Type</typeparam>
	public class MyHashMap<K, V> : MyHashSet<K>
    {
        V[] _values;
        V[]? oldvalues;

        public V this[K key]
        {
            get => _values[WhereIs(key)];

            set
            {
                if (Contains(key))
                    _values[WhereIs(key)] = value;
                else
                    Add(key, value);
            }
        }

        public MyHashMap() : base()
        {
            _values = new V[_capacity];
        }

        protected override void Resize()
        {
            oldvalues = _values;
            base.Resize();
            oldvalues = null;
        }

        protected override void CreateNewStorageState(uint capacity)
        {
            base.CreateNewStorageState(capacity);
            _values = new V[_capacity];
        }

        protected override void placeValueAtNewLocation(K oldvalue, uint fromIndex)
        {
            base.placeValueAtNewLocation(oldvalue, fromIndex);
            if (oldvalue != null)
                _values[WhereIs(oldvalue)] = oldvalues[fromIndex];
        }

        public void Add(K key, V value)
        {
            base.Add(key);
            _values[WhereIs(key)] = value;
        }
    }
}
