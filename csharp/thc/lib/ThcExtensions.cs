using System; //exceptions
using System.Collections.Generic; //list
using System.Linq; //tolist<>()
namespace thc.lib
{
	/// <summary>
	/// extensions lib for thc lib.
	/// </summary>
	public static class ThcExtensions
	{
		#region  this.methods
		/// <summary>
		/// removes first element from array and returns this array.
		/// </summary>
		/// <typeparam name="T">type of array's elements.</typeparam>
		/// <param name="array">the array to remove the element from.</param>
		/// <param name="value">element of array, that must disappear.</param>
		/// <returns>array without first <paramref name="value"/>.</returns>
		public static T[] DeleteValue<T>(this T[] array, T value)
		{
			List<T> x = array.ToList();
			x.Remove(value);
			return x.ToArray();
		}
		/// <summary>
		/// removes first <see cref="char"/> element from <see cref="string"/> <paramref name="value"/>.
		/// </summary>
		/// <param name="str"><see cref="string"/>, where must disappear <paramref name="value"/>.</param>
		/// <param name="value">element of <paramref name="str"/>, that must disappear.</param>
		/// <returns><see cref="string"/> without first <paramref name="value"/>.</returns>
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
		/// <summary>
		/// makes from array <see cref="char"/><c>[]</c> to <see cref="string"/> object.
		/// </summary>
		/// <remarks>
		/// if <paramref name="chars"/> is empty, then returns <c>""</c>.
		/// </remarks>
		/// <param name="chars">array of <see cref="char"/><c>[]</c>.</param>
		/// <returns><see cref="char"/><c>[]</c> array as <see cref="string"/>.</returns>
		public static string CharArrayToString(this char[] chars)
		{
			string ret = "";
			for (int i = 0; i < chars.Length; i++) ret += chars[i];
			return ret;
		}
		/// <summary>
		/// returns count of specified <see cref="char"/> <paramref name="value"/>.
		/// </summary>
		/// <param name="str"><see cref="string"/> where to count <see cref="char"/> <paramref name="value"/>.</param>
		/// <param name="value"><see cref="char"/> that will be counted.</param>
		/// <returns>count of <paramref name="value"/>.</returns>
		public static int CountOfChars(this string str, char value)
		{
			int c = 0;
			for (int i = 0; i < str.Length; i++) if (str[i] == value) c++;
			return c;
		}
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
		#endregion this.
	}
}