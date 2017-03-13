using MongoDB.Bson;

namespace DeploymentSettings.Repositories.Models
{
    public class SettingsModel<T>
    {
        public ObjectId Id { get; set; }

        public string Key { get; set; }

        public T Value { get; set; }

        //public string Description { get; set; }
    }    
}
