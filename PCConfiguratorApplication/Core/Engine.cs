namespace PCConfiguratorApplication.Core
{
    using PCConfiguratorApplication.Core.Contracts;
    using PCConfiguratorApplication.IO;
    using PCConfiguratorApplication.IO.Contracts;
    using System;

    public class Engine : IEngine
    {
        private const string Separator = ", ";

        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IController controller;

        public Engine()
        {
            this.writer = new ConsoleWriter();
            this.reader = new ConsoleReader();
            this.controller = new Controller();
        }

        public void Run()
        {
            controller.LoadInventory();

            string loadInfo;
            loadInfo =  controller.Intro();

            writer.WriteLine(loadInfo);
            string[] input = reader.ReadLine().Split(Separator);

            try
            {
              
            }
            catch (Exception ex)
            {
                writer.WriteLine(ex.Message);
            }

        }
    }
}
