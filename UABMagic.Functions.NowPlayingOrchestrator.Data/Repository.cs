namespace UABMagic.Functions.NowPlayingOrchestrator.Data;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : UABEntity, new()
{
    private readonly string _supabaseKey;
    private readonly string _supabaseUrl;

    public Repository()
    {
        _supabaseKey = Environment.GetEnvironmentVariable(Constants.SupabaseKey) ?? string.Empty;
        _supabaseUrl = Environment.GetEnvironmentVariable(Constants.SupabaseURL) ?? string.Empty;
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        var client = new Supabase.Client(_supabaseUrl, _supabaseKey);

        await client.InitializeAsync();

        var results = await client.From<TEntity>().Get();

        return results.Models;
    }

    public async Task AddEntityAsync(TEntity entity)
    {
        if (entity is UABEntity)
        {
            entity.CreatedAt = DateTime.Now.ToUniversalTime();
        }

        var client = new Supabase.Client(_supabaseUrl, _supabaseKey);

        await client.InitializeAsync();

        await client.From<TEntity>().Insert(entity);
    }
}
