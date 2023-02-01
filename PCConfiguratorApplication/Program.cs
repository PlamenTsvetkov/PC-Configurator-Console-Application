
using PCConfiguratorApplication.Core.Contracts;
using PCConfiguratorApplication.Core;

IEngine engine = new Engine();
engine.Run();
//var data = new Items();
//var projectDir = GetProjectDirectory();
//var baseDir = projectDir + @"Datasets/pc-store-inventory.json";

//using (StreamReader r = new StreamReader(baseDir))
//{
//    string json = r.ReadToEnd();
//    data= JsonConvert.DeserializeObject<Items>(json);
//}

//foreach (var item in data.CPUs)
//{
//    var sb = new StringBuilder();
//    sb.AppendLine(item.ComponentType);
//    sb.AppendLine(item.PartNumber);
//    sb.AppendLine(item.Name);
//    sb.AppendLine(item.SupportedMemory);
//    sb.AppendLine(item.Socket);
//    sb.AppendLine(item.Price.ToString());
//    Console.WriteLine(sb.ToString());
//}
//Console.WriteLine("------------------");
//foreach (var item in data.Memory)
//{
//    var sb = new StringBuilder();
//    sb.AppendLine(item.ComponentType);
//    sb.AppendLine(item.PartNumber);
//    sb.AppendLine(item.Name);
//    sb.AppendLine(item.Type);
//    sb.AppendLine(item.Price.ToString());
//    Console.WriteLine(sb.ToString());
//}
//Console.WriteLine("------------------");
//foreach (var item in data.Motherboards)
//{
//    var sb = new StringBuilder();
//    sb.AppendLine(item.ComponentType);
//    sb.AppendLine(item.PartNumber);
//    sb.AppendLine(item.Name);
//    sb.AppendLine(item.Socket);
//    sb.AppendLine(item.Price.ToString());
//    Console.WriteLine(sb.ToString());
//}
//Console.WriteLine("------------------");
//Console.WriteLine(data.CPUs.Length);
//Console.WriteLine(data.Memory.Length);
//Console.WriteLine(data.Motherboards.Length);



