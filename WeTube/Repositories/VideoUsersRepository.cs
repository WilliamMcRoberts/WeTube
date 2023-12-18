using LanguageExt.Common;
using WeTube.DataAccess;
using WeTube.Models;

namespace WeTube.Repositories;

public class VideoUsersRepository(ISqlConnection db) : IVideoUsersRepository
{
    private readonly ISqlConnection _db = db;

    public async ValueTask<Result<IEnumerable<VideoUser>>> GetVideoUsers() =>
       await _db.LoadData<VideoUser, dynamic>("SELECT * FROM VideoUsers", new { });
}
