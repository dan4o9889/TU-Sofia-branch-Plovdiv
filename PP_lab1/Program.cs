using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace d_ivanov
{
    class Program
    {
        static void Main(string[] args)
        {
            string command = null;
            Console.WriteLine("==Command List==");
            Console.WriteLine("- stop");
            Console.WriteLine("- normal");
            Console.WriteLine("- parallel");
            Console.WriteLine("================");
            Console.WriteLine("Enter command...");
            while ((command = Console.ReadLine())!="stop")
            {
                if (command == "normal")
                {
                    Console.Write("Enter size: ");
                    var size = int.Parse(Console.ReadLine());
                    CalcualteMatrixNormal(size);
                }
                else if (command == "parallel") 
                {
                    Console.Write("Enter size: ");
                    var size = int.Parse(Console.ReadLine());
                    CalculateMatrixParallel(size);
                }
            }
        }

        private static void CalcualteMatrixNormal(int size)
        {
            int[,] matrixOne = new int[size,size];
            int[,] matrixTwo = new int[size,size];
            int[,] resultMatrix = new int[size,size];
           var sw = new Stopwatch();

           for (int row = 0; row < size; row++)
           {
                for (int col = 0; col < size; col++)
                {
                    matrixOne[row,col] = row + col + 2;
                    matrixTwo[row, col] = row + 10;
                }
           }
           Console.WriteLine("===Normal cycle===");
           sw.Reset();

            sw = Stopwatch.StartNew();
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    resultMatrix[row, col] = matrixOne[row, col] * matrixTwo[row, col];
                }
            }
           sw.Stop();

           Console.WriteLine($"Elapsed time: {sw.Elapsed}");
        }
        private static void CalculateMatrixParallel(int size)
        {
            int[,] matrixOne = new int[size, size];
            int[,] matrixTwo = new int[size, size];
            int[,] resultMatrix = new int[size, size];
            var sw = new Stopwatch();
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    matrixOne[row, col] = row + col + 2;
                    matrixTwo[row, col] = row + 10;
                }
            }
            Console.WriteLine("===Parallel cycle===");
            sw.Reset();

            sw = Stopwatch.StartNew();
            Parallel.For(0, size, (row) =>
            {
                Parallel.For(0, size, (col) =>
                {
                    resultMatrix[row, col] = matrixOne[row, col] * matrixTwo[row, col];
                });
            });
            sw.Stop();
            
            Console.WriteLine($"Elapsed time: {sw.Elapsed}");
        }
    }
}
