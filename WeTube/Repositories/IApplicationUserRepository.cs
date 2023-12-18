using LanguageExt;
using LanguageExt.Common;

using WeTube.Data;

namespace WeTube.Repositories;

public interface IApplicationUserRepository
{
    ValueTask<Result<IEnumerable<ApplicationUser>>> GetApplicationUsers();
    ValueTask<Option<ApplicationUser>> GetApplicationUser(string id);
}
