using ETradeAPI.Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ETradeAPI.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
    {
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        List<(string fileName, string path)> datas = new();
        List<bool> results = new();

        foreach (IFormFile file in files)
        {
            string newFileName = await FileReNameAsync(file.FileName);
            bool result = await CopyFileAsync($"{uploadPath}\\{newFileName}", file);
            datas.Add((newFileName, $"{uploadPath}\\{newFileName}"));
            results.Add(result);
        }

        if (results.TrueForAll(r => r.Equals(true)))
        {
            return datas;
        }

        //todo  eğer yukarıdaki if ğereli değilse  onun burda hata kontrolü yapılacak

        return null;
    }


    public Task<string> FileReNameAsync(string fileName)
    {
        
    }

    public async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
        try
        {
            await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

            await fileStream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}