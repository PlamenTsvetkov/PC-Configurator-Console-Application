namespace PCConfiguratorApplication.Repositories
{
    using PCConfiguratorApplication.Models.Motherboards.Contracts;
    using PCConfiguratorApplication.Repositories.Contracts;
    using PCConfiguratorApplication.Utilities;

    public class MotherboardRepository : IRepository<IMotherboard>
    {
        private readonly List<IMotherboard> models;

        public MotherboardRepository()
        {
            this.models = new List<IMotherboard>();
        }

        public IReadOnlyCollection<IMotherboard> Models => this.models.AsReadOnly();

        public void Add(IMotherboard model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidMotherboards);
            }
            this.models.Add(model);
        }

        public IMotherboard FindBy(string partNumber)
        => this.models.FirstOrDefault(c => c.PartNumber == partNumber);

        public int Lenght()
        => models.Count();

        public bool Remove(IMotherboard model)
        => this.models.Remove(model);
    }
}
