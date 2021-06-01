using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.ImplementationHR
{
	public static class BiggerIsGreaterAlgo
	{
		public static void Execute()
		{
            string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string directory = System.IO.Path.GetDirectoryName(assemblyPath);
            string tescasePath = System.IO.Path.Combine(directory, @"Test\BiggerIsGreater\Tescase1.txt");
            string resultPath = System.IO.Path.Combine(directory, @"Test\BiggerIsGreater\Result1.txt");

            string[] testcases = System.IO.File.ReadAllLines(tescasePath);
            string[] results = System.IO.File.ReadAllLines(resultPath);

            int sucessCount = 0;
            int failCount = 0;

            for (int index = 1; index < testcases.Length; index++)
			{
                string tescase = testcases[index];
                string result = ExecuteAlgo(tescase);
                string testRes = results[index - 1];

                if (result == testRes)
				{
                    sucessCount++;
				}
                else
				{
                    failCount++;
                }
			}

            Console.WriteLine("Success: " + sucessCount);
            Console.WriteLine("Fail:" + failCount);
        }

		private static string ExecuteAlgo(string word)
		{
            char[] charArray = word.ToCharArray();

            int xPoint = -1;

            for (int xCycle = charArray.Length - 1; xCycle > 0; xCycle--)
            {
                if (charArray[xCycle - 1] < charArray[xCycle])
                {
                    xPoint = xCycle - 1;
                    break;
                }
            }

            if (xPoint == -1)
            {
                return "no answer";
            }

            int yPoint = xPoint + 1;

            if (yPoint >= charArray.Length)
            {
                return "no answer";
            }

            int smallestIndex = yPoint;
            char smallestValue = charArray[smallestIndex];
            bool yPointSelected = false;

            for (int yCycle = yPoint; yCycle < charArray.Length; yCycle++)
            {
                if (charArray[xPoint] >= charArray[yCycle])
                {
                    yPoint = yCycle - 1;
                    yPointSelected = true;
                    break;
                }
            }

            if (!yPointSelected && yPoint == xPoint + 1)
            {
                for (int yCycle = yPoint; yCycle < charArray.Length; yCycle++)
                {
                    if (charArray[yCycle] <= smallestValue)
                    {
                        smallestIndex = yCycle;
                        smallestValue = charArray[yCycle];
                    }
                }
                yPoint = smallestIndex;
            }

            char charX = charArray[xPoint];
            char charY = charArray[yPoint];
            charArray[xPoint] = charY;
            charArray[yPoint] = charX;

            List<char> list1 = new List<char>();
            List<char> list2 = new List<char>();

            for (int xIt = 0; xIt <= xPoint; xIt++)
            {
                list1.Add(charArray[xIt]);
            }

            for (int yIt = xPoint + 1; yIt < charArray.Length; yIt++)
            {
                list2.Add(charArray[yIt]);
            }

            list2.Sort();
            string result = new string(list1.ToArray()) + new string(list2.ToArray());
            return result == word ? "no answer" : result;
        }
	}
}