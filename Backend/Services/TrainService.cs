using MongoDB.Driver;
using TravelerAppService.Context;
using TravelerAppService.Models;
using TravelerAppWebService.Services.Interfaces;

namespace TravelerAppWebService.Services
{
    public class TrainService : ITrainService
    {
        private readonly IMongoCollection<Train> _trainCollection;

        public TrainService(MongoDBContext dbContext)
        {
            _trainCollection = dbContext.GetCollection<Train>("train");
        }

        //create
        public async Task CreateAsync(Train train)
        {
            await _trainCollection.InsertOneAsync(train);
        }

        //getAllData
        public async Task<IEnumerable<Train>> GetAllAsync()
        {
            var filter = Builders<Train>.Filter.Empty;
            var trains = await _trainCollection.Find(filter).ToListAsync();
            return trains;
        }

        //getById
        public async Task<Train> GetByIdAsync(string id)
        {
            var filter = Builders<Train>.Filter.Eq(x => x.Id, id);
            return await _trainCollection.Find(filter).FirstOrDefaultAsync();
        }

        //updateById
        public async Task UpdateAsync(string id, Train model)
        {
            var filter = Builders<Train>.Filter.Eq(x => x.Id, id);
            await _trainCollection.ReplaceOneAsync(filter, model);
        }

        //delete
        public async Task DeleteAsync(string id)
        {
            var filter = Builders<Train>.Filter.Eq(x => x.Id, id);
            await _trainCollection.DeleteOneAsync(filter);
        }
    }
}
