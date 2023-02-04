namespace PCConfiguratorApplication.Models.Configurations
{
    using PCConfiguratorApplication.Models.Configurations.Contracts;
    using PCConfiguratorApplication.Models.Motherboards.Contracts;
    using PCConfiguratorApplication.Models.Memories.Contracts;
    using PCConfiguratorApplication.Models.Cpus.Contracts;

    public class Configuration : IConfiguration
    {
        public Configuration(int id, ICpu cpu, IMemory memory, IMotherboard motherboard)
        {
            this.Id = id;
            this.Cpu = cpu;
            this.Memory = memory;
            this.Motherboard = motherboard;
        }
        public int Id { get; private set; }

        public ICpu Cpu { get; private set; }

        public IMemory Memory { get; private set; }

        public IMotherboard Motherboard { get; private set; }

        public decimal Price => CalculatePrice();

        private decimal CalculatePrice()
        {
            return this.Cpu.Price + this.Memory.Price + this.Motherboard.Price;
        }


        public override string ToString()
        {
            return $"Configuration N:{Id} {Environment.NewLine}{this.Cpu.ToString()} + {Environment.NewLine}{this.Memory.ToString()} + {Environment.NewLine}{this.Motherboard.ToString()} {Environment.NewLine}Price: {Price} {Environment.NewLine}-----------------";
        }

    }
}
