namespace PCConfiguratorApplication.Utilities
{
    public static class ExceptionMessages
    {
        public const string InvalidCpu = "CPU cannot be null or empty.";

        public const string InvalidCpuPartNumber = "CPU with part number {0} does not exist.";

        public const string InvalidMemory = "Memory cannot be null or empty.";

        public const string InvalidMemoryPartNumber = "DDR Memory with part number {0} does not exist.";

        public const string InvalidMotherboards = "Motherboard cannot be null or empty.";

        public const string InvalidMotherboardPartNumber = "Motherboard with part number {0} does not exist.";

        public const string InvalidPrice = "The price cannot be less than or equal to zero.";

        public const string InvalidConfigurationNumber = "Configuration with that number does not exits. \"Exit\" -> If you want to exit the program or enter new configuration number id:";

        public const string NotSameSocket = "Motherboard and CPU are not with the same socket.";

        public const string CpuDontSupportMemory = "Memory of type {0} is not compatible with the CPU";
    }
}

