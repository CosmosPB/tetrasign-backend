using TetraSign.Core.Domain;
using TetraSign.Core.Helpers;

namespace TetraSign.Core.Infraestructure;

public interface IRepository<TEntity, TDatabaseSettings>  
    where TEntity: IAggregateRoot
    where TDatabaseSettings: IDatabaseSettings
{
    Task<IEnumerable<TEntity>> Find();
    Task<TEntity> FindById(string id);
    Task Add(TEntity entity);
    Task Update(TEntity entity);
    Task Remove(string id);
}