using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Models.DTO;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Controllers
{
    [RoutePrefix("quanly")]
    public class ManagerController : Controller
    {
        IRegistrationCreativeExpRepository registrationCreativeExpRepository;
        IProgramRepository programRepository;

        public ManagerController(IRegistrationCreativeExpRepository registrationCreativeExpRepository, IProgramRepository programRepository)
        {
            this.registrationCreativeExpRepository = registrationCreativeExpRepository;
            this.programRepository = programRepository;
        }

        // GET: Manager 
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
        [Route("trainghiemsangtao")]
        [HttpGet]
        public ActionResult TraiNghiemSangTao()
        {
            List<RegistrationCreativeExp> registrationCreativeExps = registrationCreativeExpRepository.GetAllRegistrationCreativeExpByDateAndProgramId(DateTime.Now, 1);
            List<Program> programs = programRepository.GetPrograms();
            ListRegistrationCreativeExpOneViewModel listRegistrationCreativeExpOneViewModel = new ListRegistrationCreativeExpOneViewModel(registrationCreativeExps, programs);
            return View(listRegistrationCreativeExpOneViewModel);
        }
        [Route("dulieutrainghiemsangtao/{dateRegisted}/{programId}")]
        [HttpGet]
        public ActionResult DataRegistrationCreativeExp(DateTime dateRegisted, int programId)
        {
            List<RegistrationCreativeExp> registrationCreativeExps = registrationCreativeExpRepository.GetAllRegistrationCreativeExpByDateAndProgramId(dateRegisted, programId);
            if (registrationCreativeExps.Count < 1)
            {
                return Json("404", JsonRequestBehavior.AllowGet);
            }
            var jsonRegistrationCreativeExps = JsonConvert.SerializeObject(registrationCreativeExps,
            Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            return Json(jsonRegistrationCreativeExps, JsonRequestBehavior.AllowGet);
        }
    }
}