using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TraiNghiemSangTao.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        [Route("lienhe")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}