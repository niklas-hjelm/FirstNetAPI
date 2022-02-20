using FirstNetAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FirstNetAPI.DAL
{
    public class AnimalDocRepository
    {
        private readonly IMongoDatabase _database;

        public AnimalDocRepository()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false");
            var client = new MongoClient(settings);
            _database = client.GetDatabase("AnimalDb");
        }

        public void Create(AnimalDoc? animal)
        {
            var collection = _database.GetCollection<AnimalDoc>("animals");

            if (animal != null) collection.InsertOne(animal);
        }

        public void Delete(string id)
        {
            var collection = _database.GetCollection<AnimalDoc>("animals");

            collection.DeleteOne(a => a.Id.ToString() == id);
        }

        public List<AnimalDoc> GetAll()
        {
            var collection = _database.GetCollection<AnimalDoc>("animals");

            var allDocuments = collection.Find(_ => true).ToList();

            return allDocuments;
        }

        public AnimalDoc? GetById(string id)
        {
            var collection = _database.GetCollection<AnimalDoc>("animals");

            var doc = collection.Find(a => a.Id.ToString() == id).ToList();

            return doc.FirstOrDefault();
        }

        public void Update(AnimalDoc animal)
        {
            var animalCollection = _database.GetCollection<AnimalDoc>("animals");

            var filterDefinition = Builders<AnimalDoc>.Filter.Eq(b => b.Id, animal.Id);
            var updateDefinition = Builders<AnimalDoc>.Update
                .Set(a => a.Name, animal.Name)
                .Set(a => a.Type, animal.Type);

            animalCollection.UpdateOne(filterDefinition, updateDefinition);
        }
    }
}
