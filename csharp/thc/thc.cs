using System.Diagnostics; //process
using System.IO; //path, file
using System.Text; //stringbuilder

namespace thc
{
	public static class thc
	{
		public static string CutToFirst(this string str, char symbol, out int firstSpace)
		{
			string ret = "";
			StringBuilder strbld = new StringBuilder(str);
			firstSpace = -1;
			for (int i = 0; i < str.Length - 1; i++)
			{
				if (str[i + 1] != symbol) ret += str[i];
				else
				{
					firstSpace = i + 1;
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
		public static void Launch(string fileAndArgs)
		{
			string file = fileAndArgs.CutToFirst(' ', out int i);
			string args = fileAndArgs.Substring(i + 1);
			Process.Start(file, args);
		}
	}
}
