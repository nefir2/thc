using System; //serializable, exception
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
		public static void Launch(string fileAndArgs)
		{
			try
			{
				string file = fileAndArgs.CutToFirst(' ', out int i);
				string args = fileAndArgs.Substring(i + 1);
				Process.Start(file, args);
			}
			catch (FileNotFoundException ex) 
			{
				throw new FileNotFoundException("touhou game not found.", ex);
			}
		}
		/// <summary>
		/// parsing string with number of touhou game.
		/// </summary>
		/// <param name="numberOfTh">number of touhou game.</param>
		/// <returns>returns <see cref="string"/> with number of touhou in format: <c>th{number}</c>.</returns>
		/// <exception cref="NumberOfThException"></exception>
		public static string thArgMaker(string numberOfTh)
		{
			if (numberOfTh == "95") numberOfTh = "095";
			else if (numberOfTh == "75") numberOfTh = "075";

			if (numberOfTh.StartsWith("th")) return numberOfTh;
			else if (int.TryParse(numberOfTh, out int i)) return $"th{i:d2}";
			else throw new NumberOfThException("not a number of touhou game.");
		}
		public static string jsArgMaker(string lang)
		{
			if (lang.EndsWith(".js")) return lang;
			else if (lang.Contains(".")) throw new Exception("not a .js file.");
			else return $"{lang}.js";
		}
	}

	[Serializable] public class NumberOfThException : Exception
	{
		public NumberOfThException() { }
		public NumberOfThException(string message) : base(message) { }
		public NumberOfThException(string message, Exception inner) : base(message, inner) { }
		protected NumberOfThException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
