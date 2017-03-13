using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using DeploymentSettings.Repositories;
using DeploymentSettings.Repositories.Interfaces;
using DeploymentSettings.Repositories.DataProviders.Interfaces;
using DeploymentSettings.Repositories.DataProviders;
using System.Runtime.Caching;
using MongoDB.Driver;
using System.Security.Authentication;

namespace DeploymentSettings.WebApi.Modules
{
    public class RepositoriesModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SettingsRepository>().As<ISettingsRepository>();
            
            builder.Register(c => new CacheProvider(MemoryCache.Default, DateTime.Now.AddDays(5))).As<ICacheProvider>().SingleInstance();
            builder.Register(c => new MongoDbProvider(GetMongoClient())).As<IDataProvider>();

            base.Load(builder);
        }

        public IMongoDatabase GetMongoClient()
        {
            string connectionString = @"mongodb://balneabilidadedb:G4b63lVz94hIjIa73QDqLX1UriDgdZazplaCmbHLAvyZ7e5c1dnpL77rGRMeUWKEO048tYQ2PWxCG8nozOaQKg==@balneabilidadedb.documents.azure.com:10250/?ssl=true&sslverifycertificate=false";
            var settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);

            return mongoClient.GetDatabase("Settingsdb");            
        }
    }
}