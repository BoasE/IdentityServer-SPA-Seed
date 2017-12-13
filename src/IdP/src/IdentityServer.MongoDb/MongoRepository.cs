// ==========================================================================
// MongoRepository.cs
// Bus Portal (busliniensuche.de)
// ==========================================================================
// All rights reserved.
// ========================================================================== 

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;

namespace IdentityServer.MongoDb
{
    public abstract class MongoRepository<TDto, TEntity> where TEntity :class
    {
        protected IMongoDatabase Database { get; }

        protected static FilterDefinitionBuilder<TDto> Filters { get; } = Builders<TDto>.Filter;

        protected IMongoCollection<TDto> Collection { get; }

        protected MongoRepository(IMongoDatabase database, string collectionName)
        {
            Database = database;
            Collection = database.GetCollection<TDto>(collectionName);
        }

        protected virtual Task PrepareCollection()
        {
            return Task.CompletedTask;
        }

        protected async Task<IEnumerable<TEntity>> ListAsync(FilterDefinition<TDto> query)
        {
            List<TDto> dtos = await Collection.Find(query).ToListAsync();
            return dtos.Select(Mapper.Map<TDto, TEntity>).ToList();
        }

        protected async Task<TEntity> SingleAsync(FilterDefinition<TDto> query)
        {
            TDto dto = await Collection.Find(query).SingleOrDefaultAsync();
            return dto != null ? Mapper.Map<TDto, TEntity>(dto) : null;
        }
    }
}