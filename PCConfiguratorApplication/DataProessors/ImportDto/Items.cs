namespace PCConfiguratorApplication.DataProessors.ImportDto
{
    using PCConfiguratorApplication.Models.Cpus;
    using PCConfiguratorApplication.Models.Memories;
    using PCConfiguratorApplication.Models.Motherboards;

    public class Items
    {
        public Cpu[] CPUs { get; set; }

        public Memory[] Memory { get; set; }

        public Motherboard[] Motherboards { get; set; }
    }
}
