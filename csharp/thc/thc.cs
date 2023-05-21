using System; //exception
using System.Collections.Generic; //list
using System.ComponentModel; //win32exception
using System.Diagnostics; //process
using System.IO; //filenotfoundexception
using System.Linq; //.tolist<>()
namespace thc
{
	/// <summary> class that reading and parsing console input. </summary>
	public static class Thc
	{
		/// <summary>
		/// cut in substring to first <paramref name="symbol"/> in <paramref name="str"/>. <paramref name="firstSymbol"/> is postion of <paramref name="symbol"/>. 
		/// </summary>
		/// <param name="str">string to cut.</param>
		/// <param name="symbol">cutting till this symbol in <paramref name="str"/> in returning value.</param>
		/// <param name="firstSymbol">position of this <paramref name="symbol"/>.</param>
		/// <returns><paramref name="str"/> cutted till <paramref name="symbol"/>.</returns>
		public static string CutToFirst(this string str, char symbol, out int firstSymbol)
		{
			string ret = "";
			firstSymbol = -1;
			for (int i = 0; i < str.Length; i++)
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
			if (numberOfTh.StartsWith("th")) return numberOfTh;
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
		/// removes element from array and returns this array.
		/// </summary>
		/// <typeparam name="T">type of array's elements.</typeparam>
		/// <param name="array">the array to remove the element from.</param>
		/// <param name="value">element of array, that must disappear.</param>
		/// <returns>array without <paramref name="value"/>.</returns>
		public static T[] DeleteValue<T>(this T[] array, T value)
		{
			List<T> x = array.ToList();
			x.Remove(value);
			return x.ToArray();
		}
		public static string DeleteValue(this string str, char value)
		{
			List<char> x = str.ToList();
			x.Remove(value);
			return x.ToArray().CharArrayToString(); //.ToString();
		}
		/// <summary>
		/// метод вырезающий подстроку из общей строки по указанным позициям.
		/// </summary>
		/// <remarks>
		/// <paramref name="start"/>: знак под указанным номером возвращается в составе подстроки. <br/>
		/// <paramref name="end"/>: знак под указанным номером не возвращается в составе подстроки.
		/// </remarks>
		/// <param name="value">строка, из которой вырезается подстрока.</param>
		/// <param name="start">точка начала выреза.</param>
		/// <param name="end">точка окончания выреза.</param>
		/// <returns>вырезанная подстрока типа <see cref="string"/>.</returns>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="IndexOutOfRangeException"/>
		public static string Cut(this string value, int start, int end)
		{
			if (start > end) throw new ArgumentException("начальная позиция для вырезания подстроки не может быть больше конечной позиции.");
			if (start < 0 || end < 0) throw new IndexOutOfRangeException("начало или конец вырезания подстроки не может быть меньше нуля.");
			if (start > value.Length || end > value.Length) throw new IndexOutOfRangeException("начало или конец вырезания подстроки не может быть больше длины строки.");

			string cutted = "";
			for (int i = start; i < end; i++) cutted += value[i];
			return cutted;
		}
		public static string CharArrayToString(this char[] chars)
		{
			string ret = "";
			for (int i = 0; i < chars.Length; i++) ret += chars[i];
			return ret;
		}
		public static int CountOfChars(this string str, char value)
		{
			int c = 0;
			for (int i = 0; i < str.Length; i++) if (str[i] == value) c++;
			return c;
		}
	}
}
