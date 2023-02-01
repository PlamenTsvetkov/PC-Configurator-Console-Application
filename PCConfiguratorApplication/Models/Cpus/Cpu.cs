namespace PCConfiguratorApplication.Models.Cpus
{
    using PCConfiguratorApplication.Models.Component;
    using PCConfiguratorApplication.Models.Cpus.Contracts;

    public class Cpu : ComponentWithSocket , ICpu
    {
        private string supportedMemory;

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

        public string SupportedMemory
        {
            get => this.supportedMemory;

            private set => this.supportedMemory = value;
        }
    }
}
