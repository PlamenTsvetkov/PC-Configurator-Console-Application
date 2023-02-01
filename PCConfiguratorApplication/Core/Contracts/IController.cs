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

        void AddConfiguration(Cpu cpu, Memory memory, Motherboard motherboard);

        void LoadInventory();

        string Intro();

        //string BeginRace(string racerOneUsername, string racerTwoUsername);

        //string Report();
    }
}
