using WeTube.Data;
using WeTube.DataAccess;
using LanguageExt.Common;
using LanguageExt;
using Dapper;
using static LanguageExt.Prelude;

namespace WeTube.Repositories;

public class ApplicationUserRepository(ISqlConnection db, IConfiguration configuration, ApplicationDbContext adb) : IApplicationUserRepository
{
    private readonly ISqlConnection _db = db;
    private readonly IConfiguration _config = configuration;

    public async ValueTask<Result<IEnumerable<ApplicationUser>>> GetApplicationUsers() =>
       await _db.LoadData<ApplicationUser, dynamic>("SELECT * FROM AspNetUsers", new { });


    public async ValueTask<Option<ApplicationUser>> GetApplicationUser(string id)
    {
        var user =
            await _db.LoadData<ApplicationUser, dynamic>(
                "SELECT * FROM AspNetUsers WHERE Id = @Id",
                 new
                 {
                     Id = new DbString
                     {
                         Value = id,
                         IsFixedLength = false,
                         Length = -1,
                         IsAnsi = true
                     }
                 });

        return user.Match<Option<ApplicationUser>>(
               Succ: res =>
               {
                   var u = res.FirstOrDefault();
                   return u is null ? None : Some(u);
               },
               Fail: ex => None
           );
    }
}

public interface IApplicationUserRepository
{
    ValueTask<Result<IEnumerable<ApplicationUser>>> GetApplicationUsers();
    ValueTask<Option<ApplicationUser>> GetApplicationUser(string id);

}