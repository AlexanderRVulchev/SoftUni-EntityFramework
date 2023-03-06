using ProductShop.Data;
using ProductShop.DTOs.Import;
using ProductShop.Models;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            var context = new ProductShopContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
                        
            string usersXmlFilePath = @"../../../Datasets/users.xml";
            string usersXmlString = File.ReadAllText(usersXmlFilePath);
            ImportUsers(context, usersXmlString);

            string productsXmlFilePath = @"../../../Datasets/products.xml";
            string productsXmlString = File.ReadAllText(productsXmlFilePath);
            string output = ImportProducts(context, productsXmlString);

            Console.WriteLine(output);
        }

        // This is a generic XML deserializer
        private static T Deserialize<T>(string inputXml, string rootName)
        {
            XmlRootAttribute root = new XmlRootAttribute(rootName);
            XmlSerializer serializer = new XmlSerializer(typeof(T), root);

            using StringReader reader = new StringReader(inputXml);

            T dtos = (T)serializer.Deserialize(reader);
            return dtos;
        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var userDtos = Deserialize<ImportUserDto[]>(inputXml, "Users");

            User[] users = userDtos
                .Select(s => new User()
                {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Age = s.Age
                })
                .ToArray();

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count()}";
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var productDtos = Deserialize<ImportProductDto[]>(inputXml, "Products");

            Product[] products = productDtos
                .Select(p => new Product()
                {
                    Name = p.Name,
                    Price = p.Price,
                    BuyerId = p.BuyerId == 0 ? null : p.BuyerId,
                    SellerId = p.SellerId
                })
                .ToArray();

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count()}";
        }
    }
}