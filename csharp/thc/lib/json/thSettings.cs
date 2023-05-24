namespace thc.lib.json
{
	/// <summary>
	/// settings for to save as object in json file.
	/// </summary>
	public class ThSettings : ITouhouSettings
	{
		#region properties
		public string DefaultTouhou { get; set; }
		public string DefaultLang { get; set; }
		public string ThcrapPath { get; set; }
		#endregion
		#region ctors
		/// <summary> touhou default settings for launch. </summary>
		/// <param name="defaultTouhou">default number of touhou.</param>
		/// <param name="defaultLang">default language for touhou.</param>
		/// <param name="thcrapPath">path to thcrap folder.</param>
		public ThSettings(string defaultTouhou = "", string defaultLang = "en.js", string thcrapPath = ".\\")
		{
			DefaultTouhou = defaultTouhou;
			DefaultLang = defaultLang;
			ThcrapPath = thcrapPath;
		}
		#endregion
		#region override
		public override string ToString() => $"default touhou number: {DefaultTouhou}\ndefault language: {DefaultLang}\nthcrap folder path: {ThcrapPath}";
		#endregion
	}
}