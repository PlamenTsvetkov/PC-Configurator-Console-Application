namespace PCConfiguratorApplication.Models.Component
{
    using PCConfiguratorApplication.Models.Component.Contracts;

    public abstract class ComponentWithSocket : Component, IComponentWithSocket
    {

        protected ComponentWithSocket(
            string componentType,
            string partNumber,
            string name,
            decimal price,
            string socket)
            : base(componentType, partNumber, name, price)
        {
            this.Socket = socket;
        }

        public string Socket { get; private set; }

        public override string ToString()
        {
            return base.ToString()+ $"|| Socket: {this.Socket} ";
        }
    }
}
