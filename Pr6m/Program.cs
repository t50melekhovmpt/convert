using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pr6m
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Class1> cats = new List<Class1>();
            Console.WriteLine("Введите путь до файла, который вы хотите открыть");
            Console.WriteLine("-------------------------------------------------");
            string home = Console.ReadLine();
            string feed = Path.GetExtension(home);
            switch (feed)
            {
                case ".txt":
                    using (StreamReader boss = new StreamReader(home))
                    {
                        while (!boss.EndOfStream)
                        {
                            string name = boss.ReadLine();
                            Console.WriteLine(name);
                            int age = int.Parse(boss.ReadLine());
                            Console.WriteLine(age);
                            cats.Add(new Class1(name, age));
                        }
                    }
                    break;
                case ".json":
                    string json = File.ReadAllText(home);
                    cats = JsonConvert.DeserializeObject<List<Class1>>(json);
                    break;
                case ".xml":
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Class1>));
                    using (FileStream input = new FileStream(home, FileMode.Open))
                    {
                        Classes = (List<Class1>)serializer.Deserialize(input);
                    }
                    break;
            }
            Console.WriteLine("Нажмите F1 для сохранения, Escape для выхода");
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.F1)
                {
                    Console.WriteLine("Введите путь к файлу и формат в котором хотите сохранить(txt,json,xml)");
                    Console.WriteLine("--------------------------------------------------------------------");
                    string home1 = Console.ReadLine();
                    string feed1 = Path.GetExtension(home1);
                    switch (feed1)
                    {
                        case ".txt":
                            using (StreamWriter writer = new StreamWriter(home1))
                            {
                                foreach (var cat in cats)
                                {
                                    writer.WriteLine(cat.name);
                                    writer.WriteLine(cat.age);
                                }
                            }
                            Console.WriteLine("Сохранено TXT");
                            return;
                        case ".json":
                            string result = JsonConvert.SerializeObject(cats);
                            File.WriteAllText(home1, result);
                            Console.WriteLine("Сохранено JSON");
                            return;
                        case ".xml":
                            XmlSerializer serializer = new XmlSerializer(typeof(List<Class1>));
                            using (FileStream output = new FileStream(home1, FileMode.Create))
                            {
                                serializer.Serialize(output, cats);
                            }
                            Console.WriteLine("Сохранено XML");
                            return;
                    }
                }
            } while (key != ConsoleKey.Escape);
        }
    }
}