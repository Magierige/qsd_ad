using NUnit.Framework;

namespace AD
{
	public class Opdracht_3_Tests
	{
        [TestCase(58, 1u)]
        [TestCase(9, 2u)]
        public void CorrectLocationLineairProbe(int find, uint location)
        {
            int[] numbers = { 89, 18, 49, 58, 9 };
            MyHashSet<uint> mymap =
                new(
                    10,                                           // Initial capacity
                    (x) => { return x % 10; },                    // Hash function x mod 10
                    MyHashSet<uint>.ProbeFunctions.ProbeLineair,  // Probe function lineair
                    MyHashSet<int>.CapacityStrategies.None,       // Capacity strategy none
                    1);                                           // load until 100% full
            foreach (int number in numbers)
                mymap.Add((uint)number);
            Assert.That(mymap.WhereIs((uint)find), Is.EqualTo(location));
        }

        [TestCase(58, 2u)]
        [TestCase(49, 0u)]
        [TestCase(9, 3u)]
        public void CorrectLocationQuadraticProbe(int find, uint location)
        {
            int[] numbers = { 89, 18, 49, 58, 9 };
            MyHashSet<uint> mymap =
                new MyHashSet<uint>(
                    10,                                             // Initial capacity
                    (x) => { return x % 10; },                      // Hash function x mod 10
                    MyHashSet<uint>.ProbeFunctions.ProbeQuadratic,  // Probe function quadratic
                    MyHashSet<uint>.CapacityStrategies.None,        // Capacity strategy none
                    1);                                             // load until 100% full
            foreach (int number in numbers) mymap.Add((uint)number);
            Assert.That(mymap.WhereIs((uint)find), Is.EqualTo(location));
        }
    }
}
