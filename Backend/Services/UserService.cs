using MongoDB.Driver;
using TravelerAppService.Context;
using TravelerAppService.Models;
using TravelerAppWebService.Services.Interfaces;

namespace TravelerAppService.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserService(MongoDBContext dbContext)
        {
            _userCollection = dbContext.GetCollection<User>("user");
        }

        //create
        public async Task CreateAsync(User user)
        {
            await _userCollection.InsertOneAsync(user);
        }

        //get
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var filter = Builders<User>.Filter.Empty;
            var users = await _userCollection.Find(filter).ToListAsync();
            return users;
        }

        //getById
        public async Task<User> GetByIdAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq(x => x.NationalIdentificationCard, id);
            return await _userCollection.Find(filter).FirstOrDefaultAsync();
        }

        //updateById
        public async Task UpdateAsync(string id, User model)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, id);
            await _userCollection.ReplaceOneAsync(filter, model);
        }

        //delete
        public async Task DeleteAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, id);
            await _userCollection.DeleteOneAsync(filter);
        }

    }
}
