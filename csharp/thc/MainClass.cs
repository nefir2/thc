using System; //console, exception
using System.IO; //file, directory
using System.Reflection; //assembly
using System.Text.Json; //jsonexception
using thc.lib; //thc
using thc.lib.json; //thsettings, jsonsaver
namespace thc
{
	/// <summary>
	/// main class.
	/// </summary>
	internal static class MainClass
	{
		#region fields
		/// <summary>
		/// path to folder where placed this program.
		/// </summary>
		static readonly string point;
		/// <summary>
		/// name of program that must be named <c>thcrap_loader.exe</c>
		/// </summary>
		static readonly string thcrapload;
		/// <summary>
		/// name of folder for json settings.
		/// </summary>
		static readonly string settingsFolderName;
		/// <summary>
		/// path to folder with json file.
		/// </summary>
		static readonly string settingsFolderPath;
		/// <summary>
		/// path to json file with settings.
		/// </summary>
		static readonly string jsonPath;
		/// <summary>
		/// instance of <see cref="ThSettings"/> for saving in json file.
		/// </summary>
		static ThSettings settings;
		#endregion
		#region ctors
		static MainClass()
		{
			point = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			thcrapload = "thcrap_loader.exe";
			settingsFolderName = "thc config";
			settingsFolderPath = Path.Combine(point, settingsFolderName);
			jsonPath = $@"{settingsFolderPath}\{Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location)}.json";

			Directory.CreateDirectory(settingsFolderPath);
		}
		#endregion
		#region methods
		/// <summary>
		/// main method.
		/// </summary>
		/// <param name="args">args from console.</param>
		public static void Main(string[] args)
		{
			try
			{
				settings = Thc.FetchFile(jsonPath);
				if (settings is null)
				{
					settings = new ThSettings(Thc.ThArgMaker("6"), thcrapPath: point);
					JsonSaver.MakeFile(jsonPath, settings);
				}				
			}
			catch (JsonException ex)
			{
				if (!(args.Length != 0 && args[0].Equals("--repair")))
				{
					Console.WriteLine
					(
						$"json file with path {jsonPath} is corrupted.\n" +
						$"use \"{Assembly.GetExecutingAssembly().Location} --repair\" to fix json file by yourself.\n" +
						$"use \"{Assembly.GetExecutingAssembly().Location} --repair -d\" to return settings to default.\n" +
						$"\n" +
						$"exception message: {ex.Message}\n"
					);
					return;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
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
							case "--repair":
								Thc.Launch($"notepad {jsonPath}");
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
							case "--repair":
								if (args[1].Equals("-d") || args[1].Equals("-default")) JsonSaver.MakeFile(jsonPath, settings);
								else if (args[1].Equals("-e") || args[1].Equals("-empty")) JsonSaver.MakeFile(jsonPath, new ThSettings("", "", ""));
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
							for (int i = 0; i < langs.Length; i++)
							{
								if
								(
									Path.GetFileName(langs[i]) != "config.js" &&
									Path.GetFileName(langs[i]) != "games.js" &&
									Path.GetFileName(langs[i]) != "mods.js"
								) Console.WriteLine($"\t{Path.GetFileName(langs[i])}");
							}
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
				switch (args.Length) //@@@@@@@ main arguments @@@@@@@@
				{
					case 2:
						Thc.Launch($"\"{settings.ThcrapPath}\\{thcrapload}\" {Thc.ThArgMaker(args[0])} {Thc.JsArgMaker(args[1])}");
						return;
					case 1:
						Thc.Launch($"\"{settings.ThcrapPath}\\{thcrapload}\" {Thc.ThArgMaker(args[0])} {Thc.JsArgMaker(settings.DefaultLang)}");
						return;
					case 0:
						if (settings.DefaultTouhou == "")
						{
							Usage();
							return;
						}
						else Thc.Launch($"\"{settings.ThcrapPath}\\{thcrapload}\" {Thc.ThArgMaker(settings.DefaultTouhou)} {Thc.JsArgMaker(settings.DefaultLang)}");
						return;
					default:
						Usage();
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
				"Usage: " + Assembly.GetExecutingAssembly().Location + " [-{switches}|--{functions}|{th}] [lang]\n"		+
				"\topening touhou game with chosen number and default language.\n"										+
				"\tth - number of touhou game. may be like \"th06\" or like \"6\" (default=th06).\n"					+
				"\tlang - language for game from config json files (default=en.js).\n"									+
				"\n"																									+
				"switches:\n"																							+
				"\t-h[elp] - show help.\n"																				+
				"\t-s[ettings] - show settings from json file.\n"														+
				"\t-l[anguages] - show languages for thcrap_loader.exe\n"												+
				"\n"																									+
				"functions:\n"																							+
				"\t--th {num} - set number of default touhou.\n"														+
				"\t--lang[uage] {str} - set default language file.\n"													+
				"\t--thcrap {path} - set folder where can be found thcrap_loader.exe\n"									+
				"\t--test - launch thcrap_test.\n"																		+
				"\t--configure [-o[ld]] - launch thcrap_configure.\n"													+
				"\t\t-o[ld] - launch old version.\n"																	+
				"\t--repair [-d[efault]|-e[mpty]] - repair json file if it broken.\n"									+
				"\t\t-d[efault] - returns default settings.\n" +
				"\t\t-e[mpty] - fill object with empty data in json file."
			);
		}
		#endregion
	}
}