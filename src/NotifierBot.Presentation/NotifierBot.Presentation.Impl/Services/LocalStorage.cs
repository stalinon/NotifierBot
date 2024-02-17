using System.IO.Compression;
using System.Text;
using Microsoft.JSInterop;
using NotifierBot.Presentation.Services;

namespace NotifierBot.Presentation.Impl.Services;

/// <inheritdoc cref="ILocalStorage" />
public class LocalStorage(IJSRuntime jSRuntime) : ILocalStorage
{
	/// <inheritdoc />
	public async Task RemoveAsync(string key)
	{
		await jSRuntime.InvokeVoidAsync("localStorage.removeItem", key).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public async Task SaveStringAsync(string key, string value)
	{
		var compressedBytes = await Compressor.CompressBytesAsync(Encoding.UTF8.GetBytes(value));
		await jSRuntime.InvokeVoidAsync("localStorage.setItem", key, Convert.ToBase64String(compressedBytes)).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public async Task<string?> GetStringAsync(string key)
	{
		var str = await jSRuntime.InvokeAsync<string>("localStorage.getItem", key).ConfigureAwait(false);
		if (string.IsNullOrEmpty(str))
		{
			return null;
		}
		
		var bytes = await Compressor.DecompressBytesAsync(Convert.FromBase64String(str));
		return Encoding.UTF8.GetString(bytes);
	}

	/// <inheritdoc />
	public async Task SaveStringArrayAsync(string key, string[]? values)
	{
		await SaveStringAsync(key, values == null ? "" : string.Join('\0', values));
	}

	/// <inheritdoc />
	public async Task<string[]?> GetStringArrayAsync(string key)
	{
		var data = await GetStringAsync(key);
		return !string.IsNullOrEmpty(data) ? data.Split('\0') : null;
	}

	private static class Compressor
	{
		/// <summary>
		///		Ужать байты
		/// </summary>
		public static async Task<byte[]> CompressBytesAsync(byte[] bytes)
		{
			using var outputStream = new MemoryStream();
			await using (var compressionStream = new GZipStream(outputStream, CompressionLevel.Optimal))
			{
				await compressionStream.WriteAsync(bytes);
			}
			
			return outputStream.ToArray();
		}

		/// <summary>
		///		Расжать байты
		/// </summary>
		public static async Task<byte[]> DecompressBytesAsync(byte[] bytes)
		{
			using var inputStream = new MemoryStream(bytes);
			using var outputStream = new MemoryStream();
			await using (var compressionStream = new GZipStream(inputStream, CompressionMode.Decompress))
			{
				await compressionStream.CopyToAsync(outputStream);
			}
			
			return outputStream.ToArray();
		}
	}
}