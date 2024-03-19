﻿using Microsoft.AspNetCore.Http;

namespace ETradeAPI.Application.Services;

public interface IFileService
{
    Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files);

    Task<string> FileReNameAsync(string fileName);

    Task<bool> CopyFileAsync(string path, IFormFile file);
}

// Task<(string fileName,string path)> uploadAsync ();