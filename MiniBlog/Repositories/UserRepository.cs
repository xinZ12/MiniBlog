﻿using MiniBlog.Model;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> userCollection;

        public UserRepository(IMongoClient mongoClient)
        {
            this.userCollection = mongoClient.GetDatabase("MiniBlog").GetCollection<User>(User.CollectionName);
        }

        public async Task<List<User>> GetUsers() =>
            await userCollection.Find(_ => true).ToListAsync();

        public async Task<User> GetByName(string name)
        {
            return await userCollection.Find(_ => _.Name == name).FirstOrDefaultAsync();
        }

        public async Task<User> Create(User user)
        {
            await userCollection.InsertOneAsync(user);
            return await userCollection.Find(a => a.Name == user.Name).FirstAsync();
        }
    }
}
