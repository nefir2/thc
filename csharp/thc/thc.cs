using System; //exception
using System.Diagnostics; //process
using System.IO; //path, file
using System.Text; //stringbuilder

namespace thc
{
	public static class Thc
	{
		public static string CutToFirst(this string str, char symbol, out int firstSymbol)
		{
			string ret = "";
			StringBuilder strbld = new StringBuilder(str);
			firstSymbol = -1;
			for (int i = 0; i < str.Length - 1; i++)
			{
				if (str[i] != symbol) ret += str[i];
				else
				{
					firstSymbol = i;
					break;
				}
			}
			return ret;
		}
		public static bool CheckPath(string file, out bool isLocal) 
		{
			isLocal = File.Exists($".\\{file}");
			return File.Exists($"{file}") || isLocal;
			//return overall || local ? (true, overall) : (overall, local);
		}
		/// <summary>
		/// launching program with args in one <see cref="string"/> <paramref name="fileAndArgs"/>
		/// </summary>
		/// <param name="fileAndArgs">name of program to launch and his args</param>
		/// <exception cref="FileNotFoundException"/>
		public static Process Launch(string fileAndArgs)
        {
            string file = fileAndArgs.CutToFirst(' ', out int i);
            string args = fileAndArgs.Substring(i + 1);
			Process proc;
            try
			{
				proc = Process.Start(file, args);
			}
			catch (FileNotFoundException ex) 
			{
				throw new FileNotFoundException($"{file} not found.", ex);
			}
			return proc;
		}
		/// <summary>
		/// parsing string with number of touhou game.
		/// </summary>
		/// <param name="numberOfTh">number of touhou game.</param>
		/// <returns>returns <see cref="string"/> with number of touhou in format: <c>th{number}</c>.</returns>
		/// <exception cref="NumberOfThException"></exception>
		public static string ThArgMaker(string numberOfTh)
		{
			if (numberOfTh.StartsWith("th")) return numberOfTh;
			else if (int.TryParse(numberOfTh, out int i))
			{
				if (numberOfTh == "95") return $"th{095:d3}";
				else if (numberOfTh == "75") return $"th{075:d3}";
                return $"th{i:d2}";
			}
			else throw new NumberOfThException("not a number of touhou game.");
		}
		public static string JsArgMaker(string lang)
		{
			if (lang.EndsWith(".js")) return lang;
			else if (lang.Contains(".")) throw new Exception("not a .js file.");
			else return $"{lang}.js";
		}
	}

}
