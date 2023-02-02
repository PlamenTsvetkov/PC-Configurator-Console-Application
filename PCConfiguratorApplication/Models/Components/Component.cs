namespace PCConfiguratorApplication.Models.Component
{
    using PCConfiguratorApplication.Models.Component.Contracts;
    using PCConfiguratorApplication.Utilities;

    public abstract class Component : IComponent
    {

        private string componentType;

        private string partNumber;

        private string name;

        private decimal price;

        protected Component(string componentType, string partNumber, string name, decimal price)
        {
            this.ComponentType = componentType;

            this.PartNumber = partNumber;

            this.Name = name;

            this.Price = price;
        }

        public string ComponentType
        {
            get => this.componentType;

            private set => this.componentType = value;
        }

        public string PartNumber
        {
            get => this.partNumber;

            private set => this.partNumber = value;
        }

        public string Name
        {
            get => this.name;

            private set => this.name = value;
        }

        public decimal Price
        {
            get => this.price;

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPrice);
                }
                this.price = value;
            }
        }

        public override string ToString()
        {
            return $"{this.componentType} Part Number: {this.PartNumber}, Name: {this.Name}. Price: {this.Price:f2}";
        }
    }
}
