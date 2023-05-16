using System; //cw
using System.Reflection; //assembly
namespace thc
{
	/// <summary> main class. </summary>
	internal class Program
	{
		/// <summary>
		/// main method.
		/// </summary>
		/// <param name="args">args from console.</param>
		public static void Main(string[] args)
		{
			string thcrap = "thcrap_loader.exe"; //path to thcrap_loader.exe
			string lang = "ru.js"; //default language
			try
			{
				switch (args.Length)
				{
					case 2:
						Thc.Launch($"{thcrap} {Thc.ThArgMaker(args[0])} {Thc.JsArgMaker(args[1])}");
						break;
					case 1:
						if (args[0].StartsWith("-h") || args[0].StartsWith("--h"))
						{
							Usage();
							return;
						}
						else Thc.Launch($"{thcrap} {Thc.ThArgMaker(args[0])} {lang}");
						break;
					default:
						Usage();
						break;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message); //"thcrap_loader is not found"
			}
			return;
		}
		/// <summary>
		/// manual for this program.
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
	}
}
