using NUnit.Framework;

namespace AD
{
    public class MoreSpecificTests
    {
        [TestCase(100u, 20, 100)] // no resize 
        [TestCase(10u, 7, 20)]    // resize after 5ht element (1/0.5) * 2 * 5 == 20
        [TestCase(13u, 17, 56)]   // resize after 7ht element (1/0.5) * 2 * 8 == 28,
                                  // resize after 14ht element (1/0.5) * 2 * 14 == 56
        public void CorrectLoadfactor(uint initialCap, int numberItems, int sizeAfterResize)
        {
            string[] strings = {"ksjdfhiweu", "oqwbd", "83jeue",
                            "poidbq3", "wiuhsfd" };
            MyHashSet<string> mymap = new MyHashSet<string>(initialCap, 0.5);
            for (int i = 0; i < numberItems; i++)
            {
                mymap.Add($"{strings[i % 5]} {strings[(4 * i + 4) % 5]} {i}");
            }
            Assert.That(1.0 * numberItems / sizeAfterResize, Is.EqualTo(mymap.MaxLoadFactor));
        }


        [Test]
        public void IcanStoreManyValues()
        {
            MyHashSet<long> mymap = new MyHashSet<long>(202,
                MyHashSet<long>.HashFunctions.DefaultHashValue,
                MyHashSet<long>.ProbeFunctions.ProbeQuadratic,
                MyHashSet<long>.CapacityStrategies.PrimeSelect,
                0.47);

            for (int i = 0; i < 2000000; i++) { mymap.Add(i); }

            for (int i = 0; i < 2000000; i += 7)
            {
                Assert.That(mymap.Contains(i), Is.True);
            }
            Assert.That(mymap.Contains(2000000), Is.False);
        }

        [Test]
        public void AddAValues()
        {
            MyHashSet<Integer> mymap = new MyHashSet<Integer>(7, 0.5);

            Integer acht = Integer.GetInteger(8);
            mymap.Add(acht);
            Assert.That(mymap.GetCount() == 1);
            mymap.Add(acht);
            Assert.That(mymap.GetCount() == 1);

            for (int i = 10; i < 100; i++) mymap.Add(Integer.GetInteger(i));
            Assert.That(mymap.GetCount() == 91);
            for (int i = 20; i < 100; i++) mymap.Delete(Integer.GetInteger(i));
            Assert.That(mymap.GetCount() == 11);
        }

        [Test]
        public void MaxLoadFactorCanBe1()
        {
            // Store 5 values in a map of capacity 3
            // After 3 values the map doubles in size
            // So Capacity is 6 after 5 elements are added.
            MyHashSet<Integer> mymap = new MyHashSet<Integer>(
                            3,
                            MyHashSet<Integer>.HashFunctions.DefaultHashValue,
                            MyHashSet<Integer>.ProbeFunctions.ProbeLineair,
                            MyHashSet<Integer>.CapacityStrategies.None,
                            1);
            for (int i = 0; i < 5; i++) mymap.Add(Integer.GetInteger(i));
            Assert.That(mymap.GetCount() == 5);
            Assert.That(mymap.Capacity == 6);
        }

        [Test]
        public void DeleteManyValues()
        {
            MyHashSet<int> mymap = new();
            uint initialCap = mymap.Capacity;
            for (int i = 0; i < 500_000; i++) mymap.Add(i);
            for (int i = 0; i < 500_000; i++) mymap.Delete(i);
            Assert.That(mymap.Capacity <= initialCap);
        }
    }
}