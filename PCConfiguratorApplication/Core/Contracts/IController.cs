namespace PCConfiguratorApplication.Core.Contracts
{
    using PCConfiguratorApplication.Models.Cpus;
    using PCConfiguratorApplication.Models.Memories;
    using PCConfiguratorApplication.Models.Motherboards;

    public interface IController
    {
        void AddCpu(string componentType, string partNumber, string name, string supportMemory, string socket , decimal price);

        void AddMemory(string componentType, string partNumber, string name, string type, decimal price);

        void AddMotherboard(string componentType, string partNumber, string name,  string socket, decimal price);

        void AddConfiguration(int id, Cpu cpu, Memory memory, Motherboard motherboard);

        string ValidateFullList(string[] input, bool isOrderCompleted);

        string ValidateListWithTwoImputs(string[] input);

        string ValidateListWithOneImput(string[] input);

        void LoadInventory();

        string Intro();

        void GenerateConigurations();

        string GetConigurations();

        string BuyConfiguration(int id);
    }
}
