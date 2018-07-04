using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Controllers
{
    public class NoidungkhacController : Controller
    {
        ISchoolRepository schoolRepository;

        public NoidungkhacController(ISchoolRepository schoolRepository)
        {
            this.schoolRepository = schoolRepository;
        }


        // GET: Noidungkhac
        [Route("noidungkhac")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}