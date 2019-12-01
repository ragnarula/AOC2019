using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
    public class Program
    {

        public static int CalculateExtra(int ModuleFuel)
        {
            int TotalextraFuel = 0;
            int ExtraFuel = ModuleFuel;

            while (ExtraFuel > 0)
            {
                ExtraFuel = (ExtraFuel / 3) - 2;
                TotalextraFuel += ExtraFuel > 0 ? ExtraFuel : 0;
            }

            return TotalextraFuel;
        }

        static void Main(string[] args)
        {

            string[] lines = System.IO.File.ReadAllLines(@"input.txt");
            var ModuleFuel = lines
                .Select(item => Convert.ToDouble(item))
                .Select(item => item / 3)
                .Select(item => Convert.ToInt32(Math.Floor(item)))
                .Select(item => item - 2);

            var Part1 = ModuleFuel
                .Aggregate(0, (Current, Next) => Current + Next);

            Console.WriteLine("Part 1: {0}", Part1);

            var Part2 = ModuleFuel
                .Select(item => item + CalculateExtra(item))
                .Aggregate(0, (Current, Next) => Current + Next);

            Console.WriteLine("Part 2: {0}", Part2);

            Console.ReadKey();

            
        }
    }
}
