using NUnit.Framework;
using System;

namespace AD
{
    //Slide 3: Needle in a haystack:
	public class Opdracht_1_Tests
	{
        [Test]
        public void LookupUnknownValuesOrNullLeadsToMinusOne()
        {
            MyHashSet<string> mymap = new(
                initialCapacity: 10,
                capacityFunction: MyHashSet<string>.CapacityStrategies.FirstPrime);
            Assert.That(mymap.WhereIs(null) == -1);
        }

        [Test]
        public void CantAddNullToAHashSet()
        {
            MyHashSet<string> mymap = new(
                initialCapacity: 10,
                capacityFunction: MyHashSet<string>.CapacityStrategies.FirstPrime);
            Assert.Throws<NullReferenceException>(() => mymap.Add(null));
        }
    }
}
