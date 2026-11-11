namespace BLL.Contracts;

public interface IBaseRepository<TEntity>
    where TEntity : IBaseEntity
{
    void Save(TEntity entity);
    TEntity? Load(Guid id);
    List<TEntity> LoadAll();
    void Delete(Guid id);
}