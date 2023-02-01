namespace PCConfiguratorApplication.Models.Memories
{
    using PCConfiguratorApplication.Models.Component;
    using PCConfiguratorApplication.Models.Memories.Contracts;

    public class Memory : Component, IMemory
    {
        private string type;

        public Memory(
            string componentType,
            string partNumber,
            string name,
            decimal price,
            string type)
            : base(componentType, partNumber, name, price)
        {
            this.Type = type;
        }

        public string Type
        {
            get => this.type;

            private set => this.type = value;
        }
    }
}
