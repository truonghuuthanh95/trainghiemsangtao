using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TraiNghiemSangTao.Controllers
{
    public class UpdateSuccessfulController : Controller
    {
        // GET: UpdateSuccessful
        [Route("capnhatthanhcong", Name ="capnhatthanhcong")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}