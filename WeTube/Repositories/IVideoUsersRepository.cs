using LanguageExt.Common;
using WeTube.Models;

namespace WeTube.Repositories;

public interface IVideoUsersRepository
{
    ValueTask<Result<IEnumerable<VideoUser>>> GetVideoUsers();
}
