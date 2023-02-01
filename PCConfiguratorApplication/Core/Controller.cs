namespace PCConfiguratorApplication.Core
{
    using Newtonsoft.Json;
    using PCConfiguratorApplication.Core.Contracts;
    using PCConfiguratorApplication.DataProessors.ImportDto;
    using PCConfiguratorApplication.Models.Configurations;
    using PCConfiguratorApplication.Models.Configurations.Contracts;
    using PCConfiguratorApplication.Models.Cpus;
    using PCConfiguratorApplication.Models.Cpus.Contracts;
    using PCConfiguratorApplication.Models.Memories;
    using PCConfiguratorApplication.Models.Memories.Contracts;
    using PCConfiguratorApplication.Models.Motherboards;
    using PCConfiguratorApplication.Models.Motherboards.Contracts;
    using PCConfiguratorApplication.Repositories;
    using System.Text;

    public class Controller : IController
    {
        private CpuRepository cpus;
        private MemoryRepository memories;
        private MotherboardRepository motherboards;
        private ConfigurationRepository configurations;

        public Controller()
        {
            this.cpus = new CpuRepository();
            this.memories = new MemoryRepository();
            this.motherboards = new MotherboardRepository();
            this.configurations = new ConfigurationRepository();
        }

        public void AddCpu(string componentType, string partNumber, string name, string supportMemory, string socket, decimal price)
        {
            ICpu cpu;

            cpu = new Cpu(componentType, partNumber, name, price, socket, supportMemory);

            cpus.Add(cpu);
        }

        public void AddMemory(string componentType, string partNumber, string name, string type, decimal price)
        {
            IMemory memory;

            memory = new Memory(componentType, partNumber, name, price,type);

            memories.Add(memory);
        }

        public void AddMotherboard(string componentType, string partNumber, string name, string socket, decimal price)
        {
            IMotherboard motherboard;

            motherboard = new Motherboard(componentType, partNumber, name, price, socket);

            motherboards.Add(motherboard);
        }

        public void AddConfiguration(Cpu cpu, Memory memory, Motherboard motherboard)
        {
            IConfiguration configuration;
            
            configuration= new Configuration(cpu,memory,motherboard);

            configurations.Add(configuration);
        }

        public void LoadInventory()
        {
            var data = new Items();
            var projectDir = GetProjectDirectory();
            var baseDir = projectDir + @"Datasets/pc-store-inventory.json";

            using (StreamReader r = new StreamReader(baseDir))
            {
                string json = r.ReadToEnd();
                data = JsonConvert.DeserializeObject<Items>(json);
            }

            foreach (var item in data.CPUs)
            {
               AddCpu(item.ComponentType,item.PartNumber,item.Name,item.SupportedMemory,item.Socket, item.Price);
            }

            foreach (var item in data.Memory)
            {
                AddMemory(item.ComponentType, item.PartNumber, item.Name, item.Type, item.Price);
            }

            foreach (var item in data.Motherboards)
            {
                AddMotherboard(item.ComponentType, item.PartNumber, item.Name,  item.Socket, item.Price);
            }
        }

        static string GetProjectDirectory()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var directoryName = Path.GetFileName(currentDirectory);
            var relativePath = directoryName.StartsWith("netcoreapp") ? @"../../../" : string.Empty;

            return relativePath;
        }

        public string Intro()
        {
            var sb = new StringBuilder();

            if (cpus.Lenght()>0)
            {
                sb.AppendLine("CPUs:");
                foreach (var item in cpus.Models)
                {
                    sb.AppendLine(item.ToString());
                }
            }
            if (memories.Lenght() > 0)
            {
                sb.AppendLine("Memory:");
                foreach (var item in memories.Models)
                {
                    sb.AppendLine(item.ToString());
                }
            }
            if (motherboards.Lenght() > 0)
            {
                sb.AppendLine("Motherboards:");
                foreach (var item in motherboards.Models)
                {
                    sb.AppendLine(item.ToString());
                }
            }
            sb.Append("Please enter part number(s): ");
            return sb.ToString().TrimEnd();
        }
    }
}
