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
        [Route("tailieu")]
        [HttpGet]
        public ActionResult TaiLieu()
        {
            return View();
        }
        [Route("taitailieu/{tenfile}")]
        [HttpGet]
        public ActionResult TaiTaiLieu(string tenfile)
        {
            return File(System.Web.HttpContext.Current.Server.MapPath("~/Utils/Files/" + tenfile), "application/pdf", tenfile);
        }

    }
}