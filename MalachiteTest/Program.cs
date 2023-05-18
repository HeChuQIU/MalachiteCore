// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

ItemTest it = new ItemTest("asd");
List<string> list = new List<string>();
for (int k = 0; k < 1000000; k++)
{
    string key = k + "";
    list.Add(key);
}

long currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
int i = 0;
foreach (string key in list)
{
    it.Properties.Add(key, i);
    i++;
}
long end = DateTimeOffset.UtcNow.ToUnixTimeSeconds();



// foreach (KeyValuePair<string, object> kv in it.Properties)
// {
//     Console.WriteLine(kv.Key + " " + kv.Value);
// }

Console.WriteLine("Time: " + (end - currentTimestamp) + " ms");
Random r = new();
string index = r.Next(100000).ToString();
Console.WriteLine($"Key: {index} Value: {it.Properties[index]}");
Console.WriteLine(it.Properties.Size());
Console.ReadLine();