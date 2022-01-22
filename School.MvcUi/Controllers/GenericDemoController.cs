using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School.MvcUi.Controllers
{
    using Models;

    public class GenericDemoController : Controller
    {
        public ActionResult Index()
        {
            var schowek = new OneThingCase<int>();

            var listaPojemnikow = new List<OneThingCase<string>>();

            IEnumerable<OneThingCase<string>> listaPojemnikowDoOdczytu = listaPojemnikow;

            listaPojemnikow.Add(new OneThingCase<string>());

            listaPojemnikowDoOdczytu.First();


            schowek.Put(1);

            return View(schowek);
        }
    }
}