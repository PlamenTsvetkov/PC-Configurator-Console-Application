namespace PCConfiguratorApplication.Models.Configurations
{
    using PCConfiguratorApplication.Models.Configurations.Contracts;
    using PCConfiguratorApplication.Models.Cpus.Contracts;
    using PCConfiguratorApplication.Models.Memories.Contracts;
    using PCConfiguratorApplication.Models.Motherboards.Contracts;

    public class Configuration : IConfiguration
    {
        private ICpu cpu;
        private IMemory memory;
        private IMotherboard motherboard;
        

        public Configuration(int id, ICpu cpu, IMemory memory, IMotherboard motherboard)
        {
            this.Id = id;
            this.Cpu = cpu;
            this.Memory = memory;
            this.Motherboard = motherboard;
        }
        public int Id { get; private set; }

        public ICpu Cpu
        {
            get => this.cpu;

            private set
            {
                this.cpu = value;
            }
        }

        public IMemory Memory
        {
            get => this.memory;

            private set
            {
                this.memory = value;
            }
        }

        public IMotherboard Motherboard
        {
            get => this.motherboard;

            private set
            {
                this.motherboard = value;
            }
        }

        public decimal Price => CalculatePrice();

        private decimal CalculatePrice()
        {
            return this.cpu.Price + this.memory.Price + this.motherboard.Price;
        }


        public override string ToString()
        {
            return $"Configuration N:{Id} {Environment.NewLine}{this.Cpu.ToString()} + {Environment.NewLine}{this.Memory.ToString()} + {Environment.NewLine}{this.Motherboard.ToString()} {Environment.NewLine}Price: {Price} {Environment.NewLine}-----------------";
        }

    }
}
