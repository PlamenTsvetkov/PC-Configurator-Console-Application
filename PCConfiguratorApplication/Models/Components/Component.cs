namespace PCConfiguratorApplication.Models.Component
{
    using PCConfiguratorApplication.Models.Component.Contracts;
    using PCConfiguratorApplication.Utilities;

    public abstract class Component : IComponent
    {
        private decimal price;

        protected Component(string componentType, string partNumber, string name, decimal price)
        {
            this.ComponentType = componentType;

            this.PartNumber = partNumber;

            this.Name = name;

            this.Price = price;
        }

        public string ComponentType { get; private set; }

        public string PartNumber { get; private set; }

        public string Name { get; private set; }

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
            return $"{this.ComponentType} --> Part Number: {this.PartNumber} || Name: {this.Name} || Price: {this.Price:f2} ";
        }
    }
}
