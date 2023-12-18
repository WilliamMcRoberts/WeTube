using Dapper;
using LanguageExt.Common;
using WeTube.DataAccess;
using WeTube.Models;

namespace WeTube.Repositories;

public class VideoRepository(ISqlConnection db) : IVideoRepository
{
    private readonly ISqlConnection _db = db;

    public async ValueTask<Result<IEnumerable<VideoModel>>> GetVideosFromVideoUserId(string userId) =>
       await _db.LoadData<VideoModel, dynamic>("SELECT * FROM Videos WHERE UserId = @UserId", new
       {
           UserId = new DbString
           {
               Value = userId,
               IsFixedLength = false,
               Length = -1,
               IsAnsi = true
           }
       });
    public async ValueTask<Result<int>> AddVideo(string userId, string videoName, string videoPath) =>
        await _db.SaveData($@"insert into Videos (UserId, VideoName, VideoPath)
                              values (@UserId, @VideoName, @VideoPath);", new
        {
            UserId = new DbString
            {
                Value = userId,
                IsFixedLength = false,
                Length = -1,
                IsAnsi = true
            },
            VideoName = new DbString
            {
                Value = videoName,
                IsFixedLength = false,
                Length = -1,
                IsAnsi = true
            },
            VideoPath = new DbString
            {
                Value = videoPath,
                IsFixedLength = false,
                Length = -1,
                IsAnsi = true
            },
        });
}

