using LanguageExt.Common;

namespace WeTube.DataAccess
{
    public interface ISqlConnection
    {
        Task<Result<IEnumerable<T>>> LoadData<T, U>(string sqlQuery, U parameters, string connectionId = "DefaultConnection");
        Task<Result<int>> SaveData<T>(string sqlQuery, T parameters, string connectionId = "DefaultConnection");
    }
}
