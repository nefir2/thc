namespace thc
{
	public class ThSettings
	{
		/// <summary>
		/// default number of touhou.
		/// </summary>
		public string DefaultTouhou { get; set; }
		/// <summary>
		/// default language json file for touhou.
		/// </summary>
		public string DefaultLang{ get; set; }
		/// <summary>
		/// path to thcrap folder.
		/// </summary>
		public string ThcrapPath { get; set; }

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
		public override string ToString() => $"default touhou number: {DefaultTouhou}\ndefault language: {DefaultLang}\nthcrap folder path: {ThcrapPath}";
	}
}
