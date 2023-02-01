namespace PCConfiguratorApplication.Models.Motherboards
{
    using PCConfiguratorApplication.Models.Component;
    using PCConfiguratorApplication.Models.Motherboards.Contracts;

    public class Motherboard : ComponentWithSocket , IMotherboard
    {
        public Motherboard(
           string componentType,
           string partNumber,
           string name,
           decimal price,
           string socket)
           : base(componentType, partNumber, name, price, socket)
        {
        }
    }
}
