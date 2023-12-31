﻿using LanguageExt.Common;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics;

namespace WeTube.Processors;

public class VideoFileProcessor(IConfiguration config) : IVideoFileProcessor
{
    public async Task<Result<string>> UploadFile(IBrowserFile file, string userId)
    {
        var randFileName = Path.GetRandomFileName();
        string newfile = Path.ChangeExtension(
            randFileName,
            Path.GetExtension(file.Name));

        string newFileName = $"{Path.GetFileNameWithoutExtension(file.Name)}|{newfile}";

        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(newFileName);

        string storagePath = config.GetValue<string>("VideoStorage")!;

        if (string.IsNullOrWhiteSpace(storagePath))
        {
            return new(new Exception("Storage path was not valid."));
        }

        string path = Path.Combine(storagePath, userId, newFileName);

        if (!Directory.Exists(Path.Combine(storagePath, userId, "hls", fileNameWithoutExtension)))
            Directory.CreateDirectory(Path.Combine(storagePath, userId, "hls", fileNameWithoutExtension));

        await using FileStream fs = new(path, FileMode.Create);
        await file.OpenReadStream(4000000000).CopyToAsync(fs);

        var outputPath = Path.Combine(storagePath, userId, "hls", fileNameWithoutExtension, fileNameWithoutExtension);

        var result = ConvertToHls(
            Path.Combine(storagePath, userId, newFileName),
            Path.Combine(storagePath, userId, "hls", fileNameWithoutExtension, fileNameWithoutExtension));

        return result.Match<Result<string>>(
            ok => new($"{outputPath}.m3u8"),
            err => new(err));
    }

    public Result<bool> ConvertToHls(string inputPath, string outputPath)
    {
        var arguments =
            $"-i \"{inputPath}\" -c:v libx264 -b:v 1M -hls_time 10 -hls_list_size 0 -hls_segment_filename \"{outputPath}%03d.ts\" \"{outputPath}.m3u8\"";

        try
        {
            Parallel.Invoke(
                () =>
                {
                    using Process? process = Process.Start(
                        new ProcessStartInfo
                        {
                            FileName = "ffmpeg",
                            Arguments = arguments,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            UseShellExecute = false,
                            CreateNoWindow = true,
                        });
                });

            return new(true);
        }
        catch (Exception ex)
        {
            return new(new Exception($"Video was not processed, Error: {ex.Message}"));
        }
    }
}