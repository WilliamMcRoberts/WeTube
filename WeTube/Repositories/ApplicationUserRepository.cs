using WeTube.Data;
using WeTube.DataAccess;
using LanguageExt.Common;

namespace WeTube.Repositories;

public class ApplicationUserRepository(ISqlConnection db) : IApplicationUserRepository
{
    private readonly ISqlConnection _db = db;

    public async ValueTask<Result<IEnumerable<ApplicationUser>>> GetApplicationUsers() =>
       await _db.LoadData<ApplicationUser, dynamic>("SELECT * FROM AspNetUsers", new { });
}
