using System; //console, exception
using System.IO; //file, directory
using System.Reflection; //assembly
using System.Text.Json; //jsonexception
using thc.lib; //thc, itouhousettings
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
		/// instance of <see cref="ITHCSettings"/> for saving in json file.
		/// </summary>
		static ITHCSettings settings;
		/// <summary>
		/// path to folder where placed this program.
		/// </summary>
		static readonly string point;
		/// <summary>
		/// name of program that help edit json file when it need to be repaired.
		/// </summary>
		static readonly string editor;
		/// <summary>
		/// name of folder bin in thcrap folder.
		/// </summary>
		static readonly string binName;
		/// <summary>
		/// name of program that testing dependecies in thcrap/bin folder.
		/// </summary>
		static readonly string testName;
		/// <summary>
		/// path to json file with settings.
		/// </summary>
		static readonly string jsonPath;
		/// <summary>
		/// name of program that must be named <c>thcrap_loader.exe</c>
		/// </summary>
		static readonly string thcrapload;
		/// <summary>
		/// name of program for configuration thcrap (old version) in thcrap/bin folder.
		/// </summary>
		static readonly string configureName;
		/// <summary>
		/// array of js(on) files that is not a languages.
		/// </summary>
		static readonly string[] notLangNames;
		/// <summary>
		/// name of program for configuration thcrap (new version) in thcrap/bin folder.
		/// </summary>
		static readonly string configureV3Name;
		/// <summary>
		/// name of config folder in thcrap folder.
		/// </summary>
		static readonly string configFolderName;
		/// <summary>
		/// name of folder for json settings.
		/// </summary>
		static readonly string settingsFolderName;
		/// <summary>
		/// path to folder with json file.
		/// </summary>
		static readonly string settingsFolderPath;
		#endregion
		#region ctors
		static MainClass()
		{
			editor = "notepad";
			binName = "bin";
			settings = null;
			testName = "thcrap_test.exe";
			thcrapload = "thcrap_loader.exe";
			notLangNames = new string[] { "config.js", "games.js", "mods.js" };
			configureName = "thcrap_configure.exe";
			configureV3Name = "thcrap_configure_v3.exe";
			configFolderName = "config";

			point = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			settingsFolderName = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location) + " config";
			settingsFolderPath = Path.Combine(point, settingsFolderName);
			jsonPath = $"{Path.Combine(settingsFolderPath, Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location))}.json";

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
			if (args.Length == 0 && !args[0].Equals("--repair"))
			{
				if (!SetSettings()) return;
			}

			if (!IsThcrapDirectory(settings.ThcrapPath)) Console.WriteLine("not a thcrap folder.\nuse \"thc.exe --thcrap {path}\" to configure path to thcrap folder.");

			if (args.Length > 0)
			{
				if (args[0].Length >= 2 && args[0].Cut(0, 2).Equals("--")) //@@@@@@@@@@ functions @@@@@@@@@
				{
					if (args.Length == 1) //function without additional arguments
					{
						switch (args[0])
						{
							case "--configure":
								Thc.Launch(Path.Combine(settings.ThcrapPath, binName, configureV3Name));
								return;
							case "--test":
								Thc.Launch(Path.Combine(settings.ThcrapPath, binName, testName));
								return;
							case "--repair":
								Thc.Launch($"{editor} {jsonPath}");
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
								if (Directory.Exists(args[1]))
								{
									if (File.Exists(Path.Combine(args[1], thcrapload)) && Directory.Exists(Path.Combine(args[1], configFolderName)))
									{
										settings.ThcrapPath = args[1];
										JsonSaver.MakeFile(jsonPath, settings);
									}
									else Console.WriteLine("not a thcrap folder.");
								}
								else Console.WriteLine("folder not exist or path is not a folder.");
								return;
							case "--configure":
								if (args[1].Equals("-o") || args[1].Equals("-old")) Thc.Launch(Path.Combine(settings.ThcrapPath, binName, configureName));
								else Usage();
								return;
							case "--repair":
								if (args[1].Equals("-d") || args[1].Equals("-default"))
								{
									File.Delete(jsonPath);
									JsonSaver.MakeFile(jsonPath, new ThSettings(Thc.ThArgMaker("6"), thcrapPath: point));
								}
								else if (args[1].Equals("-e") || args[1].Equals("-empty"))
								{
									File.Delete(jsonPath);
									JsonSaver.MakeFile(jsonPath, new ThSettings("", "", ""));
								}
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
							string[] langs = Directory.GetFiles(Path.Combine(settings.ThcrapPath, configFolderName));
							Console.WriteLine("languages:");
							for (int i = 0; i < langs.Length; i++)
							{
								bool toNext = false;
								foreach (string notLang in notLangNames)
								{
									if (Path.GetFileName(langs[i]) == notLang)
									{
										toNext = true;
										break;
									}
								}
								if (!toNext) Console.WriteLine("\t" + Path.GetFileName(langs[i]));
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

			Launcher(args);
		}
		/// <summary>
		/// manual for this program.
		/// </summary>
		public static void Usage()
		{
			Console.WriteLine
			(
				"Usage: " + Assembly.GetExecutingAssembly().Location + " [-{switches}|--{functions} [data]|{th} [lang]]\n"		+
				"\topening touhou game with chosen number and default language.\n"												+
				"\tth - number of touhou game. may be like \"th06\" or like \"6\" (default=th06).\n"							+
				"\tlang - language for game from config json files (default=en.js).\n"											+
				"\n"																											+
				"switches:\n"																									+
				"\t-h[elp] - show help.\n"																						+
				"\t-s[ettings] - show settings from json file.\n"																+
				"\t-l[anguages] - show languages for thcrap_loader.exe\n"														+
				"\n"																											+
				"functions:\n"																									+
				"\t--th {num} - set number of default touhou.\n"																+
				"\t--lang[uage] {str} - set default language file.\n"															+
				"\t--thcrap {path} - set folder where can be found thcrap_loader.exe\n"											+
				"\t--test - launch thcrap_test.exe\n"																			+
				"\t--configure [-o[ld]] - launch thcrap_configure.exe\n"														+
				"\t\t-o[ld] - launch old version.\n"																			+
				"\t--repair [-d[efault]|-e[mpty]] - repair json file if it broken.\n"											+
				"\t\t-d[efault] - returns default settings.\n"																	+
				"\t\t-e[mpty] - fill object with empty data in json file."
			);
		}
		/// <summary>
		/// set <see cref="settings"/> variable.
		/// </summary>
		/// <returns>returns <see langword="true"/> if code is exited without exceptions.</returns>
		public static bool SetSettings()
		{
			try
			{
				settings = Thc.FetchFile(jsonPath);
				if (!(settings is ITHCSettings))
				{
					settings = new ThSettings(Thc.ThArgMaker("6"), thcrapPath: point);
					JsonSaver.MakeFile(jsonPath, settings);
				}
				return true;
			}
			catch (JsonException ex)
			{
				Console.WriteLine
				(
					$"json file with path {jsonPath} is corrupted.\n" +
					$"use \"{Assembly.GetExecutingAssembly().Location} --repair\" to fix json file by yourself.\n" +
					$"use \"{Assembly.GetExecutingAssembly().Location} --repair -d\" to return settings to default.\n" +
					$"\n" +
					$"exception message: {ex.Message}\n"
				);
				return false;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}
		/// <summary>
		/// check is it thcrap folder.
		/// </summary>
		/// <param name="path">path to thcrap folder.</param>
		/// <returns>if it thcrap folder with <c>thcrap_loader.exe</c> and <c>config/</c> returns <see langword="true"/>.</returns>
		public static bool IsThcrapDirectory(string path) => File.Exists(Path.Combine(path, thcrapload)) && Directory.Exists(Path.Combine(path, configFolderName));
		/// <summary>
		/// launcher of touhou.
		/// </summary>
		/// <param name="arguments">checking arguments from <see cref="Main(string[])"/> if it has needed data.</param>
		public static void Launcher(string[] arguments)
		{
			try
			{
				switch (arguments.Length) //@@@@@@@ main arguments @@@@@@@@
				{
					case 2:
						Thc.Launch($"\"{Path.Combine(settings.ThcrapPath, thcrapload)}\" {Thc.ThArgMaker(arguments[0])} {Thc.JsArgMaker(arguments[1])}");
						return;
					case 1:
						Thc.Launch($"\"{Path.Combine(settings.ThcrapPath, thcrapload)}\" {Thc.ThArgMaker(arguments[0])} {Thc.JsArgMaker(settings.DefaultLang)}");
						return;
					case 0:
						if (settings.DefaultTouhou == "")
						{
							Usage();
							return;
						}
						else Thc.Launch($"\"{Path.Combine(settings.ThcrapPath, thcrapload)}\" {Thc.ThArgMaker(settings.DefaultTouhou)} {Thc.JsArgMaker(settings.DefaultLang)}");
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
		#endregion
	}
}