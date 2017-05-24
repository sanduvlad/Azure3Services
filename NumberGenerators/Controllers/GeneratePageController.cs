using NumberGenerators.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                return View(generator);
            }

                return View();
        }
    }
}