using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    class Program
    {
        enum Mode { Position, Immediate };
        struct Instruction
        {
            public int Op;
            public Mode Mode1;
            public Mode Mode2;
            public Mode Mode3;
        }

        static Mode ParseMode(int mode)
        {
            Debug.Assert(mode == 0 || mode == 1);
            if(mode == 0)
            {
                return Mode.Position;
            }
            else
            {
                return Mode.Immediate;
            }
        }

        static Instruction ParseInstruction(int instruction)
        {
            Instruction parsed = new Instruction();
            parsed.Op = instruction % 100;
            Debug.Assert(parsed.Op >= 1 && parsed.Op <= 8);

            parsed.Mode1 = Mode.Position;
            parsed.Mode2 = Mode.Position;
            parsed.Mode3 = Mode.Position;

            if(instruction > 99)
            {
                parsed.Mode1 = ParseMode((instruction / 100) % 10);
            }
            if(instruction > 999)
            {
                parsed.Mode2 = ParseMode((instruction / 1000) % 10);
            }
            if (instruction > 9999)
            {
                parsed.Mode3 = ParseMode((instruction / 10000) % 10);
            }
            return parsed;  
        }

        static int GetInput()
        {
            return 5;
        }

        static void Output(int value)
        {
            Console.WriteLine("Output: {0}", value);
        }

        static int GetValue(int[] memory, int address, Mode mode)
        {
            if(mode == Mode.Immediate)
            {
                return memory[address];
            }
            int realAddress = memory[address];
            return memory[realAddress];
        }

        static void SetValue(int[] memory, int address, int value)
        {
            memory[memory[address]] = value;
        }

        static void Run(int[] memory)
        {
            var ip = 0;

            while(memory[ip] != 99)
            {
                var instruction = ParseInstruction(memory[ip]);
                if(instruction.Op == 1)
                {
                    int result = GetValue(memory, ip + 1, instruction.Mode1) + GetValue(memory, ip + 2, instruction.Mode2);
                    SetValue(memory, ip + 3, result);
                    ip += 4;
                }
                if (instruction.Op == 2)
                {
                    int result = GetValue(memory, ip + 1, instruction.Mode1) * GetValue(memory, ip + 2, instruction.Mode2);
                    SetValue(memory, ip + 3, result);
                    ip += 4;
                }
                if(instruction.Op == 3)
                {
                    SetValue(memory, ip + 1, GetInput());
                    ip += 2;
                }
                if(instruction.Op == 4)
                {
                    int result = GetValue(memory, ip + 1, instruction.Mode1);
                    Output(result);
                    ip += 2;
                }
                if(instruction.Op == 5)
                {
                    int result = GetValue(memory, ip + 1, instruction.Mode1);
                    if(result != 0)
                    {
                        ip = GetValue(memory, ip + 2, instruction.Mode2);
                    }
                    else
                    {
                        ip += 3;
                    }
                }
                if (instruction.Op == 6)
                {
                    int result = GetValue(memory, ip + 1, instruction.Mode1);
                    if (result == 0)
                    {
                        ip = GetValue(memory, ip + 2, instruction.Mode2);
                    }
                    else
                    {
                        ip += 3;
                    }
                }
                if(instruction.Op == 7)
                {
                    int first = GetValue(memory, ip + 1, instruction.Mode1);
                    int second = GetValue(memory, ip + 2, instruction.Mode2);
                    int result = first < second ? 1 : 0;
                    SetValue(memory, ip + 3, result);
                    ip += 4;
                }
                if(instruction.Op == 8)
                {
                    int first = GetValue(memory, ip + 1, instruction.Mode1);
                    int second = GetValue(memory, ip + 2, instruction.Mode2);
                    int result = first == second ? 1 : 0;
                    SetValue(memory, ip + 3, result);
                    ip += 4;
                }
            }
        }

        static void Main(string[] args)
        {
            string file = System.IO.File.ReadAllText(@"day5-part1-input.txt");
            var codes = file.Split(',');
            var memory = codes.Select(item => Convert.ToInt32(item)).ToArray();

            int[] workingMemory = new int[memory.Length];
            Array.Copy(memory, 0, workingMemory, 0, memory.Length);

            Run(workingMemory);

            Console.WriteLine("Finished!");
            Console.ReadKey();
        }
    }
}
