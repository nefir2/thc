﻿using System; //cw
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
			string settingsFolderName = "thc config";
			string thcrapload = "thcrap_loader.exe";
			string point = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			Directory.CreateDirectory(Path.Combine(point, settingsFolderName));
			string jsonPath = $@"{point}\{settingsFolderName}\{Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location)}.json";

			ThSettings settings = FetchFile(jsonPath);
			if (settings is null)
			{
				settings = new ThSettings(Thc.ThArgMaker("6"), thcrapPath: point);
				JsonSaver.MakeFile(jsonPath, settings);
			}

			if (args.Length > 0)
			{
				if (args[0].Length >= 2 && args[0].Cut(0, 2).Equals("--")) //@@@@@@@@@@ functions @@@@@@@@@
				{
					if (args.Length == 1) //function without additional arguments
					{
						switch (args[0])
						{
							case "--configure":
								Thc.Launch($"{settings.ThcrapPath}\\bin\\thcrap_configure_v3.exe");
								return;
							case "--test":
								Thc.Launch($"{settings.ThcrapPath}\\bin\\thcrap_test.exe");
								return;
							default:
								Usage();
								return;
						}
					}
					else if (args.Length == 2) //function and argument for it
					{
						switch (args[0])
						{
							case "--th":
								settings.DefaultTouhou = Thc.ThArgMaker(args[1]);
								JsonSaver.MakeFile(jsonPath, settings);
								return;
							case "--lang":
							case "--language":
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
							case "--configure":
								if (args[1].Equals("-o") || args[1].Equals("-old")) Thc.Launch($"{settings.ThcrapPath}\\bin\\thcrap_configure.exe");
								else Usage();
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
						case "-settings":
							Console.WriteLine(settings);
							return;
						case "-l":
						case "-langs":
						case "-languages":
							string[] langs = Directory.GetFiles(Path.Combine(settings.ThcrapPath, "config"));
							Console.WriteLine("languages:");
							for (int i = 0; i < langs.Length; i++) if (Path.GetFileName(langs[i]) != "config.js" && Path.GetFileName(langs[i]) != "games.js" && Path.GetFileName(langs[i]) != "mods.js") Console.WriteLine($"\t{Path.GetFileName(langs[i])}");
							return;
						case "-h":
						case "-help":
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
				$"Usage: {Assembly.GetExecutingAssembly().Location} [-{{switches}}|--{{functions}}] {{th}} [lang]\n"	+
				$"\topening touhou game with chosen number and default language.\n"										+
				$"\tth - number of touhou game. may be like \"th06\" or like \"6\" (default=th06).\n"					+
				$"\tlang - language for game from config json files (default=en.js).\n"									+
				$"\n"																									+
				$"switches:\n"																							+
				$"\t-h[elp] - show help.\n"																				+
				$"\t-s[ettings] - show settings from json file.\n"														+
				$"\t-l[anguages] - show languages for thcrap_loader.\n"													+
				$"\n"																									+
				$"functions:\n"																							+
				$"\t--th {{num}} - set number of default th.\n"															+
				$"\t--lang[uage] {{str}} - set default language file.\n"														+
				$"\t--thcrap {{path}} - set folder where can be found thcrap_loader.exe\n"								+
				$"\t--test - launch thcrap_test.\n"																		+
				$"\t--configure [-o[ld]] - launch thcrap_configure. -o[ld] - launch old version.\n"
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