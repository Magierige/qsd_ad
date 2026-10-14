using System;
using System.Collections.Generic;
using System.Diagnostics;

using AD;

Opdracht_1();
Opdracht_2();
Opdracht_3();
Opdracht_4();

void Opdracht_1()
{
    Console.WriteLine();
    Console.WriteLine("Slide 3: Needle in a haystack:");
    Console.WriteLine("   Select a random numbers from 400_001 ...");
    Console.WriteLine("   Select random numbers out of 400_001, ");
    Console.WriteLine("   untill we find the one not previously selected");
    TimeSpan elapsed = TimeAction(() =>
    {
        Stack<ulong> ints = Opdracht3RandomNumbers(100_000, 500_000, 400_001);
    });
    Console.WriteLine("Opdracht 3 random numbers took me: " + elapsed);
}

void Opdracht_2()
{
    Console.WriteLine();
    Console.WriteLine("Slide 17: Using the Java string HashCode(),");
    Console.WriteLine("          How can we get John, Lisa and Sandra");
    Console.WriteLine("          at the location on this sheet?");

    //Well, lets add a single char
    MyHashSet<string> myNames = new(16);

    myNames.Add("John Smith 4");
    myNames.Add("Lisa Smith 3");
    myNames.Add("Sandra Dee 7");

    for (ulong i = 0; i < myNames.Capacity; i++)
        Console.WriteLine($"{i}: {myNames.GetValue(i)}");

    Console.WriteLine();
    Console.WriteLine("Slide 20 en verder: Where is 'Hello'");

    // Online tool for checking the java HashCode for a string
    // https://www.w3schools.com/java/tryjava.asp?filename=demo_ref_string_hashcode
    // java HashCode() for "Hello" == 69609650
    // 69609650 % 10000 = 9650

    MyHashSet<string> stringSet = new(10000);
    Console.WriteLine("Before we add Hello: " + stringSet.WhereIs("Hello"));
    stringSet.Add("Hello");
    Console.WriteLine("After we add Hello: " + stringSet.WhereIs("Hello"));
}

void Opdracht_3()
{
    Console.WriteLine();
    Console.WriteLine("Slide 35 en verder (40,51 en 62): A HashSet with 5 numbers");

    MyHashSet<uint> mymapLiniar = new(
                    initialCapacity: 10,                //Initial capacity
                    hash: (x) => x % 10,                //Hash function x mod 10
                    probe: (k, x) => k                  //Probe function lineair
                    );

    MyHashSet<uint> mymapQuadratic = new(
                    initialCapacity: 10,                //Initial capacity
                    hash: (x) => x % 10,                //Hash function x mod 10
                    probe: (k, x) => k * k              //Probe function Quadratic
                    );

    MyHashSet<uint> mymapDoubleHash = new(
                    initialCapacity: 10,                //Initial capacity
                    hash: (x) => x % 10,                //Hash function x mod 10
                    probe: (k, x) => k * (7 - (x % 7))  //Probe function Double Hash
                    );

    uint[] numbers = new uint[] { 89, 18, 49, 58, 9 };
    foreach (uint number in numbers)
    {
        mymapLiniar.Add(number);
        mymapQuadratic.Add(number);
        mymapDoubleHash.Add(number);
    }

    Console.WriteLine();
    Console.WriteLine(" ind| lin| qdr| dbl");
    Console.WriteLine("-------------------");
    for (ulong i = 0; i < mymapLiniar.Capacity; i++)
    {
        Console.WriteLine(
            ("" + i).PadLeft(4) + "|" +
            // GetValue() can't return null or "" if the HashMap type is primative.
            // It must return a number, the default is 0
            // Thats why myMap.IsOccupied(i) is called to check for empty
            // spots in the HashMap.
            (mymapLiniar.IsOccupied(i) ? "" + mymapLiniar.GetValue(i) : "").PadLeft(4) + "|" +
            (mymapQuadratic.IsOccupied(i) ? "" + mymapQuadratic.GetValue(i) : "").PadLeft(4) + "|" +
            (mymapDoubleHash.IsOccupied(i) ? "" + mymapDoubleHash.GetValue(i) : "").PadLeft(4));
    }
}

void Opdracht_4()
{
    Console.WriteLine();
    Console.WriteLine("");

    MyHashMap<string, int> nums = new();

    for (int i = 0; i < 750_000; i++)
        nums.Add($"nummer{i}", i);
    Console.WriteLine("Driehonderdvijfduizend schrijf je zo: " + nums["nummer305000"]);
    Console.WriteLine("Capacity: " + nums.Capacity);
    Console.WriteLine("Count: " + nums.GetCount());
    Console.WriteLine("Item nummer202 : " + nums.WhereIs("nummer202"));
    Console.WriteLine("Key 39: " + nums.GetValue(39));
    Console.WriteLine();

    for (int i = 100; i < 749_990; i++)
        nums.Delete($"nummer{i}");

    Console.WriteLine("Zevenenzestig schrijf je zo: " + nums["nummer67"]);
    Console.WriteLine("Capacity: " + nums.Capacity);
    Console.WriteLine("Count: " + nums.GetCount());
    Console.WriteLine("Deleted item: " + nums.WhereIs("nummer202"));
    Console.WriteLine("Key 39: " + nums.GetValue(39));
    Console.WriteLine();
}

// Helper functions go here...

TimeSpan TimeAction(Action blockingAction)
{
    Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
    blockingAction();
    stopWatch.Stop();
    return stopWatch.Elapsed;
}

Stack<ulong> Opdracht3RandomNumbers(int min, int max, int number)
{
    Stack<ulong> ints = new();

    MyHashSet<ulong> ulongSet = new(
        initialCapacity: 1_000_000,
        hashFunction: (x) => x,
        probeFunction: (k, x) => (ulong)((1 << (int)k) - 1),
        capacityStrategy: MyHashSet<ulong>.CapacityStrategies.PrimeSelect,
        maxLoad: 0.6);

    Random rnd = new Random();

    while (ints.Count < number)
    {
        ulong val = (ulong)rnd.Next(min, max + 1);
        if (!ulongSet.Contains(val))
        {
            ints.Push(val);
            ulongSet.Add(val);
        }
    }
    return ints;
}
