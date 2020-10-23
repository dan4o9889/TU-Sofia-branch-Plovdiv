using System;
using System.Diagnostics;
using System.Threading;

namespace PP_LU2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter number of rows for matrix: ");
            var rows = int.Parse(Console.ReadLine());
            Console.Write("Enter number of columns for the matrix: ");
            var cols = int.Parse(Console.ReadLine());

            var matrix = new double[rows, cols];

            FillMatrixRandom(matrix);

            var threadOne = new Thread(() => MatrixSum(matrix));
            var threadTwo = new Thread(() => MatrixSum(matrix));

            threadOne.Name = "Thread One";
            threadTwo.Name = "Thread Two";
            threadOne.Priority = ThreadPriority.Highest;
            threadTwo.Priority = ThreadPriority.Lowest;

            threadOne.Start();
            threadTwo.Start();

            threadOne.Join();
            threadTwo.Join();
        }

        private static void MatrixSum(double[,] matrix)
        {
            double sum = 0;
            var sw = new Stopwatch();
            sw.Start();
            for (int rows = 0; rows < matrix.GetLength(0); rows++)
            {
                for (int cols = 0; cols < matrix.GetLength(1); cols++)
                {
                    sum += matrix[rows, cols];
                    if(rows % 100==0 && cols % 100 == 0)
                    {
                        Console.WriteLine($"Name of thread is {Thread.CurrentThread.Name}," +
                            $"row {rows}, column {cols}");
                    }
                }
            }
            sw.Stop();
            Console.WriteLine($"Elapsed time {sw.Elapsed} - {Thread.CurrentThread.Name}");
        }

        private static void FillMatrixRandom(double[,] matrix)
        {
            for (int rows = 0; rows < matrix.GetLength(0); rows++)
            {
                for (int cols = 0; cols < matrix.GetLength(1); cols++)
                {
                    var randomNumber = new Random();
                    matrix[rows, cols] = randomNumber.NextDouble();

                }
            }
        }
    }
}
