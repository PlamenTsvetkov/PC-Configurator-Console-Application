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
        private const string CongratulationsString = "Congratulations on your order!";
        private const string InvalidInput = "Invalid input!";
    

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

            controller.GenerateCofigurations();


            writer.Write(controller.Intro());

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
            bool IsOrderCompleted = false;
            if (input.Length==3)
            {
                writer.Write(controller.ValidateFullList(input,ref IsOrderCompleted));
            }
            else if (input.Length == 2)
            {
                writer.Write(controller.ValidateListWithTwoImputs(input));
            }
            else if(input.Length == 1)
            {
                writer.Write(controller.ValidateListWithOneImput(input));
            }
            else
            {
                writer.WriteLine(InvalidInput);
            }
            if (IsOrderCompleted)
            {
                ProcessTheConfigurationPurchaseResponse();
            }

        }

        private void IsConfigurations(string input)
        {
           
            if (input == ConfigurationstString)
            {
                writer.Write(controller.GetCofigurations());

                ProcessConfigurationResponse();
            }
        }

        private void ProcessConfigurationResponse()
        {

            string[] configurationsInput = reader.ReadLine().Split(Separator);

            bool isThereAConfigurationNumber = true;

            IsExit(configurationsInput[0]);

            var id = Int32.Parse(configurationsInput[0]);

            writer.Write(controller.BuyConfigurationById(id, ref isThereAConfigurationNumber));

            if (!isThereAConfigurationNumber)
            {
                ProcessConfigurationResponse();
            }

            ProcessTheConfigurationPurchaseResponse();
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

        private void ProcessTheConfigurationPurchaseResponse()
        {
            string[] response = reader.ReadLine().Split(Separator);

            IsExit(response[0]);

            IsYes(response[0]);
        }
    }
}
