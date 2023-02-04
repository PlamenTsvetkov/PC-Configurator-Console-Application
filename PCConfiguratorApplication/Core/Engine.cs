namespace PCConfiguratorApplication.Core
{
    using System;

    using PCConfiguratorApplication.Core.Contracts;
    using PCConfiguratorApplication.IO.Contracts;
    using PCConfiguratorApplication.Utilities;
    using PCConfiguratorApplication.IO;

    public class Engine : IEngine
    {
        private const string Separator = ", ";
        private const string ExitString = "Exit";
        private const string YestString = "Yes";
        private const string ConfigurationstString = "Configurations";
        private const string IntroString = "Intro";
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

            GetIntro();

        }

        private void GetIntro()
        {
            writer.Write(controller.Intro());

            while (true)
            {
                string[] input = reader.ReadLine().Split(Separator, StringSplitOptions.RemoveEmptyEntries);

                IsExit(input[0].Trim());

                IsConfigurations(input[0].Trim());

                CheckPartNumber(input);
            }
        }

        private void CheckPartNumber(string[] input)
        {
            bool IsOrderCompleted = false;
            if (input.Length == 3)
            {
                writer.Write(controller.ValidateFullList(input, ref IsOrderCompleted));

            }
            else if (input.Length == 2)
            {
                writer.Write(controller.ValidateListWithTwoImputs(input));
            }
            else if (input.Length == 1)
            {
                writer.Write(controller.ValidateListWithOneImput(input));
            }
            else
            {
                writer.WriteLine(InvalidInput);
            }

            Console.ForegroundColor = ConsoleColor.Gray;

            if (IsOrderCompleted)
            {
                ProcessTheConfigurationPurchaseResponse();
            }

        }

        private void IsConfigurations(string input)
        {

            if (input.ToLower() == ConfigurationstString.ToLower())
            {
                writer.Write(controller.GetCofigurations());

                ProcessConfigurationResponse();
            }
        }

        private void ProcessConfigurationResponse()
        {

            string[] configurationsInput = reader.ReadLine().Split(Separator, StringSplitOptions.RemoveEmptyEntries);

            Console.ForegroundColor = ConsoleColor.Gray;

            bool isThereAConfigurationNumber = true;

            IsExit(configurationsInput[0].Trim());

            IsIntro(configurationsInput[0].Trim());

            var id = Int32.Parse(configurationsInput[0]);

            writer.Write(controller.BuyConfigurationById(id, ref isThereAConfigurationNumber));

            if (!isThereAConfigurationNumber)
            {
                ProcessConfigurationResponse();
            }

            ProcessTheConfigurationPurchaseResponse();
        }

        private void ProcessTheConfigurationPurchaseResponse()
        {
            string[] response = reader.ReadLine().Split(Separator, StringSplitOptions.RemoveEmptyEntries);

            IsExit(response[0].Trim());

            IsIntro(response[0].Trim());

            IsYes(response[0].Trim());
        }

        private void IsExit(string input)
        {
            if (input.ToLower() == ExitString.ToLower())
            {
                Environment.Exit(0);
            }
        }

        private void IsIntro(string input)
        {
            if (input.ToLower() == IntroString.ToLower())
            {
                GetIntro();
            }
        }

        private void IsYes(string input)
        {
            if (input.ToLower() == YestString.ToLower())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                writer.WriteLine($"{Environment.NewLine}{SuccessMessages.SuccesOrder}");
                Console.ForegroundColor = ConsoleColor.Gray;
                Environment.Exit(0);
            }
        }

    }
}
