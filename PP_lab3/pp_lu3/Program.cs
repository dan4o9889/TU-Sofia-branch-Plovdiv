using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace pp_lu3
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //Initializing array and size of the array
            Console.Write("Enter size of array: ");
            var size = int.Parse(Console.ReadLine());
            var array = new double[size];

            //Calling method which fills array with random numbers
            FillArrayRandom(array);

            //Running method without Tasks
            var sw = new Stopwatch();
            sw.Start();
            ArrayFunc(array,0,size);
            sw.Stop();
            Console.WriteLine($"Time elapsed without Tasks - {sw.Elapsed}");

            //Calling method which operates with Tasks
            TaskFunc(array);
        }

        private static void TaskFunc(double[] array)
        {
            //Initializing the Task array
            var countOfProc = Environment.ProcessorCount;
            var taskArray = new Task[countOfProc];

            //Initializing variables which are used for calculating fixed ranges
            var low = 0;
            var high = array.GetLength(0);
            var total = high - low;
            var subRange = total / countOfProc;
            var currentLow = low;

            //Main body of the method where we assign the Tasks
            var sw = new Stopwatch();
            sw.Start();
          for (int task = 0; task < taskArray.GetLength(0); task++)
          {

                var currentHigh = currentLow + subRange;
                if (task == taskArray.GetLength(0) - 1) currentHigh = high;

                taskArray[task] = Task.Factory.StartNew(() => ArrayFunc(array,currentLow,currentHigh));

                Console.WriteLine($"{currentLow} - {currentHigh}");

                currentLow += subRange;
          }
            Task.WaitAll(taskArray);
            sw.Stop();

            Console.WriteLine($"Time elapsed with Tasks - {sw.Elapsed}");
        }

        private static void FillArrayRandom(double[] array)
        {
            for (int index = 0; index < array.GetLength(0); index++)
            {
                var randomNumber = new Random();
                array[index] = randomNumber.NextDouble();
            }
        }

        private static void ArrayFunc(double []array, int start, int stop)
        {
            for (int index = start; index < stop; index++)
            {
                var randomNumber = new Random();
                array[index] *= randomNumber.NextDouble();
            }
        }
    }
}
