namespace PCConfiguratorApplication.Repositories
{
    
    using PCConfiguratorApplication.Models.Configurations.Contracts;

    public class ConfigurationRepository 
    {

        private readonly List<IConfiguration> models;

        public ConfigurationRepository()
        {
            this.models = new List<IConfiguration>();
        }

        public IReadOnlyCollection<IConfiguration> Models => this.models.AsReadOnly();

        public void Add(IConfiguration model)
        {
            this.models.Add(model);
        }

        public int Lenght()
        => models.Count();

        public bool Remove(IConfiguration model)
        => this.models.Remove(model);
    }
}
