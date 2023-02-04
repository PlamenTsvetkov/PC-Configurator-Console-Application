namespace PCConfiguratorApplication.Models.Cpus
{
    using PCConfiguratorApplication.Models.Cpus.Contracts;
    using PCConfiguratorApplication.Models.Component;

    public class Cpu : ComponentWithSocket, ICpu
    {

        public Cpu(
            string componentType,
            string partNumber,
            string name,
            decimal price,
            string socket,
            string supportedMemory)
            : base(componentType, partNumber, name, price, socket)
        {
            this.SupportedMemory = supportedMemory;
        }

        public string SupportedMemory { get; private set; }

        public override string ToString()
        {
            return base.ToString() + $"|| Supported Memory: {this.SupportedMemory}";
        }
    }
}
