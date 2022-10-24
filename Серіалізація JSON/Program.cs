using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Серіалізація_JSON
{
    internal class PublishingHouse
    {
        public int Id { get; }
        public string Name { get; }
        public string Adress { get; }

        public PublishingHouse(int Id, string Name, string Adress)
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new ArgumentException($"'{nameof(Name)}' cannot be null or empty.", nameof(Name));
            }

            if (string.IsNullOrEmpty(Adress))
            {
                throw new ArgumentException($"'{nameof(Adress)}' cannot be null or empty.", nameof(Adress));
            }

            this.Id = Id;
            this.Name = Name;
            this.Adress = Adress;
        }

        public override string? ToString()
        {
            return "Id:" + Id + " Name:" + Name + " Adress:" + Adress;
        }
    }

    internal class Book
    {
        [JsonIgnore]
        public int PublishingHouseId { get; }

        [JsonPropertyName("Title")]
        public string Name { get; }

        public PublishingHouse PublishingHouse { get; }

        public Book(string name, PublishingHouse publishingHouse)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }
            PublishingHouse = publishingHouse ?? throw new ArgumentNullException(nameof(publishingHouse));
            Name = name;
            PublishingHouseId = publishingHouse.Id;
        }

        public override string? ToString()
        {
            return "{\nPublishingHouseId:" + PublishingHouseId + "\nTitle:" + Name + "\nPublishingHouse:" + PublishingHouse.ToString() + "\n}";
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var deserializedPath = "C:\\Users\\mykhailolapshyn\\Desktop\\JSON Серіалізація\\Серіалізація JSON\\Серіалізація JSON\\bin\\Debug\\net6.0\\bookFileSerialization.json";
                var deserialized = File.ReadAllText(deserializedPath);
                var option = new JsonSerializerOptions()
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                    WriteIndented = true,
                };
                List<Book> books = JsonSerializer.Deserialize<List<Book>>(deserialized, option) ?? throw new NullReferenceException("file is empty");
                foreach (var book in books)
                {
                    Console.WriteLine(book);
                }
                var serializedPath = "Test.json";
                var serialized = JsonSerializer.Serialize(books, option);
                File.WriteAllText(serializedPath, serialized);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }





}

