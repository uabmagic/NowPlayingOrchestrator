namespace UABMagic.Functions.NowPlayingOrchestrator.Data.Interfaces;

public interface IRepository<TEntity> where TEntity : UABEntity, new()
{
    Task<IReadOnlyList<TEntity>> GetAllAsync();

    Task AddEntityAsync(TEntity entity);
}
