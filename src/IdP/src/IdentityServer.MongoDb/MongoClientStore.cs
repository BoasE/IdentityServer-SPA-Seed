// ==========================================================================
// CustomClientStore.cs
// Bus Portal (busliniensuche.de)
// ==========================================================================
// All rights reserved.
// ========================================================================== 

using System.Threading.Tasks;
using AutoMapper;
using IdentityServer.MongoDb.dto;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using MongoDB.Driver;

namespace IdentityServer.MongoDb
{
    public sealed class MongoClientStore : MongoRepository<MongoClientDto, Client>, IClientStore
    {
        public MongoClientStore(IMongoDatabase db) : base(db, "clients")
        {
        }

        public Task Add(Client client)
        {
            MongoClientDto dto = Mapper.Map<Client, MongoClientDto>(client);
            return Collection.InsertOneAsync(dto);
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            return SingleAsync(Filters.Eq(x => x.ClientId, clientId));
        }
    }
}