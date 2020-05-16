using System;
using System.Diagnostics;

namespace SudokuGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = Console.LargestWindowWidth / 2;
            Console.WindowHeight = Console.LargestWindowHeight - 10;
            var sw = new Stopwatch();
            Console.Write("Sudoku tablosunun kenar uzunluğunu girin (table length): ");
            byte tableLength = byte.Parse(Console.ReadLine());
            Console.Write("Sudoku tablosunun blok genişliğini girin (block width): ");
            byte blockWidth = byte.Parse(Console.ReadLine());
            Console.Clear();
            DateTime start = DateTime.Now;
            sw.Start();
            Console.WriteLine("{0}x{0}/{1}x{2}\nSudoku hazırlanıyor... {3}", tableLength, blockWidth, tableLength / blockWidth, start);
            Layout layout = new Layout(tableLength, blockWidth);
            DateTime end = DateTime.Now;
            sw.Stop();

            foreach (var cell in layout.Cells)
            {
                var color = (ConsoleColor)((cell.BlockIndex + 1) % (tableLength / blockWidth + 1) + 1);
                Console.BackgroundColor = color;
                Console.CursorLeft = (cell.X + 1) * 4;
                Console.CursorTop = (cell.Y + 1) * 2 + 2;
                if (color == ConsoleColor.White || color == ConsoleColor.Gray || color == ConsoleColor.Cyan || color == ConsoleColor.Yellow)
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(" {0,2}  ", cell.Worth);
                if (color == ConsoleColor.White || color == ConsoleColor.Gray || color == ConsoleColor.Cyan || color == ConsoleColor.Yellow)
                    Console.ForegroundColor = ConsoleColor.Gray;
                Console.CursorTop++;
                Console.CursorLeft -= 5;
                Console.Write("     ");
            }
            Console.CursorLeft = 0;
            Console.CursorTop = layout.TableLength * 2 + 6;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Sudoku hazırlandı. " + end);
            Console.WriteLine("{0,2} sn ({1:0}dk {2,0:0}sn)", sw.Elapsed.TotalSeconds, sw.Elapsed.TotalMinutes, sw.Elapsed.TotalSeconds);
            Console.WriteLine("{0} ms", sw.ElapsedMilliseconds);
            Console.WriteLine("{0} ticks", sw.ElapsedTicks);
            Console.ReadKey();
        }
    }
}
