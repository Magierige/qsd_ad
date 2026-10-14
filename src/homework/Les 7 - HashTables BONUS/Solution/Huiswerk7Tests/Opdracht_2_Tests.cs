using NUnit.Framework;

namespace AD
{
	public class Opdracht_2_Tests
	{
        [Test]
        public void IcanStoreAStringValue()
        {
            MyHashSet<string> mymap = new MyHashSet<string>();
            mymap.Add("Goedemorgen");
            Assert.That(mymap.Contains("Goedemorgen"), Is.True);
            Assert.That(mymap.Contains("Goedemiddag"), Is.False);
        }

        [Test]
        public void TheCorrectHashValueForStrings()
        {
            MyHashSet<string> mymap = new MyHashSet<string>(10000);
            mymap.Add("Hello");
            Assert.That(mymap.WhereIs("Hello") == 9650);
        }

        [Test]
        public void DeleteAndRestoreStringValues()
        {
            MyHashSet<string> mymap = new MyHashSet<string>(7);

            mymap.Add("Lisa"); //lisa will endup at pos 2
            mymap.Add("Bram"); //Bram will also hash to 2
                               //After a Qudratic probe this will be 3
            mymap.Add("Anna"); //Anna will endup at pos 1

            Assert.That(mymap.Contains("Ann"), Is.False);
            Assert.That(mymap.Contains("Anna"), Is.True);

            mymap.Delete("Lisa");
            Assert.That(mymap.WhereIs("Bram") == 3); //Bram still at pos 3
            Assert.That(mymap.WhereIs("Lisa") == -1); //Lisa is deleted
            mymap.Add("Lisa"); //Lisa should return to her place
            Assert.That(mymap.WhereIs("Lisa") == 2); //Lisa is back
            mymap.Delete("Bram");
            Assert.That(mymap.WhereIs("Bram") == -1); //Bram still at pos 3
            mymap.Add("Hasan"); //Hasan has hash 3, he will replace Bram
                                //Still 3 item so no rehash
            Assert.That(mymap.Capacity == 7);
            mymap.Add("Bram"); // Now bram is stored at pos 6 (Quadratic probing)
                               // But then, the loadfactor > 0.47
                               // So after a resize Bram goes to pos 2
            Assert.That(mymap.WhereIs("Bram") == 14);
        }
    }
}
