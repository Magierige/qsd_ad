# Huiswerk7

Les 7 gaat over Hashing. Eén van de datatypes die hierbij een rol
spelen is de `HashSet`. Een `HashSet` is een Set van waarden (values) die
opgeslagen worden. De kracht van de `HashSet` zit in de Algoritmische
complexiteit van de Set operaties:

- Add: $O(1)$
- Delete: $O(1)$
- WhereIs: $O(1)$
- Contains: $O(1)$

Zie Slide 5 van de powerpoint bij les 7.

Onze implementatie van `HashSet` heeft de volgende attributen:

```csharp
                             //Commentaar in het nederlands als uitleg, staat niet in de code zelf.
  private uint _capacity;    //Hoe veel elementen kunnen er in de Set
  private double _maxload;   //Boven welke load factor moeten we de Set vergroten
  private double _minload;   //Onder welke load factor moeten we de Set verkleinen
  private T[] _values;       //Een array van values van het type T
  private bool[] _occupied;  //Staat er een waarde op deze plaats (belangrijk bij primitieve types)
  private bool[] _deleted;   //Is de waarde verwijderd? Lazy deletion Slide 32
  private uint _count;       //Hoeveel elementen zitten er in de Set? (ex. de verwijderde elementen)
  private uint _deleteCount; //Hoeveel elementen zijn verwijderd (belangrijk voor het bepalen van de load factor)
```
Verder kent de `HashSet` een drietal delegates:

```csharp
  private Func<T, ulong> HashFunction;        // De primaire Hash functie is een functie van T naar een unsigned long
  private Func<int, T, ulong> ProbeFunction;  // De Probe functie wordt gebruik in het geval van een collision (slide 32 en verder). 
                                              // Let op! de ProbeFunction krijgt ook een value van het type T mee om Doublehashing 
                                              // mogelijk te maken als Probing strategie. We vragen jullie alleen om Probe(k) te schrijven.
                                              // zie regel 214 en 215 in deze README / 190 em 192 in HashSet.cs.
  private Func<uint, uint> CapcityStrategy;   // De functie die bepaald welke nieuwe capacity wordt toegepast bij een resize
                                              // Deze functie ontvangt de dubbele size als input maar aangezien we streven naar een
                                              // priemgetal als size (Slide 16), willen we eigenlijk op zoek naar een priemgetal
                                              // in de buurt hiervan.
```

De `HashSet` is een partial class. De constructors zijn uitgewerkt in
een `MyHashSetConstructors.cs` om verwarring te voorkomen. Bestudeer
deze zolang als je wilt, Je hoeft ze niet te kennen om het huiswerk te
kunnen maken.

Daarnaast is er een module `HashHelp.cs` waarin een deel van de class
`HashSet` is uitgewerkt. Dit betreft statische functies die je kunt
gebruiken als implementatie voor de bovenstaande delegates. Hiervoor
geldt ook dat we deze functies uitleggen bij de opgaven waar je ze
kunt gebruiken.

Een deel van de implementatie is al gegeven:

```csharp
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

        // A Resize including Rehash should be triggerd if, after adding a value, the MaxLoadFactor
        // is equal or bigger than _maxLoad. A rehash should also be triggerd by deleted items.
        // Deleted items do cause longer lookups when they need to be skipped.
        // So when calculating the current MaxLoadFactor we calculate _count + _deleteCount
        // To prevent integer calculation multiply 1.0 * ()
        public double MaxLoadFactor =>
            1.0 * (_count + _deleteCount) / _capacity;

        // When many elements have been deleted, the current MaxLoadFactor is still
        // high, but after a Resize and Rehash only the non deleted items (_count) remain.
        // so the current MinLoadFactor calculates _count / _capacity.
        // To prevent integer calculation multiply 1.0 * ()
        public double MinLoadFactor =>
            1.0 * (_count ) / _capacity;
```

## Opgave

In `Opdrachten.cs` staat een deel van `HashSet` uitgewerkt, maar bij
veel methods staat nog `NotImplementedException`.

Implementeer de volgende methods:

```csharp
        /// <summary>
        ///    slide 32, 44, 55
        ///    Calculate the index of the location where to store the value
        ///    The general function looks like this:
        ///    Index = (H(x) + P(k,x)) % tablesize
        ///    
        ///    Where    H(x) is the HashFunction
        ///    And      P(k,x) is the ProbeFunction
        ///    And      x is the value 
        ///    And      k is the probing attempt
        /// </summary>
        /// <param name="value">The value to store</param>
        /// <param name="k">Attempt</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        private ulong GetIndexAt(T value, uint k)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        ///   Returns the location of this value in the HashSet
        ///   Returns -1 if the value is null or not found.
        ///
        ///   Pre condition: return -1 if value = null
        ///   
        ///   keep trying to find an equal value by increasing k
        ///   untill we hit a free spot (IsOccupied(index) == false)
        ///   or we do find the equal value.
        ///
        ///   If the value is not deleted (IsDeleted(index) == false)
        ///   and the value is equal return the index in all other cases
        ///   return -1
        ///   
        /// </summary>
        /// <param name="value">search value</param>
        /// <returns>The location or -1</returns>
        public long WhereIs(T value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///    Return true if we can find the value in this HashSet.
        ///    Return false if not.
        /// </summary>
        /// <param name="value">Looking for this value</param>
        /// <returns>true if the value is found</returns>
        public bool Contains(T value)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
        ///     If the MaxLoadFactor is greater than or equal to the _maxLoad factor
        ///     call Resize.
        ///
        ///     For an explanation of MinLoad and MaxLoad see the top of this file
        ///     Or some new slide :-)
        ///     
        /// </summary>
        /// <param name="value"></param>
        public void Add(T value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Delete the value if this set contains it.
        ///     Lazy delete Slide 32!
        ///     Increase the delete count decrease the count.
        ///
        ///     If the MinLoadFactor is lower than or equal to
        ///     _minload call Resize.
        ///
        ///     For an explanation of MinLoad and MaxLoad see the top of this file
        ///     Or some new slide :-)
        ///    
        /// </summary>
        /// <param name="value">The value to remove from this set</param>
        public void Delete(T value)
        {
            throw new NotImplementedException();
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
                throw new NotImplementedException();
            }

        }

        public static partial class ProbeFunctions
        {
            /// <summary>
            /// Analyses https://www.rose-hulman.edu/class/cs/csse230/202140/Slides/19-HashTableAnalysis.pdf
            /// </summary>
            public static ulong ProbeLineair(uint k, T value) => throw new NotImplementedException();

            public static ulong ProbeQuadratic(uint k, T value) => throw new NotImplementedException();

        }

```