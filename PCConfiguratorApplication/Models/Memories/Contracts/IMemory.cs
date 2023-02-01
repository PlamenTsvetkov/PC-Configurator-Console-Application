namespace PCConfiguratorApplication.Models.Memories.Contracts
{
    using PCConfiguratorApplication.Models.Component.Contracts;

    public interface IMemory : IComponent
    {
        string Type { get; }
    }
}
