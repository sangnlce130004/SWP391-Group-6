using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BikeShop.Extensions
{
	public class PhotoUpload
	{

		public static string StorageDirectoryName = "Photos";
		public static string StorageRelativePath => Path.Combine("Media", StorageDirectoryName);

		public static async Task<string> SaveToAsync(IWebHostEnvironment _environment, IFormFile validFile)
		{
			
			if(!validFile.ContentType.StartsWith("image"))
            {
				return null;
            }

			string filename = $"{DateTime.Now.Ticks}_{validFile.FileName}";
			string RelativeURL = Path.Combine(StorageRelativePath, filename);
			var filepath = Path.Combine(_environment.WebRootPath, RelativeURL);

			var folder = Path.GetDirectoryName(filepath);
			if(!Directory.Exists(folder))
            {
				Directory.CreateDirectory(folder);
            }

			using (var fileStream = new FileStream(filepath, FileMode.Create))
			{
				await validFile.CopyToAsync(fileStream);
			}

			return RelativeURL;
		}
	}
}
