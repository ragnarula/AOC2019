using System;
using Day1;
using NUnit.Framework;

namespace Day1Test
{
    public class UnitTest1
    {
        [Test]
        public void TestCalculateExtra()
        {
            Assert.That(Program.CalculateExtra(14), Is.EqualTo(2));
            Assert.That(Program.CalculateExtra(1969), Is.EqualTo(966));
            Assert.That(Program.CalculateExtra(100756), Is.EqualTo(50346));
        }
    }
}
