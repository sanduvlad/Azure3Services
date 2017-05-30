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
        private Guid id;
        // GET: GeneratePage
        public ActionResult Index(Guid id)
        {
            this.id = id;
            using (var data = new DataBase())
            {
                Generator generator = data.Generators.First(g => g.ID == id);
                GeneratorWithDetails generatorWithDetails = new GeneratorWithDetails();
                generatorWithDetails._Generator = generator;
                generatorWithDetails.Numbers = new List<int>();

                AzureConnectionSingleton.GetInstance();

                return View(generator);
            }

                return View();
        }

        [System.Web.Http.HttpPost]
        public ActionResult GenerateNumbers()
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
            for (int i = 0; i < charNumbers.Count; i++)
            {
                if (false == String.IsNullOrEmpty(charNumbers[i]))
                {
                    numbers.Add(new IntNumber() { Number = int.Parse(charNumbers[i]), PartitionKey = id.ToString(), RowKey = id.ToString() });
                }
            }
            AzureConnectionSingleton.GetInstance().StoreNumbers(numbers);
            return RedirectToAction("Index", "GeneratePage", new { id = this.id });
        }
    }
}