namespace PCConfiguratorApplication.Repositories
{
    using PCConfiguratorApplication.Models.Cpus.Contracts;
    using PCConfiguratorApplication.Repositories.Contracts;
    using PCConfiguratorApplication.Utilities;
    using System.Collections.Generic;

    public class CpuRepository : IRepository<ICpu>
    {
        private readonly List<ICpu> models;

        public CpuRepository()
        {
            this.models = new List<ICpu>();
        }

        public IReadOnlyCollection<ICpu> Models => this.models.AsReadOnly();

        public void Add(ICpu model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidCpu);
            }
            this.models.Add(model);
        }

        public ICpu FindBy(string partNumber)
        => this.models.FirstOrDefault(c => c.PartNumber == partNumber);

        public int Lenght()
        => models.Count();

        public bool Remove(ICpu model)
        => this.models.Remove(model);
        
    }
}
