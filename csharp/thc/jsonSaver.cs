﻿using System.Threading.Tasks; //task<t>
using System.Text.Json; //jsonserializer
using System.IO; //filestream
namespace thc
{
	public static class JsonSaver
	{
		public static void MakeFile<T>(string path, T saveToJson)
		{
			using (FileStream x = new FileStream(path, FileMode.Create)) JsonSerializer.SerializeAsync(x, saveToJson, typeof(T), new JsonSerializerOptions() { WriteIndented = true });
		}
		public static async void MakeFileAsync<T>(string path, T saveToJson)
		{
			using (FileStream x = new FileStream(path, FileMode.Create)) await JsonSerializer.SerializeAsync(x, saveToJson, typeof(T), new JsonSerializerOptions() { WriteIndented = true });
		}
		public static T ReadFile<T>(string path)
		{
			T ret;
			using (FileStream x = new FileStream(path, FileMode.Open)) ret = JsonSerializer.Deserialize<T>(x, new JsonSerializerOptions() { WriteIndented = true });
			return ret;
		}
		public static async Task<T> ReadFileAsync<T>(string path)
		{
			T ret;
			using (FileStream x = new FileStream(path, FileMode.Open)) ret = await JsonSerializer.DeserializeAsync<T>(x, new JsonSerializerOptions() { WriteIndented = true });
			return ret;
		}
	}
}