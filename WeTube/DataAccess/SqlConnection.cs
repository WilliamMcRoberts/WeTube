using Dapper;
using LanguageExt.Common;
using System.Data;
using Microsoft.Data.Sqlite;

namespace WeTube.DataAccess;

public class SqlConnection(IConfiguration configuration) : ISqlConnection
{
    private readonly IConfiguration _configuration = configuration;

    public async Task<Result<IEnumerable<T>>> LoadData<T, U>(
        string sqlQuery, U parameters, string connectionId = "DefaultConnection")
    {
        using IDbConnection connection = new Microsoft.Data.Sqlite.SqliteConnection(
                       _configuration.GetConnectionString(connectionId));

        IEnumerable<T> results = null;

        try
        {
            results = await connection.QueryAsync<T>(sqlQuery, parameters);
        }
        catch (Exception ex)
        {
            return new(ex);
        }

        return results is null ?
            new(new Exception("No data was returned."))
            : new(results);
    }

    public async Task<Result<int>> SaveData<T>(
        string sqlQuery, T parameters, string connectionId = "DefaultConnection")
    {
        using IDbConnection connection =
            new Microsoft.Data.Sqlite.SqliteConnection(
                _configuration.GetConnectionString(connectionId));

        try
        {
            var rows = await connection.ExecuteAsync(sqlQuery, parameters);
            return new(rows);
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}