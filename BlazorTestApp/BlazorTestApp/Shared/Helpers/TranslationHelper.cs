using BlazorTestApp.Shared.Resources;
using System.Globalization;
using System.Resources;

namespace BlazorTestApp.Shared.Helpers
{
	public static class TranslationHelper
	{

		/// <summary>
		/// used for change the language
		/// </summary>
		/// <param name="language"></param>
		public static void SetDefaultLanguage(string language)
		{
			if (!string.IsNullOrEmpty(language))
			{
				CultureInfo culture = new(language);
				CultureInfo.DefaultThreadCurrentCulture = culture;
				CultureInfo.DefaultThreadCurrentUICulture = culture;
			}
		}

		/// <summary>
		/// if you need or want get the translation without use the provider IStringLocalizer
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string? GetTranslationByKey(string key)
		{
			try
			{
				var rm = new ResourceManager(typeof(Resource));
				return rm.GetString(key);
			}
			catch
			{
				return key;
			}
		}
	}
}
