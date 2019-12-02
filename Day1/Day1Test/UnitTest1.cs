using System;
using System.Linq;
using Day1;
using Day2;
using NUnit.Framework;

namespace Day1Test
{
    public class Day1Tests
    {
        [Test]
        public void TestCalculateExtra()
        {
            Assert.That(Day1.Program.CalculateExtra(14), Is.EqualTo(2));
            Assert.That(Day1.Program.CalculateExtra(1969), Is.EqualTo(966));
            Assert.That(Day1.Program.CalculateExtra(100756), Is.EqualTo(50346));
        }
    }

    public class Day2Tests
    {
        [Test]
        public void TestOpcode()
        {
            int[] memory = { 1, 0, 0, 0, 99 };
            Day2.Program.RunOpcodes(memory);
            int[] expectedOutput = { 2, 0, 0, 0, 99 };
            Assert.IsTrue(Enumerable.SequenceEqual(memory, expectedOutput));
        }
    }
}
