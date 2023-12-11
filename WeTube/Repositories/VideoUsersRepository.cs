using LanguageExt.Common;
using WeTube.DataAccess;

namespace WeTube;

public class VideoUsersRepository(ISqlConnection db)
{
    private readonly ISqlConnection _db = db;

    public async ValueTask<Result<IEnumerable<VideoUser>>> GetVideonUsers() =>
       await _db.LoadData<VideoUser, dynamic>("SELECT * FROM AspNetUsers", new { });
}
