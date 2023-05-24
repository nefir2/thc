namespace thc.lib
{
	/// <summary>
	/// interface for thsettings saving.
	/// </summary>
	internal interface ITouhouSettings
	{
		/// <summary>
		/// default number of touhou.
		/// </summary>
		string DefaultTouhou { get; set; }
		/// <summary>
		/// default language for touhou.
		/// </summary>
		string DefaultLang { get; set; }
		/// <summary>
		/// default path to thcrap directory.
		/// </summary>
		string ThcrapPath { get; set; }
	}
}