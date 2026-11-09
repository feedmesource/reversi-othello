namespace BLL.Contracts;

public interface IBaseRepository<TEntity>
{
    void Save(TEntity config);
    TEntity? Load (Guid id);
    List<TEntity> LoadAll ();
    void Delete(Guid id);
}