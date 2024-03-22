﻿using ETradeAPI.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ETradeAPI.Infrastructure.Services.Storage.Local;

public class LocalStorage : ILocalStorage
{

    private readonly IWebHostEnvironment _webHostEnvironment;

    public LocalStorage(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }



    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
    {
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        List<(string fileName, string path)> datas = new();


        foreach (IFormFile file in files)
        {
            await CopyFileAsync($"{uploadPath}\\{file.FileName}", file);
            datas.Add((file.FileName, $"{path}\\{file.Name}"));

        }

        return datas;
    }

    async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
        try
        {
            await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
        }
        catch (Exception e)
        {
            throw e;
        }
    }



    public async Task DeleteAsync(string path, string fileName) => File.Delete($"{path}\\{fileName}");


    public List<string> GetFiles(string path)
    {
        DirectoryInfo directory = new DirectoryInfo(path);
        return directory.GetFiles().Select(x => x.Name).ToList();
    }


    public bool HasFile(string path, string fileName) => File.Exists($"{path}\\{fileName}");
}