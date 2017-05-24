using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Text;
using NumberGenerators.Models;

namespace NumberGenerators.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Http.HttpPost]
        public ActionResult Register([FromBody]UserPass s)
        {

            using (DataBase d = new DataBase())
            {
                Generator generator = d.Generators.FirstOrDefault(g => g.UserName == s.User && g.Password == s.Pass);

                if (generator == null)
                {
                    generator = new Generator();
                    generator.ID = Guid.NewGuid();
                    generator.UserName = s.User;
                    generator.Password = s.Pass;

                    d.Generators.Add(generator);
                    d.SaveChanges();
                }
                return RedirectToAction("Index", "GeneratePage", new { id = generator.ID });
            }
        }
    }
}