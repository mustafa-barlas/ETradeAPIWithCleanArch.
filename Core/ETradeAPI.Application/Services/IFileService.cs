﻿using Microsoft.AspNetCore.Http;

namespace ETradeAPI.Application.Services;

public interface IFileService         ////  DEPRECATED
{
    Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files);


    Task<bool> CopyFileAsync(string path, IFormFile file);
}

// Task<(string fileName,string path)> uploadAsync ();