using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    class SnakeLaddHRgraphtheory
    {
        private int[,] board = new int[10, 10] { 
                                 { 91, 92, 93, 94, 95, 96, 97, 98, 99, 100 },
                                 { 81, 82, 83, 84, 85, 86, 87, 88, 89, 90 },
                                 { 71, 72, 73, 74, 75, 76, 77, 78, 79, 80 },
                                 { 61, 62, 63, 64, 65, 66, 67, 68, 69, 70 },
                                 { 51, 52, 53, 54, 55, 56, 57, 58, 59, 60 },
                                 { 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 },
                                 { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 },
                                 { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 },
                                 { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 },
                                 { 1,  2,  3,  4,  5,  6,  7,  8,  9,  10 },
                               };

        private int pointCurrently = 1;
        private const int end = 100;

        private int[,] coordinatesLadders;
        private int[,] coordinatesSnakes;

        public SnakeLaddHRgraphtheory()
        {
            int testcase = Int32.Parse(System.Console.ReadLine());

            for(int i = 0; i < testcase; i++)
            {
                int laddersNo = Int32.Parse(System.Console.ReadLine());
                coordinatesLadders = GenerateCoordinates(laddersNo);
                //DisplayGrid(coordinatesLadders);

                int snakesNo = Int32.Parse(System.Console.ReadLine());
                coordinatesSnakes = GenerateCoordinates(snakesNo);
                //DisplayGrid(coordinatesSnakes);

                Console.WriteLine(Simulation());
                //Console.ReadKey();
            }
        }

        public int[,] GenerateCoordinates(int row)
        {
            int[,] coordinates = new int[row, 2];

            for(int rwI = 0; rwI < row; rwI++)
            {
                for(int col = 0; col < 2; col++)
                {
                    //Console.WriteLine("enetr coordinates");
                    coordinates[rwI, col] = Int32.Parse(System.Console.ReadLine());
                }
            }

            return coordinates;
        }

        public int Simulation()
        {
            int moves = 0;
            bool state = true;

            while(state)
            {
                int target = pointCurrently;

                int[] moveAblePath = ScanPathForLadders();
                bool laddersDecision = ContainsALadders(moveAblePath);

                if(laddersDecision)
                {
                    int highestLadder = FindhighestLadder(moveAblePath);
                    pointCurrently = highestLadder;
                    moves++;

                    if(pointCurrently == end)
                    {
                        state = false;
                    }

                    //Console.WriteLine(pointCurrently);
                }
                else
                {
                    int near = 100 - pointCurrently;

                    if( near < 7 && near > 0)
                    {
                        pointCurrently = pointCurrently + near;
                    }
                    else
                    {
                        int maxmove = 6;
                        bool dec = true;

                        while(dec)
                        {
                            int ahead = pointCurrently + maxmove;

                            if(ContainsSnake(ahead))
                            {
                                maxmove--;
                            }
                            else
                            {
                                pointCurrently = ahead;
                                moves++;
                                dec = false;
                            }
                        }
                    }

                    if(pointCurrently == 100)
                    {
                        state = false;
                    }
                }
            }

            return moves;
        }

        public int[] ScanPathForLadders()
        {
            int[] array = new int[6] { 0, 0, 0, 0, 0, 0 };
            int index = 0;

            int limit = pointCurrently + 6;

            for(int calpoint = pointCurrently + 1; calpoint <= limit; calpoint++)
            {
                for(int i = 0; i < coordinatesLadders.GetLength(0); i++)
                {
                    if(calpoint == coordinatesLadders[i, 0])
                    {
                        array[index] = coordinatesLadders[i, 0];
                        index++;
                    }
                }
            }

            return array;
        }

        public bool ContainsALadders(int[] array)
        {
            foreach(int element in array)
            {
                if(element != 0)
                {
                    return true;
                }
            }

            return false;
        }

        public int FindhighestLadder(int[] array)
        {
            int highestPoint = 0;
            int beginingPoint = 0;

            for(int iter = 0; iter < array.Length; iter++)
            {
                if(array[iter] == 0)
                {
                    continue;
                }

                int temp = EndofLadder(array[iter]);
                if(highestPoint < temp)
                {
                    highestPoint = temp;
                    beginingPoint = array[iter];
                }
            }

            return highestPoint;
        }

        private int EndofLadder(int bengin)
        {
            for(int i = 0; i < coordinatesLadders.Length; i++)
            {
                if(bengin == coordinatesLadders[i, 0])
                {
                    //Console.WriteLine("cbdc");
                    return coordinatesLadders[i, 1];
                }
            }

            return 0;
        }

        public bool ContainsSnake(int number)
        {
            for(int row = 0; row < coordinatesSnakes.GetLength(0); row++)
            {
                if(number == coordinatesSnakes[row, 0])
                {
                    return true;
                }
            }

            return false;
        }

        public void DisplayGrid(int[,] grid)
        {
            System.Console.WriteLine();

            for(int row = 0; row < grid.GetLength(0); row++)
            {
                for(int column = 0; column < grid.GetLength(1); column++)
                {
                    System.Console.Write(grid[row, column] + " ");
                }
                System.Console.WriteLine();
            }

            System.Console.WriteLine();
        }
    }
}
