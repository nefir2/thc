using System; //cw
using System.IO;
using System.Reflection; //assembly

namespace thc
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			string thcrap = "thcrap_loader.exe";
			string lang = "ru.js";
			try
			{
				
				switch (args.Length) //thcrap_loader.exe
				{
					case 2:
						Thc.Launch($"{thcrap} {Thc.thArgMaker(args[0])} {Thc.jsArgMaker(args[1])}");
						break;
					case 1:
						if (args[0].StartsWith("-h") || args[0].StartsWith("--h"))
						{
							Usage();
							return;
						}
						else Thc.Launch($"{thcrap} {Thc.thArgMaker(args[0])} {lang}");
						break;
					default:
						Usage();
						break;
				}
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("thcrap_loader is not found");
			}
			return;
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
