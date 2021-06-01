using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.TestTools
{
	public class CalculateDataCenter
	{
		public static void Calucate()
		{
			int datacenterArea = 5000000;
			double percentageAvailable = 0.60; // percentage usable.
			int rackSize = 42;
			int serverSize = 1;

			double result = CalculateServers(datacenterArea, percentageAvailable, rackSize, serverSize);

			Console.WriteLine("Result....." + Convert.ToInt32(result));
		}

		private static double CalculateServers(int datacenterArea, double percentageAvailable, int rackSize, int serverSize)
		{
			double totalAreaAvialable = datacenterArea * percentageAvailable;
			//Console.WriteLine(totalAreaAvialable);

			int sqFeetPerTile = 4;
			double tilesInServerRoom = totalAreaAvialable / sqFeetPerTile;
			//Console.WriteLine(tilesInServerRoom);

			double ratioOccupiedTile = 28.0 / 44.0;
			//Console.WriteLine(ratioOccupiedTile);

			double totalOccupiedTile = tilesInServerRoom * ratioOccupiedTile;

			int tileSize = 2;
			int totalRacksTile = rackSize / tileSize;
			//Console.WriteLine(totalRacksTile);

			double serverLimit = totalRacksTile * totalOccupiedTile;
			//Console.WriteLine(serverLimit);

			double totalServers = serverLimit / serverSize;

			return totalServers;
		}
	}
}