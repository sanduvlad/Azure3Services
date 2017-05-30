using Microsoft.WindowsAzure.Storage.Table;
using NumberGenerators.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace NumberGenerators.Controllers
{
    
    public class GeneratePageController : Controller
    {
        // GET: GeneratePage
        public ActionResult Index(Guid id)
        {
            
            using (var data = new DataBase())
            {
                Generator generator = data.Generators.First(g => g.ID == id);
                GeneratorWithDetails generatorWithDetails = new GeneratorWithDetails();
                generatorWithDetails._Generator = generator;
                generatorWithDetails.Numbers = new List<int>();

                List<IntNumber> list =  AzureConnectionSingleton.GetInstance().GetNumbers(id);

                Model m = new Model();
                m.generator = generator;
                m.numberList = list;

                return View(m);
            }

                return View();
        }

        [System.Web.Http.HttpPost]
        public ActionResult GenerateNumbers([FromBody] Guid guid)
        {

            string html = string.Empty;
            string url = @"https://primenumbergenerator.azurewebsites.net/api/HttpTriggerCSharp1?code=jYCUrJvUaMoIaCDfKEZguaYjtDEtt7Kkt2Zc54n/9VkY1mtmabwHgA==";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            List<IntNumber> numbers = new List<IntNumber>();
            List<string> charNumbers = new List<string>();
            charNumbers = html.Split(' ').ToList();
            for (int i = 1; i < charNumbers.Count-1; i++)
            {
                if (false == String.IsNullOrEmpty(charNumbers[i]))
                {
                    numbers.Add(new IntNumber() { Number = int.Parse(charNumbers[i]), PartitionKey = guid.ToString(), RowKey = guid.ToString() + Guid.NewGuid().ToString() });
                }
            }
            AzureConnectionSingleton.GetInstance().StoreNumbers(numbers);
            return RedirectToAction("Index", "GeneratePage", new { id = guid });
        }
    }
}