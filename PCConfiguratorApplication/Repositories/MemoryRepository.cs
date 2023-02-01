namespace PCConfiguratorApplication.Repositories
{
    using PCConfiguratorApplication.Models.Memories.Contracts;
    using PCConfiguratorApplication.Repositories.Contracts;
    using PCConfiguratorApplication.Utilities;
    using System.Collections.Generic;

    public class MemoryRepository : IRepository<IMemory>
    {
        private readonly List<IMemory> models;

        public MemoryRepository()
        {
            this.models = new List<IMemory>();
        }

        public IReadOnlyCollection<IMemory> Models => this.models.AsReadOnly();

        public void Add(IMemory model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidMemory);
            }
            this.models.Add(model);
        }

        public IMemory FindBy(string partNumber)
        => this.models.FirstOrDefault(c => c.PartNumber == partNumber);


        public int Lenght()
        => models.Count();

        public bool Remove(IMemory model)
        => this.models.Remove(model);
    }
}
