namespace PCConfiguratorApplication.Core
{
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    using PCConfiguratorApplication.Models.Configurations;
    using PCConfiguratorApplication.Models.Configurations.Contracts;
    

    using PCConfiguratorApplication.Models.Cpus;
    using PCConfiguratorApplication.Models.Cpus.Contracts;

    using PCConfiguratorApplication.Models.Memories;
    using PCConfiguratorApplication.Models.Memories.Contracts;

    using PCConfiguratorApplication.Models.Motherboards;
    using PCConfiguratorApplication.Models.Motherboards.Contracts;

    using PCConfiguratorApplication.DataProessors.ImportDto;
    using PCConfiguratorApplication.Core.Contracts;
    using PCConfiguratorApplication.Repositories;
    using PCConfiguratorApplication.Utilities;

    public class Controller : IController
    {
        private const string ExitMessage = "If you want to exit the program, write \"Exit\" ";
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

            memory = new Memory(componentType, partNumber, name, price, type);

            memories.Add(memory);
        }

        public void AddMotherboard(string componentType, string partNumber, string name, string socket, decimal price)
        {
            IMotherboard motherboard;

            motherboard = new Motherboard(componentType, partNumber, name, price, socket);

            motherboards.Add(motherboard);
        }

        public void AddConfiguration(int id, Cpu cpu, Memory memory, Motherboard motherboard)
        {
            IConfiguration configuration;

            configuration = new Configuration(id, cpu, memory, motherboard);

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
                AddCpu(item.ComponentType, item.PartNumber, item.Name, item.SupportedMemory, item.Socket, item.Price);
            }

            foreach (var item in data.Memory)
            {
                AddMemory(item.ComponentType, item.PartNumber, item.Name, item.Type, item.Price);
            }

            foreach (var item in data.Motherboards)
            {
                AddMotherboard(item.ComponentType, item.PartNumber, item.Name, item.Socket, item.Price);
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
            sb.AppendLine("Hello,");
            sb.AppendLine("We offer the following items:");


            if (cpus.Lenght() > 0)
            {
                foreach (var item in cpus.Models)
                {
                    sb.AppendLine(item.ToString());
                }
            }
            if (memories.Lenght() > 0)
            {
                foreach (var item in memories.Models)
                {
                    sb.AppendLine(item.ToString());
                }
            }
            if (motherboards.Lenght() > 0)
            {
                foreach (var item in motherboards.Models)
                {
                    sb.AppendLine(item.ToString());
                }
            }
            sb.AppendLine(ExitMessage);
            sb.AppendLine("If you want all possible configurations, write \"Configurations\" оr enter part number(s) in format : \"CPU part numer, DDR Memory part number, Motherboard part number\"");
            return sb.ToString().TrimEnd();
        }

        public void GenerateConigurations()
        {
            foreach (var cpu in cpus.Models)
            {
                foreach (var memory in memories.Models)
                {
                    if (cpu.SupportedMemory != memory.Type)
                    {
                        continue;
                    }
                    foreach (var motherboard in motherboards.Models)
                    {
                        if (cpu.SupportedMemory == memory.Type && cpu.Socket == motherboard.Socket)
                        {
                            IConfiguration configuration = new Configuration(configurations.Lenght() + 1, cpu, memory, motherboard);

                            configurations.Add(configuration);
                        }
                    }
                }
            }
        }

        public string GetConigurations()
        {
            var sb = new StringBuilder();

            if (configurations.Lenght() > 0)
            {
                sb.AppendLine("All possible configurations are as follows:");
                foreach (var configuration in configurations.Models)
                {
                    sb.AppendLine(configuration.ToString()); ;
                }
            }
            sb.AppendLine(ExitMessage);
            sb.AppendLine("Please enter configuration number: ");

            return sb.ToString().TrimEnd();
        }

        public string BuyConfiguration(int id)
        {
            var configuration = configurations.FindBy(id);
            if (configuration==null)
            {
                return "Error";
            }
            var sb = new StringBuilder();
            sb.AppendLine($"Do you really want to bay {configuration.ToString()} (\"Yes\" / \"Exit\")");
            return sb.ToString().TrimEnd();
        }

        public string ValidateFullList(string[] input,  bool isOrderCompleted)
        {
            var sb = new StringBuilder();
           
            if (configurations.Models.Any(c=>c.Cpu.PartNumber == input[0] && c.Memory.PartNumber == input[1] && c.Motherboard.PartNumber == input[2]))
            {
                var item = configurations.Models.FirstOrDefault(c => c.Cpu.PartNumber == input[0] && c.Memory.PartNumber == input[1] && c.Motherboard.PartNumber == input[2]);
                //sb.AppendLine(String.Format(SuccessMessages.SuccesBuyWithFullList,configuration.Price));

                sb.AppendLine($"Your Configuration");
                sb.AppendLine($"{item.Cpu.ComponentType} - {item.Cpu.Name} - {item.Cpu.Socket} - {item.Cpu.SupportedMemory} - {item.Cpu.PartNumber}");
                sb.AppendLine($"{item.Memory.ComponentType} - {item.Memory.Name} - {item.Memory.Type} - {item.Memory.PartNumber}");
                sb.AppendLine($"{item.Motherboard.ComponentType} - {item.Motherboard.Name} - {item.Motherboard.Socket} - {item.Motherboard.PartNumber}");
                sb.AppendLine($" Price: {item.Price}");
                isOrderCompleted = true;
            }
            else
            {
                var cpu = cpus.FindBy(input[0]);
                if (cpu==null)
                {
                    sb.AppendLine(String.Format(ExceptionMessages.InvalidCpuPartNumber, input[0]));
                    return sb.ToString().TrimEnd();
                }

                var memory = memories.FindBy(input[1]);
                if (memory == null)
                {
                    sb.AppendLine(String.Format(ExceptionMessages.InvalidMemoryPartNumber, input[1]));
                    return sb.ToString().TrimEnd();
                }

                var motherboard = motherboards.FindBy(input[2]);
                if (motherboard == null)
                {
                    sb.AppendLine(String.Format(ExceptionMessages.InvalidMotherboardPartNumber, input[2]));
                    return sb.ToString().TrimEnd();
                }

                if (cpu.SupportedMemory!=memory.Type)
                {
                    sb.AppendLine(String.Format(ExceptionMessages.CpuDontSupportMemory, memory.Type));
                }
                if (cpu.Socket!=motherboard.Socket)
                {
                    sb.AppendLine(ExceptionMessages.NotSameSocket);
                }
            }
            return sb.ToString().TrimEnd();
        }

        public string ValidateListWithTwoImputs(string[] input)
        {
            var sb = new StringBuilder();

            var cpu = cpus.FindBy(input[0]);
            if (cpu == null)
            {
                sb.AppendLine(String.Format(ExceptionMessages.InvalidCpuPartNumber, input[0]));
                return sb.ToString().TrimEnd();
            }

            var memory = memories.FindBy(input[1]);
            if (memory == null)
            {
                sb.AppendLine(String.Format(ExceptionMessages.InvalidMemoryPartNumber, input[1]));
                return sb.ToString().TrimEnd();
            }

            if (cpu.SupportedMemory != memory.Type)
            {
                sb.AppendLine(String.Format(ExceptionMessages.CpuDontSupportMemory, memory.Type));
            }

            var combination = configurations.Models.Where(c => c.Cpu.PartNumber == input[0] && c.Memory.PartNumber == input[1]);
            var configurationNumber = 1;
            foreach (var item in combination)
            {
                sb.AppendLine($"Configuration {configurationNumber}");
                sb.AppendLine($"{item.Cpu.ComponentType} - {item.Cpu.Name} - {item.Cpu.Socket} - {item.Cpu.SupportedMemory} - {item.Cpu.PartNumber}");
                sb.AppendLine($"{item.Memory.ComponentType} - {item.Memory.Name} - {item.Memory.Type} - {item.Memory.PartNumber}");
                sb.AppendLine($"{item.Motherboard.ComponentType} - {item.Motherboard.Name} - {item.Motherboard.Socket} - {item.Motherboard.PartNumber}");
                sb.AppendLine($" Price: {item.Price}");
                configurationNumber++;
            }

            return sb.ToString().TrimEnd();
        }

        public string ValidateListWithOneImput(string[] input)
        {
            var sb = new StringBuilder();

            var cpu = cpus.FindBy(input[0]);
            if (cpu == null)
            {
                sb.AppendLine(String.Format(ExceptionMessages.InvalidCpuPartNumber, input[0]));
                return sb.ToString().TrimEnd();
            }

            var combination = configurations.Models.Where(c => c.Cpu.PartNumber == input[0]);
            var configurationNumber = 1;
            foreach (var item in combination)
            {
                sb.AppendLine($"Configuration {configurationNumber}");
                sb.AppendLine($"{item.Cpu.ComponentType} - {item.Cpu.Name} - {item.Cpu.Socket} - {item.Cpu.SupportedMemory} - {item.Cpu.PartNumber}");
                sb.AppendLine($"{item.Memory.ComponentType} - {item.Memory.Name} - {item.Memory.Type} - {item.Memory.PartNumber}");
                sb.AppendLine($"{item.Motherboard.ComponentType} - {item.Motherboard.Name} - {item.Motherboard.Socket} - {item.Motherboard.PartNumber}");
                sb.AppendLine($" Price: {item.Price}");
                configurationNumber++;
            }

            return sb.ToString().TrimEnd();

        }
    }
}
