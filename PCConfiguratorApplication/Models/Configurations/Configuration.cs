namespace PCConfiguratorApplication.Models.Configurations
{
    using PCConfiguratorApplication.Models.Configurations.Contracts;
    using PCConfiguratorApplication.Models.Cpus.Contracts;
    using PCConfiguratorApplication.Models.Memories.Contracts;
    using PCConfiguratorApplication.Models.Motherboards.Contracts;
    using PCConfiguratorApplication.Utilities;

    public class Configuration : IConfiguration
    {
        private ICpu cpu;
        private IMemory memory;
        private IMotherboard motherboard;

        public Configuration(ICpu cpu, IMemory memory, IMotherboard motherboard)
        {
            this.Cpu = cpu;
            this.Memory = memory;
            this.Motherboard = motherboard;
        }

        public ICpu Cpu
        {
            get => this.cpu;

            private set
            {
                if (this.Cpu == null)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCpu);
                }

                this.cpu = value;
            }
        }

        public IMemory Memory
        {
            get => this.memory;

            private set
            {
                if (this.Memory == null)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidMemory);
                }

                this.memory = value;
            }
        }

        public IMotherboard Motherboard
        {
            get => this.motherboard;

            private set
            {
                if (this.Motherboard == null)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidMotherboards);
                }

                this.motherboard = value;
            }
        }

        public decimal Price => CalculatePrice();

        private decimal CalculatePrice()
        {
            return this.cpu.Price + this.memory.Price+this.motherboard.Price;
        }

    }
}
