﻿using LanguageExt.Common;
using Microsoft.AspNetCore.Components.Forms;

namespace WeTube.Processors
{
    public interface IVideoFileProcessor
    {
        Result<bool> ConvertToHls(string inputPath, string outputPath);
        Task<Result<string>> UploadFile(IBrowserFile file, string userId);
    }
}
