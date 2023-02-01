namespace PCConfiguratorApplication.IO
{
    using PCConfiguratorApplication.IO.Contracts;

    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
