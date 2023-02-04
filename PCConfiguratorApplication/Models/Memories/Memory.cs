namespace PCConfiguratorApplication.Models.Memories
{
    using PCConfiguratorApplication.Models.Memories.Contracts;
    using PCConfiguratorApplication.Models.Component;

    public class Memory : Component, IMemory
    {

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

        public string Type { get; private set; }

        public override string ToString()
        {
            return base.ToString() + $"|| Type: {this.Type}";
        }
    }
}
