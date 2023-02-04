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
        private const string InputExitMessage = "\"Exit\" -> If you want to exit the program";
        private const string InputIntroMessage = "\"Intro\" -> If you want to intro text";
        private const string InputPartNumberMassage = "Please enter part number(s) in this format -> \"CPU part numer, DDR Memory part number, Motherboard part number\" :";
        private const string InputConfigurationsMassage = "\"Configurations\" -> If you want all possible configurations";
        private const string ConfigurationNumberMessage = "Thере are {0} possible configurations with the provided item";

        private CpuRepository cpus;
        private MemoryRepository memories;
        private MotherboardRepository motherboards;
        private ConfigurationRepository configurations;
        private bool isThereIncompatibility = false;

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

            sb.AppendLine();

            if (memories.Lenght() > 0)
            {
                foreach (var item in memories.Models)
                {
                    sb.AppendLine(item.ToString());
                }
            }

            sb.AppendLine();

            if (motherboards.Lenght() > 0)
            {
                foreach (var item in motherboards.Models)
                {
                    sb.AppendLine(item.ToString());
                }
            }

            sb.AppendLine();

            sb.AppendLine($"{InputExitMessage}{Environment.NewLine}{InputConfigurationsMassage}{Environment.NewLine}{InputPartNumberMassage}");
            return sb.ToString().TrimEnd();
        }

        public void GenerateCofigurations()
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

        public string GetCofigurations()
        {
            var sb = new StringBuilder();

            if (configurations.Lenght() > 0)
            {
                sb.AppendLine();
                sb.AppendLine("All possible configurations are as follows:");
                foreach (var configuration in configurations.Models)
                {
                    sb.AppendLine(configuration.ToString()); ;
                }
            }
            sb.AppendLine(InputExitMessage);
            sb.AppendLine(InputIntroMessage);
            sb.AppendLine("Please enter configuration number: ");

            return sb.ToString().TrimEnd();
        }

        public string BuyConfigurationById(int id, ref bool isThereAConfigurationNumber)
        {
            var configuration = configurations.FindBy(id);
            if (configuration == null)
            {
                isThereAConfigurationNumber = false;
                Console.ForegroundColor = ConsoleColor.Red;
                return String.Format(ExceptionMessages.InvalidConfigurationNumber);
                
            }
            return OrderConfirmation(configuration);
        }

        public string ValidateFullList(string[] input, ref bool isOrderCompleted)
        {
            var sb = new StringBuilder();

            if (configurations.Models.Any(c => c.Cpu.PartNumber == input[0] && c.Memory.PartNumber == input[1] && c.Motherboard.PartNumber == input[2]))
            {
                var configuration = configurations.Models.FirstOrDefault(c => c.Cpu.PartNumber == input[0] && c.Memory.PartNumber == input[1] && c.Motherboard.PartNumber == input[2]);
                isOrderCompleted = true;
                return OrderConfirmation(configuration);
            }
            else
            {
                var cpu = cpus.FindBy(input[0]);
                if (cpu == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    sb.AppendLine(String.Format(ExceptionMessages.InvalidCpuPartNumber, input[0]));
                    sb.AppendLine(InputPartNumberMassage);
                    return sb.ToString().TrimEnd();
                }

                var memory = memories.FindBy(input[1]);
                if (memory == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    sb.AppendLine(String.Format(ExceptionMessages.InvalidMemoryPartNumber, input[1]));
                    sb.AppendLine(InputPartNumberMassage);
                    return sb.ToString().TrimEnd();
                }

                var motherboard = motherboards.FindBy(input[2]);
                if (motherboard == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    sb.AppendLine(String.Format(ExceptionMessages.InvalidMotherboardPartNumber, input[2]));
                    sb.AppendLine(InputPartNumberMassage);
                    return sb.ToString().TrimEnd();
                }

                if (cpu.SupportedMemory != memory.Type)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    sb.AppendLine(String.Format(ExceptionMessages.CpuDontSupportMemory, memory.Type));
                }
                if (cpu.Socket != motherboard.Socket)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    sb.AppendLine(ExceptionMessages.NotSameSocket);
                }
                sb.AppendLine(InputPartNumberMassage);
                return sb.ToString().TrimEnd();
            }
            
        }

        public string ValidateListWithTwoImputs(string[] input)
        {
            var sb = new StringBuilder();

            var cpu = cpus.FindBy(input[0]);
            if (cpu == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                sb.AppendLine(String.Format(ExceptionMessages.InvalidCpuPartNumber, input[0]));
                sb.AppendLine(InputPartNumberMassage);
                return sb.ToString().TrimEnd();
            }

            var memory = memories.FindBy(input[1]);
            if (memory == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                sb.AppendLine(String.Format(ExceptionMessages.InvalidMemoryPartNumber, input[1]));
                sb.AppendLine(InputPartNumberMassage);
                return sb.ToString().TrimEnd();
            }

            if (cpu.SupportedMemory != memory.Type)
            {
                sb.AppendLine(String.Format(ExceptionMessages.CpuDontSupportMemory, memory.Type));
                isThereIncompatibility = true;
            }

            if (isThereIncompatibility)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                sb.AppendLine(InputPartNumberMassage);
                return sb.ToString().TrimEnd();
            }

            return MakePossibleConfiguration(input);
        }

        public string OrderConfirmation(IConfiguration configuration)
        {
            var sb = new StringBuilder();
            Console.ForegroundColor = ConsoleColor.Yellow;
            sb.AppendLine();
            sb.AppendLine($"Do you really want to order {configuration.ToString()} (\"Yes\" / \"Exit\"): ");
            return sb.ToString().TrimEnd();
        }

        public string ValidateListWithOneImput(string[] input)
        {
            var sb = new StringBuilder();

            var cpu = cpus.FindBy(input[0]);
            if (cpu == null)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                sb.AppendLine(String.Format(ExceptionMessages.InvalidCpuPartNumber, input[0]));
                sb.AppendLine(InputPartNumberMassage);
                return sb.ToString().TrimEnd();
            }

            return MakePossibleConfiguration(input);
        }

        private string MakePossibleConfiguration(string[] input)
        {
            var sb = new StringBuilder();

            IEnumerable<IConfiguration> possibleConfigurations;

            if (input.Length==1)
            {
                 possibleConfigurations = configurations.Models.Where(c => c.Cpu.PartNumber == input[0]);
            }
            else
            {
                 possibleConfigurations = configurations.Models.Where(c => c.Cpu.PartNumber == input[0] && c.Memory.PartNumber == input[1]);
            }

            sb.AppendLine(String.Format(ConfigurationNumberMessage,possibleConfigurations.Count().ToString()));
            var configurationNumber = 1;
            foreach (var item in possibleConfigurations)
            {
                sb.AppendLine($"N: {configurationNumber}");
                sb.AppendLine(item.ToString());
               
                configurationNumber++;
            }

            sb.AppendLine($"{InputExitMessage}{Environment.NewLine}{InputConfigurationsMassage}{Environment.NewLine}{InputPartNumberMassage}");
            return sb.ToString().TrimEnd();
        }

    }
}
