namespace UABMagic.Functions.NowPlayingOrchestrator.Data.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(int id);
    Task<IReadOnlyList<TEntity>> GetAllAsync();
}
