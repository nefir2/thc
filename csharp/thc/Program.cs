using System; //cw
using System.IO; //file, directory
using System.Reflection; //assembly
namespace thc
{
	/// <summary> main class. </summary>
	internal static class Program
	{
		/// <summary>
		/// main method.
		/// </summary>
		/// <param name="args">args from console.</param>
		public static void Main(string[] args)
		{
			string jsonPath = $".\\{Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location)}.json";
			string thcrapload = "thcrap_loader.exe";
			ThSettings settings = FetchFile(jsonPath);
			if (settings is null)
			{
				settings = new ThSettings(Thc.ThArgMaker("6"));
				JsonSaver.MakeFile(jsonPath, settings);
			}
			if (args.Length > 0)
			{
				if (args[0].Length >= 2 && args[0].Cut(0, 2).Equals("--")) //@@@@@@@@@@ functions @@@@@@@@@
				{
					if (args.Length == 2)
					{
						switch (args[0])
						{
							case "--th":
								settings.DefaultTouhou = Thc.ThArgMaker(args[1]);
								JsonSaver.MakeFile(jsonPath, settings);
								return;
							case "--lang":
								settings.DefaultLang = Thc.JsArgMaker(args[1]);
								JsonSaver.MakeFile(jsonPath, settings);
								return;
							case "--thcrap":
								if (!Directory.Exists(args[1])) Console.WriteLine("folder not exist or path is not a folder.");
								else
								{
									if (!File.Exists(Path.Combine(args[1], thcrapload)) && !Directory.Exists(Path.Combine(args[1], "config"))) Console.WriteLine("not a thcrap folder.");
									else
									{
										settings.ThcrapPath = args[1];
										JsonSaver.MakeFile(jsonPath, settings);
									}
								}
								return;
							default:
								Usage();
								return;
						}
					}
					else
					{
						Usage();
						return;
					}
				}
				else if (args[0][0] == '-') //@@@@@@@ switches @@@@@@@@
				{
					switch (args[0])
					{
						case "-s":
							Console.WriteLine(settings);
							return;
						case "-h":
						default:
							Usage();
							return;
					}
				}
			}
			try
			{
				switch (args.Length)
				{
					case 2:
						Thc.Launch($"\"{settings.ThcrapPath}\\{thcrapload}\" {Thc.ThArgMaker(args[0])} {Thc.JsArgMaker(args[1])}");
						return;
					case 1:
						Thc.Launch($"\"{settings.ThcrapPath}\\{thcrapload}\" {Thc.ThArgMaker(args[0])} {Thc.JsArgMaker(settings.DefaultLang)}");
						return;
					default:
						if (settings.DefaultTouhou == "")
						{
							Usage();
							return;
						}
						else Thc.Launch($"\"{settings.ThcrapPath}\\{thcrapload}\" {Thc.ThArgMaker(settings.DefaultTouhou)} {Thc.JsArgMaker(settings.DefaultLang)}");
						return;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		/// <summary>
		/// manual for this program.
		/// </summary>
		public static void Usage()
		{
			Console.WriteLine
			(
				$"Usage: {Assembly.GetExecutingAssembly().Location} [-{{switches}}|--{{functions}}] {{th}} [lang]\n" +
				$"\topening touhou game with chosen number and default language.\n" +
				$"\tth - number of touhou game. may be like \"th06\" or like \"06\".\n" +
				$"\tlang - language for game from config json files (default=ru.js).\n" +
				$"\n" +
				$"\t\tswitches:\n" +
				$"\t-s - show settings from json file.\n" +
				$"\t-h - show help.\n" +
				$"\n" +
				$"\t\tfunctions:\n" +
				$"\t--th {{num}} - set number of default th.\n" +
				$"\t--lang {{str}} - set default language file.\n" +
				$"\t--thcrap {{path}} - set folder where can be found thcrap_loader.exe\n"
			);
		}
		/// <summary>
		/// shows and waiting for data from console.
		/// </summary>
		/// <param name="message">message why input.</param>
		/// <returns>returns inputted <see cref="string"/>.</returns>
		public static string GetString(string message)
		{
			Console.Write(message);
			return Console.ReadLine();
		}
		public static ThSettings FetchFile(string filePath)
		{
			if (!File.Exists(filePath)) return null;
			else return JsonSaver.ReadFile<ThSettings>(filePath);
		}
	}
}
