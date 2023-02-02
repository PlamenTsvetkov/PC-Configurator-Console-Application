namespace PCConfiguratorApplication.Models.Configurations.Contracts
{
    using PCConfiguratorApplication.Models.Cpus.Contracts;
    using PCConfiguratorApplication.Models.Memories.Contracts;
    using PCConfiguratorApplication.Models.Motherboards.Contracts;

    public interface IConfiguration
    {
        int Id { get; }

        ICpu Cpu { get; }

        IMemory Memory { get; }

        IMotherboard Motherboard { get; }

        decimal Price { get; }
    }
}
