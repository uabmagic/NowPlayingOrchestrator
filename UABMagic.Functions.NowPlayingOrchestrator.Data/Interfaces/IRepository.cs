namespace UABMagic.Functions.NowPlayingOrchestrator.Data.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseModel, new()
{
    Task<IReadOnlyList<TEntity>> GetAllAsync();
}
