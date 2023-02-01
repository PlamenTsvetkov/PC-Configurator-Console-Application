namespace PCConfiguratorApplication.Models.Configurations.Contracts
{
    using PCConfiguratorApplication.Models.Cpus.Contracts;
    using PCConfiguratorApplication.Models.Memories.Contracts;
    using PCConfiguratorApplication.Models.Motherboards.Contracts;

    public interface IConfiguration
    {
        ICpu Cpu { get; }

        IMemory Memory { get; }

        IMotherboard Motherboard { get; }

        decimal Price { get; }
    }
}
