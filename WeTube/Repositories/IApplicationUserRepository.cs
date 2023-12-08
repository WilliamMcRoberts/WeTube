using WeTube.Data;
using LanguageExt.Common;

namespace WeTube.Repositories
{
    public interface IApplicationUserRepository
    {
        ValueTask<Result<IEnumerable<ApplicationUser>>> GetApplicationUsers();
    }
}