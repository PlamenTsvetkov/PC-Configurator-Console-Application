namespace PCConfiguratorApplication.Models.Component.Contracts
{

    public interface IComponent
    {
        string ComponentType { get; }

        string PartNumber { get; }

        string Name { get; }

        decimal Price { get; }
    }
}
