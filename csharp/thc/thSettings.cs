namespace thc
{
	public class ThSettings
	{
		private int defaultTh;
		private string defaultLang;
		private string thcrapPath;

		/// <summary>
		/// default number of touhou.
		/// </summary>
		public int DefaultTouhou
		{
			get => defaultTh;
			set => defaultTh = value;
		}
		/// <summary>
		/// default language json file for touhou.
		/// </summary>
		public string DefaultLang
		{
			get => defaultLang;
			set => defaultLang = value;
		}
		/// <summary>
		/// path to thcrap folder.
		/// </summary>
		public string ThcrapPath
		{
			get => thcrapPath;
			set => thcrapPath = value;
		}

		/// <summary> touhou default settings for th launch. </summary>
		/// <param name="defaultTh">default number of touhou.</param>
		/// <param name="defaultLang">default language for touhou.</param>
		/// <param name="thcrapPath">path to thcrap folder.</param>
		public ThSettings(int defaultTh, string defaultLang = "ru.js", string thcrapPath = ".\\")
		{
			this.defaultTh = defaultTh;
			this.defaultLang = defaultLang;
			this.thcrapPath = thcrapPath;
		}
	}
}
