using System; //exception
using System.ComponentModel; //win32exception
using System.Diagnostics; //process
using System.IO; //filenotfoundexception
using System.Linq; //.contains(char)
using System.Text.Json; //jsonexception
using thc.lib.json; //thsettings, jsonsaver
namespace thc.lib
{
	/// <summary> 
	/// class that reading and parsing console input.
	/// </summary>
	public static class Thc
	{
		#region touhou help methods
		/// <summary>
		/// launching program with args in one <see cref="string"/> <paramref name="fileAndArgs"/>
		/// </summary>
		/// <param name="fileAndArgs">name of program to launch and his args</param>
		/// <returns>started <see cref="Process"/>.</returns>
		/// <exception cref="FileNotFoundException"/>
		/// <exception cref="Win32Exception"/>
		/// <exception cref="Exception"/>
		public static Process Launch(string fileAndArgs)
		{
			string file;
			string args;
			if (fileAndArgs.Length != 0 && fileAndArgs.StartsWith("\""))
			{
				if (fileAndArgs.CountOfChars('\"') % 2 == 1) throw new Exception($"count of '\"' is odd.");
				fileAndArgs = fileAndArgs.DeleteValue('\"');
				file = fileAndArgs.CutToFirst('\"', out int i).DeleteValue('\"');
				args = fileAndArgs.Substring(i + 1).Trim();
			}
			else if (fileAndArgs.Contains(' '))
			{ 
				file = fileAndArgs.CutToFirst(' ', out int i);
				args = fileAndArgs.Substring(i + 1);
			}
			else
			{
				file = fileAndArgs;
				args = string.Empty;
			}
			Process proc;
			try
			{
				proc = Process.Start(file, args);
			}
			catch (FileNotFoundException ex) 
			{
				throw new FileNotFoundException($"{file} not found.", ex);
			}
			catch (Win32Exception ex)
			{
				throw new Win32Exception($"{file} not found.", ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
			return proc;
		}
		/// <summary>
		/// parsing string with number of touhou game.
		/// </summary>
		/// <param name="numberOfTh">number of touhou game.</param>
		/// <returns>returns <see cref="string"/> with number of touhou in format: <c>th{number}</c>.</returns>
		/// <exception cref="Exception"></exception>
		public static string ThArgMaker(string numberOfTh)
		{
			if (numberOfTh.StartsWith("th") && int.TryParse(numberOfTh[3].ToString(), out int _)) return numberOfTh;
			else if (int.TryParse(numberOfTh, out int i))
			{
				if (i >= 75) return $"th{i:d3}";
				return $"th{i:d2}";
			}
			else throw new Exception("not a number of touhou game.");
		}
		/// <summary>
		/// parsing string with number of touhou game.
		/// </summary>
		/// <param name="numberOfTh">number of touhou game.</param>
		/// <returns>returns <see cref="string"/> with number of touhou in format: <c>th{number}</c>.</returns>
		/// <exception cref="Exception"></exception>
		public static string ThArgMaker(int numberOfTh)
		{
			try
			{
				return ThArgMaker($"{numberOfTh}");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		/// <summary>
		/// parsing string with name of lang js file for touhou.
		/// </summary>
		/// <param name="lang">language for touhou.</param>
		/// <returns><see cref="string"/> name of language in format <c>{name}.js</c></returns>
		/// <exception cref="Exception"/>
		public static string JsArgMaker(string lang)
		{
			if (lang.EndsWith(".js")) return lang;
			else if (lang.Contains(".")) throw new Exception("not a .js file.");
			else return $"{lang}.js";
		}
		/// <summary>
		/// checking for existings Json file.
		/// </summary>
		/// <remarks>
		/// if it exists, returns data from it. else returns <see langword="null"/>. <br/>
		/// if json data file corrupted, throws <see cref="JsonException"/>.
		/// </remarks>
		/// <param name="filePath">path to json file.</param>
		/// <returns>data from json file as <see cref="ThSettings"/>.</returns>
		/// <exception cref="JsonException"></exception>
		public static ThSettings FetchFile(string filePath)
		{
			try
			{
				if (!File.Exists(filePath)) return null;
				else return JsonSaver.ReadFile<ThSettings>(filePath);
			}
			catch (Exception ex)
			{
				throw new JsonException(ex.Message, ex);
			}
		}
		#endregion
	}
}