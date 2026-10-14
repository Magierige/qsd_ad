using NUnit.Framework;

namespace AD
{
	public class NummericHashMapTests
	{
        MyHashSet<byte> my101ByteMap = new MyHashSet<byte>(
            MyHashSet<byte>.ProbeFunctions.ProbeLineair);

        MyHashSet<int> my101intMap = new MyHashSet<int>(
            MyHashSet<int>.ProbeFunctions.ProbeQuadratic);


        [Test]
        public void IcanCreateAHashMap()
        {
            MyHashSet<byte> mymap = new MyHashSet<byte>(256);
            mymap.Add(127);

            Assert.IsTrue(mymap.Contains(127));
            Assert.IsFalse(mymap.Contains(110));
            Assert.That(mymap.GetCount(), Is.EqualTo(1));

        }
        [Test]
        public void StoreNummericValuesAtValueModCapacity()
        {
            MyHashSet<byte> mymap = new MyHashSet<byte>(256);
            mymap.Add(127);
            Assert.That(mymap.WhereIs(127), Is.EqualTo(127));
        }

        [TestCase(1, 1)] //mod 17 =1
        [TestCase(100, 15)] //mod 17 15
        [TestCase(202, 16)]
        [TestCase(18, 2)]
        public void ForceCollisionWithLiniarProbing(byte value, int where)
        {
            my101ByteMap.Add(value);
            Assert.That(my101ByteMap.WhereIs(value), Is.EqualTo(where));
        }

        [TestCase(1, 1)]
        [TestCase(18, 2)]
        [TestCase(35, 5)]
        [TestCase(5, 6)]
        public void ForceCollisionWithQudraticProbing(int value, int where)
        {
            my101intMap.Add(value);
            Assert.That(my101intMap.WhereIs(value), Is.EqualTo(where));
        }
    }
}

