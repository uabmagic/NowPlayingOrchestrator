namespace UABMagic.Functions.NowPlayingOrchestrator.Data;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly string? _databaseConnectionString;

    public Repository()
    {
        _databaseConnectionString = Environment.GetEnvironmentVariable(Constants.DatabaseConnectionString);
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        using var sqlConnection = new SqlConnection(_databaseConnectionString);

        sqlConnection.Open();

        var sql = $"select * from {EntityTableName}";
        var result = await sqlConnection.QueryAsync<TEntity>(sql);

        return result.ToList();
    }

    public Task<TEntity> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    private string EntityTableName => $"{typeof(TEntity).Name}s";
}
