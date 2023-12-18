using LanguageExt.Common;
using WeTube.Models;

namespace WeTube.Repositories;

public interface IVideoRepository
{
    ValueTask<Result<IEnumerable<VideoModel>>> GetVideosFromVideoUserId(string userId);
    ValueTask<Result<int>> AddVideo(string userId, string videoName, string videoPath);
}
