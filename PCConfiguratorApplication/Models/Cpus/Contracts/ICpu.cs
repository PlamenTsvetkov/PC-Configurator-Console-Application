namespace PCConfiguratorApplication.Models.Cpus.Contracts
{
    using PCConfiguratorApplication.Models.Component.Contracts;

    public interface ICpu : IComponentWithSocket
    {
        string SupportedMemory { get; }
    }
}
