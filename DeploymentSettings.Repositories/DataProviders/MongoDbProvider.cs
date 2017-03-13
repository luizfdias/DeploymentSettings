using System;
using System.Linq.Expressions;
using DeploymentSettings.Repositories.DataProviders.Interfaces;
using MongoDB.Driver;

namespace DeploymentSettings.Repositories.DataProviders
{
    public class MongoDbProvider : IDataProvider
    {
        public IMongoDatabase MongoDatabase { get; set; }

        public MongoDbProvider(IMongoDatabase mongoDatabase)
        {
            MongoDatabase = mongoDatabase;
        }

        public void AddOrUpdate<T>(T value, string collection, Expression<Func<T, bool>> filterExpression)
        {
            var result = GetValue(filterExpression, collection);

            var collectionResult = MongoDatabase.GetCollection<T>(collection);

            if (result == null)
            {                
                collectionResult.InsertOne(value);
            }
            else
            {
                collectionResult.ReplaceOne(filterExpression, value);
            }
        }

        public T GetValue<T>(Expression<Func<T, bool>> filterExpression, string collection)
        {
            var collectionResult = MongoDatabase.GetCollection<T>(collection);            
            return collectionResult.Find(filterExpression).FirstOrDefault();
        }
    }
}
