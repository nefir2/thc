using System; //cw
using System.IO; //file, directory
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
			string jsonPath = ".\\thc.json";
			ThSettings settings = FetchFile(jsonPath);
			if (settings is null)
			{
				settings = new ThSettings(Thc.ThArgMaker("6"));
				JsonSaver.MakeFile(jsonPath, settings);
			}
			if (args.Length > 0)
			{
				if (args[0].Length >= 3 && args[0].Cut(0, 3).Equals("--")) //@@@@@@@@@@ functions @@@@@@@@@
				{
					if (args.Length == 2)
					{
						switch (args[0])
						{
							case "--th":
								settings.DefaultTouhou = Thc.ThArgMaker(args[1]);
								return;
							case "--lang":
								settings.DefaultLang = Thc.JsArgMaker(args[1]);
								return;
							case "--thcrap":
								if (!Directory.Exists(args[1])) Console.WriteLine("folder not exist or path is not a folder.");
								else
								{
									if (!File.Exists(Path.Combine(args[1], "thcrap_loader.exe")) && !Directory.Exists(Path.Combine(args[1], "config"))) Console.WriteLine("not a thcrap folder.");
									else settings.ThcrapPath = args[1];
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
			string thcrapload = "thcrap_loader.exe";
			try
			{
				switch (args.Length)
				{
					case 2:
						Thc.Launch($"\"{settings.ThcrapPath}\\{thcrapload}\" {Thc.ThArgMaker(args[0])} {Thc.JsArgMaker(args[1])}");
						return;
					case 1:
						Thc.Launch($"\"{settings.ThcrapPath}\\{thcrapload}\" {Thc.ThArgMaker(args[0])} {settings.DefaultLang}");
						return;
					default:
						Thc.Launch($"\"{settings.ThcrapPath}\\{thcrapload}\" {settings.DefaultTouhou} {settings.DefaultLang}");
						//Usage();
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
//				$"\t-v - verbose do things.\n" +
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
		/// <summary>
		/// fetching settings in <paramref name="settings"/> or in <paramref name="filePath"/>.
		/// </summary>
		/// <param name="filePath">path to json file.</param>
		/// <param name="settings">object of settings.</param>
		/// <param name="verbose">if <see langword="true"/> shows comments.</param>
		/// <returns>if file was read then returns <see langword="true"/>.</returns>
		public static bool FetchSettings(string filePath, ref ThSettings settings, bool verbose = false)
		{
			bool isRead;
			if (verbose) Console.WriteLine($"checking for existing {filePath} and not nulled settings");
			if (!File.Exists(filePath) && !(settings is null))
			{
				if (verbose) Console.WriteLine($"{filePath} not exist. making {filePath} file, and pushing settings into it settings.\n\tsettings:\n{settings}");
				JsonSaver.MakeFile(filePath, settings);
				if (verbose) Console.WriteLine("done.");
				isRead = false;
			}
			else
			{
				if (verbose && settings is null) Console.WriteLine("settings is null. reading file into it.");
				else if (verbose && File.Exists(filePath)) Console.WriteLine($"{filePath} is exist. reading it into settings.");
				settings = JsonSaver.ReadFile<ThSettings>(filePath);
				if (verbose) Console.WriteLine("done.");
				isRead = true;
			}
			if (verbose) Console.WriteLine($"is read from file: {isRead}.");
			return isRead;
		}
		public static ThSettings FetchFile(string filePath)
		{
			if (!File.Exists(filePath)) return null;
			else return JsonSaver.ReadFile<ThSettings>(filePath);
		}
	}
}
