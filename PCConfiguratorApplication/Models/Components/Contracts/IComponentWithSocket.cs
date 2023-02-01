namespace PCConfiguratorApplication.Models.Component.Contracts
{
    public interface IComponentWithSocket : IComponent
    {
        string Socket { get; }
    }
}
