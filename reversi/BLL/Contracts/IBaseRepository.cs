namespace BLL.Contracts;

public interface IBaseRepository<TEntity>
{
    void Save(TEntity config);
    TEntity Load (string id);
    List<TEntity> LoadAll ();
    void Delete(string id);
}