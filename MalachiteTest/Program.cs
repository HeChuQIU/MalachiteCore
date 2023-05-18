// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

ItemTest it = new ItemTest("asdfasdfasdfasdfasdf");
it.Properties.Put("🗡", "1234");
it.Properties.Put("魔法", "123");
it.Properties.Put("套装", "null");

Console.WriteLine(it.Properties.Get<string>("套装"));
it.Properties.Remove("套装");
Console.WriteLine(it.Properties.ContainsKey("套装"));
Console.ReadLine();