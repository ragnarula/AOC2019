using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
    class Program
    {
        struct Point
        {
            public int x;
            public int y;
        }

        struct Line
        {
            public Point begin;
            public Point end;
        }

        class Wire
        {
            public List<Line> vertical;
            public List<Line> horizontal;
            public Wire()
            {
                vertical = new List<Line>();
                horizontal = new List<Line>();
            }

            List<Point> intersect(List<Line> vertical, List<Line> horizontal)
            {
                var points = new List<Point>();
                foreach (var hl in horizontal)
                {
                    Debug.Assert(hl.begin.y == hl.end.y);
                    Debug.Assert(hl.begin.x <= hl.end.x);
                    foreach (var vl in vertical)
                    {
                        Debug.Assert(vl.begin.x == vl.end.x);
                        Debug.Assert(vl.begin.y <= vl.end.y);
                        if(vl.begin.x <= hl.end.x && vl.begin.x >= hl.begin.x)
                        {
                            if(hl.begin.y <= vl.end.y && hl.begin.y >= vl.begin.y)
                            {
                                points.Add(new Point { x = vl.begin.x, y = hl.begin.y });
                            }
                        }
                    }
                }
                return points;
            }

            public List<Point> intersect(Wire otherWire)
            {
                List<Point> points = new List<Point>();
                points.AddRange(intersect(vertical, otherWire.horizontal));
                points.AddRange(intersect(otherWire.vertical, horizontal));
                return points;
            }
        }

        static int CountStepsTo(string[] parts, Point point)
        {
            int x = 0;
            int y = 0;
            int total = 0;
            foreach (var part in parts)
            {
                var dir = part.Substring(0, 1)[0];
                var len = Convert.ToInt32(part.Substring(1));
                for (int i = 0; i < len; ++i)
                {
                    total += 1;
                    switch (dir)
                    {
                        case 'R':
                            x += 1;
                            break;
                        case 'L':
                            x -= 1;
                            break;
                        case 'U':
                            y += 1;
                            break;
                        case 'D':
                            y -= 1;
                            break;
                    }

                    if(x == point.x && y == point.y)
                    {
                        return total;
                    }
                }
            }
            return -1;
        }

        static Wire MakeWire(string[] parts)
        {
            var wire = new Wire();

            int x = 0;
            int y = 0;
            foreach (var part in parts)
            {
                var dir = part.Substring(0, 1)[0];
                var len = Convert.ToInt32(part.Substring(1));
                var a = new Point { x = x, y = y };
                switch (dir)
                {
                    case 'R':
                        x += len;
                        break;
                    case 'L':
                        x -= len;
                        break;
                    case 'U':
                        y += len;
                        break;
                    case 'D':
                        y -= len;
                        break;
                }
                var b = new Point { x = x, y = y };

                if (a.x == b.x) //vertical
                {
                    if(a.y <= b.y)
                    {
                        wire.vertical.Add(new Line { begin = a, end = b });
                    }
                    else
                    {
                        wire.vertical.Add(new Line { begin = b, end = a });
                    }
                }
                else
                {
                    if(a.x <= b.x)
                    {
                        wire.horizontal.Add(new Line { begin = a, end = b });
                    }
                    else
                    {
                        wire.horizontal.Add(new Line { begin = b, end = a });
                    }
                }
            }
            return wire;
        }

        static void Main(string[] args)
        {
            string file = System.IO.File.ReadAllText(@"day3-part1-input.txt");
            string[] lines = file.Split('\n');

            var wire1 = lines[0].Split(',');
            var wire2 = lines[1].Split(',');

            var first = MakeWire(wire1);
            var second = MakeWire(wire2);

            var intersections = first.intersect(second);

            Point smallest = intersections[0];
            foreach(var point in intersections)
            {
                if(point.x + point.y < smallest.x + smallest.y)
                {
                    smallest = point;
                }
            }

            Console.WriteLine("Part 1: {0}", smallest.x + smallest.y);
            List<int> counts = new List<int>();
            foreach(var intersection in intersections)
            {
                var count1 = CountStepsTo(wire1, intersection);
                var count2 = CountStepsTo(wire2, intersection);
                counts.Add(count1 + count2);
            }
            counts.Sort();
            Console.WriteLine("Part 2: {0}", counts[0]);
            Console.ReadKey();
        }
    }
}
