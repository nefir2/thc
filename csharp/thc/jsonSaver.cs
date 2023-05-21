using System.IO; //filestream
using System.Text.Json; //jsonserializer
using System.Threading.Tasks; //task<t>
namespace thc
{
	public static class JsonSaver
	{
		public static void MakeFile<T>(string path, T saveToJson)
		{
			using (FileStream x = new FileStream(path, FileMode.OpenOrCreate)) JsonSerializer.SerializeAsync(x, saveToJson, typeof(T), new JsonSerializerOptions() { WriteIndented = true });
		}
		public static async void MakeFileAsync<T>(string path, T saveToJson)
		{
			using (FileStream x = new FileStream(path, FileMode.OpenOrCreate)) await JsonSerializer.SerializeAsync(x, saveToJson, typeof(T), new JsonSerializerOptions() { WriteIndented = true });
		}
		/// <summary>
		/// reading json file to object <typeparamref name="T"/>.
		/// </summary>
		/// <remarks>
		/// if json data file is corrupted, throws <see cref="JsonException"/>.
		/// </remarks>
		/// <typeparam name="T">type of object in json file.</typeparam>
		/// <param name="path">path to json file.</param>
		/// <returns>object read in json file.</returns>
		/// <exception cref="JsonException"></exception>
		public static T ReadFile<T>(string path)
		{

			try
			{
				T ret;
				using (FileStream x = new FileStream(path, FileMode.Open))
				{
					ret = JsonSerializer.Deserialize<T>(x, new JsonSerializerOptions() { WriteIndented = true });
				}
				return ret;
			}
			catch (JsonException ex)
			{
				throw new JsonException(ex.Message, ex);
			}
		}
		/// <summary>
		/// async reading json file to object <typeparamref name="T"/>.
		/// </summary>
		/// <remarks>
		/// if json data file is corrupted, throws <see cref="JsonException"/>.
		/// </remarks>
		/// <typeparam name="T">type of object in json file.</typeparam>
		/// <param name="path">path to json file.</param>
		/// <returns>object read in json file.</returns>
		/// <exception cref="JsonException"></exception>
		public static async Task<T> ReadFileAsync<T>(string path)
		{
			try 
			{ 
				T ret;
				using (FileStream x = new FileStream(path, FileMode.Open)) ret = await JsonSerializer.DeserializeAsync<T>(x, new JsonSerializerOptions() { WriteIndented = true });
				return ret;
			}
			catch (JsonException ex)
			{
				throw new JsonException(ex.Message, ex);
			}
		}
	}
}
