using System; //cw
using System.Reflection; //assembly

namespace thc
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			bool isExThc = thc.CheckPath("thcrap_loader.exe", out bool isl);
            Console.WriteLine
			(
				$"thcrap_loader is found: {isExThc}\n" +
				$"thcrap in . folder: {isl}"
			);
            switch (args.Length) //thcrap_loader.exe
			{
				case 2:

					break;
				case 1:
					if (args[0].StartsWith("-h") || args[0].StartsWith("--h"))
					{
						Usage();
						return;
					}
					break;
				default:
					Usage();
					return;
			}
		}
		/// <summary>
		/// shows manual for this program.
		/// </summary>
		public static void Usage()
		{
			Console.WriteLine
			(
				$"Usage: {Assembly.GetExecutingAssembly().Location} {{th}} [lang]\n" +
				$"\topening touhou game with chosen number and default language.\n" +
				$"\tth - number of touhou game. may be like \"th06\" or like \"06\".\n" +
				$"\tlang - language for game from config json files (default=ru.js)\n"
			);
		}
		/// <summary>
		/// checking path for <paramref name="file"/> in this folder or in %PATH% Variable.
		/// </summary>
		/// <param name="file">file to check</param>
		/// <param name="isLocal">if file in local folder returns <see langword="true"/></param>
		/// <returns></returns>
	}
}
