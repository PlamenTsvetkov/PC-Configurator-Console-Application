namespace PCConfiguratorApplication.Core
{
    using PCConfiguratorApplication.Core.Contracts;
    using PCConfiguratorApplication.IO;
    using PCConfiguratorApplication.IO.Contracts;
    using System;

    public class Engine : IEngine
    {
        private const string Separator = ", ";
        private const string ExitString = "Exit";
        private const string YestString = "Yes";
        private const string ConfigurationstString = "Configurations";
        private const string CongratulationsString = "Congratulations on your purchase!";
        private const bool IsOrderCompleted = false;

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

            controller.GenerateConigurations();


            writer.WriteLine(controller.Intro());

            while (true)
            {
                string[] input = reader.ReadLine().Split(Separator);

                IsExit(input[0]);

                IsConfigurations(input[0]);

                CheckPartNumber(input);
                
            }
        }
        private void CheckPartNumber(string[] input)
        {
            if (input.Length==3)
            {
                writer.WriteLine(controller.ValidateFullList(input, IsOrderCompleted));
            }
            else if (input.Length == 2)
            {
                writer.WriteLine(controller.ValidateListWithTwoImputs(input));
            }
            else if(input.Length == 1)
            {
                writer.WriteLine(controller.ValidateListWithOneImput(input));
            }
            else
            {
                writer.WriteLine("Wrong input");
            }
            if (IsOrderCompleted)
            {
                Environment.Exit(0);
            }

        }
        private void IsConfigurations(string input)
        {
            if (input == ConfigurationstString)
            {
                string configurationsInfo;
                configurationsInfo = controller.GetConigurations();

                writer.WriteLine(configurationsInfo);

                string[] configurationsInput = reader.ReadLine().Split(Separator);

                IsExit(configurationsInput[0]);

                var id = Int32.Parse(configurationsInput[0]);

                writer.Write(controller.BuyConfiguration(id));

                string[] response = reader.ReadLine().Split(Separator);

                IsExit(response[0]);

                IsYes(response[0]);
            }
        }

        private void  IsExit(string input)
        {
            if (input == ExitString)
            {
                Environment.Exit(0);
            }
        }

        private void IsYes(string input)
        {
            if (input == YestString)
            {
                writer.WriteLine(CongratulationsString);
                Environment.Exit(0);
            }
        }
    }
}
