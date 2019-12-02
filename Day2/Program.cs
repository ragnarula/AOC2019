using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    public class Program
    {
        public static bool RunOpcodes(int[] memory)
        {
            var ip = 0;

            while (memory[ip] != 99)
            {
                switch (memory[ip])
                {
                    case 1:
                        memory[memory[ip + 3]] = memory[memory[ip + 1]] + memory[memory[ip + 2]];
                        break;
                    case 2:
                        memory[memory[ip + 3]] = memory[memory[ip + 1]] * memory[memory[ip + 2]];
                        break;
                    case 99:
                        break;
                    default:
                        return false;
                }
                ip += 4;
            }

            return true;
        }

        static void Main(string[] args)
        {
            string file = System.IO.File.ReadAllText(@"day2-part1-input.txt");
            var codes = file.Split(',');
            var memory = codes.Select(item => Convert.ToInt32(item)).ToArray();

            int[] workingMemory = new int[memory.Length];
            Array.Copy(memory, 0, workingMemory, 0, memory.Length);

            workingMemory[1] = 12;
            workingMemory[2] = 2;

            var result = RunOpcodes(workingMemory);

            Console.WriteLine("Part 1: {0}", workingMemory[0]);

            for(int noun = 0; noun < 100; ++noun)
            {
                for(int verb = 0; verb < 100; ++ verb)
                {
                    workingMemory = new int[memory.Length];
                    Array.Copy(memory, 0, workingMemory, 0, memory.Length);
                    workingMemory[1] = noun;
                    workingMemory[2] = verb;
                    var p2result = RunOpcodes(workingMemory);
                    if (workingMemory[0] == 19690720)
                    {
                        Console.WriteLine("Part 2 - Noun: {0}, Verb: {1}, Result: {2}", noun, verb, 100 * noun + verb);
                        break;
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
