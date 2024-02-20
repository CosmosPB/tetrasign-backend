using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TetraSign.Core.Domain;
using TetraSign.Core.Helpers;
using TetraSign.Core.Infraestructure;

namespace  TetraSign.Core.Infraestructure;

public class MongoRepository<TEntity, TDatabaseSettings>: IRepository<TEntity, TDatabaseSettings> 
    where TEntity: IAggregateRoot
    where TDatabaseSettings: class, IDatabaseSettings
{
    private readonly IMongoCollection<TEntity> _collection;

    public MongoRepository(IOptions<TDatabaseSettings> mongo_settings)
    {
        var mongoClient = new MongoClient(mongo_settings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongo_settings.Value.DatabaseName);
        _collection = mongoDatabase.GetCollection<TEntity>(mongo_settings.Value.CollectionName);
    }

    public async Task<IEnumerable<TEntity>> Find() => await _collection.Find(_ => true).ToListAsync();

    public async Task<TEntity> FindById(string id) => await _collection.Find(_ => _.id == id).FirstOrDefaultAsync();

    public async Task Add(TEntity entity) => await _collection.InsertOneAsync(entity);

    public async Task Update(TEntity entity) => await _collection.ReplaceOneAsync(_ => _.id == entity.id, entity);

    public async Task Remove(string id) => await _collection.DeleteOneAsync(_ => _.id == id);
}