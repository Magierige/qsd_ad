using NUnit.Framework;

namespace AD
{
	public class Opdracht_0_Tests
	{
        [Test]
        public void IcanCreateAHashMapAndCapacityStrategyIsUsed()
        {
            MyHashSet<string> mymap = new(
                initialCapacity: 10,
                capacityFunction: MyHashSet<string>.CapacityStrategies.FirstPrime);
            // Assert that the capacity is altered to the first prime number.
            Assert.IsTrue(mymap.Capacity == 11);
        }

        [TestCase(2u, 11u)]
        [TestCase(10u, 11u)]
        [TestCase(11u, 11u)]
        [TestCase(7650u, 7669u)]
        [TestCase(800564u, 800573u)]
        public void CorrectlyFindTheNextPrime(uint number, uint next)
        {
            uint expect = next;
            uint actual = MyHashSet<int>.CapacityStrategies.FirstPrime(number);
            Assert.That(expect, Is.EqualTo(actual));
        }

        [TestCase(10u, 0, 10)]    // no resize
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
    }
}
