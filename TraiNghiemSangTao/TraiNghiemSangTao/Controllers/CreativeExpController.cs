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
    public class CreativeExpController : Controller
    {
        IProgramRepository programRepository;
        ISchoolRepository schoolRepository;
        IClassesRepository classesRepository;
        IDistrictRepository districtRepository;
        ISchoolDegreeRepository schoolDegreeRepository;
        IJobTitleRepository jobTitleRepository;
        ISessionADayRepository sessionADayRepository;
        IRegistrationCreativeExpRepository registrationCreativeExpRepository;

        public CreativeExpController(IProgramRepository programRepository, ISchoolRepository schoolRepository, IClassesRepository classesRepository, IDistrictRepository districtRepository, ISchoolDegreeRepository schoolDegreeRepository, IJobTitleRepository jobTitleRepository, ISessionADayRepository sessionADayRepository, IRegistrationCreativeExpRepository registrationCreativeExpRepository)
        {
            this.programRepository = programRepository;
            this.schoolRepository = schoolRepository;
            this.classesRepository = classesRepository;
            this.districtRepository = districtRepository;
            this.schoolDegreeRepository = schoolDegreeRepository;
            this.jobTitleRepository = jobTitleRepository;
            this.sessionADayRepository = sessionADayRepository;
            this.registrationCreativeExpRepository = registrationCreativeExpRepository;
        }



        // GET: CreativeExp
        [Route("trainghiemsangtao")]
        [HttpGet]
        public ActionResult Index()
        {
            List<Program> programs = programRepository.GetPrograms();

            List<School> schools = schoolRepository.GetSchoolByDistrictAndSchoolDegree(760, 3);
            List<Class> classes = classesRepository.GetClassBySchoolDegree(3);
            List<District> districts = districtRepository.GetDistricts();
            List<SchoolDegree> schoolDegrees = schoolDegreeRepository.GetSchoolDegrees();
            List<Jobtitle> jobtitles = jobTitleRepository.GetJobtitles();
            List<SessionADay> sessionADays = sessionADayRepository.GetSessionADays();
            CreativeExpOneViewModel creativeExpOneViewModel = new CreativeExpOneViewModel(null,programs, schoolDegrees, districts, schools, classes, jobtitles, sessionADays);         
            return View(creativeExpOneViewModel);
        }

        [Route("dangkitrainghiemsangtao")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCreativeExp(CreativeExpDTO creativeExpDTO)
        {
            
            RegistrationCreativeExp registrationCreativeExp =  registrationCreativeExpRepository.SaveRegistrationCreativeExp(creativeExpDTO);
            //Utils.SendMailService.SendMailToTeacher(registrationCreativeExp);
            return Json(registrationCreativeExp);
        }
        [Route("getSchoolBySchoolDegreeAndDistrict/{schoolDegreeId}/{districtId}")]
        [HttpGet]
        public ActionResult GetSchoolBySchoolDegreeAndDistrict(int schoolDegreeId, int districtId)
        {
            List<School> schools = schoolRepository.GetSchoolByDistrictAndSchoolDegree(districtId, schoolDegreeId);
            var schoolsJson = JsonConvert.SerializeObject(schools,
           Formatting.None,
           new JsonSerializerSettings()
           {
               ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
           });
            return Json(schoolsJson, JsonRequestBehavior.AllowGet);
        }
        
        [Route("getClassBySchoolDegree/{id}")]
        [HttpGet]
        public ActionResult GetClassBySchoolDegree(int id)
        {
            List<Class> classes = classesRepository.GetClassBySchoolDegree(id);
            var classesJson = JsonConvert.SerializeObject(classes,
            Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            return Json(classesJson, JsonRequestBehavior.AllowGet);
        }

        [Route("getValidStudentQuantityToRegist/{programId}/{sesstionADay}/{date}")]
        [HttpGet]
        public int GetValidStudentQuantity(int programId,int sesstionADay, DateTime date)
        {
            int validQuantity = registrationCreativeExpRepository.CheckValidQuantityStudent(programId, sesstionADay, date);
            return validQuantity;
        }
        [Route("dangkitrainghiemthanhcong/{registedCode}")]
        [HttpGet]
        public ActionResult RegistrationCreativeSuccess(string registedCode)
        {
            ViewBag.RegistedCode = registedCode;
            return View();
        }
        [Route("kiemtramatrainghiemsangtao/{registedCode}")]
        [HttpGet]
        public Boolean GetValidRegistedCode(string registedCode)
        {
           return registrationCreativeExpRepository.GetValidRegistedCode(registedCode);
           
        }
        [Route("capnhattrainghiemsangtao", Name = "capnhattrainghiemsangtao")]
        [HttpGet]
        public ActionResult UpdateCreativeExp(CreativeExpDTO creativeExpDTO)
        {
            return View();
        }
        [Route("capnhattrainghiemsangtao/{registedCode}")]
        [HttpGet]
        public ActionResult UpdateCreativeExpScreen(string registedCode)
        {
            RegistrationCreativeExp registrationCreativeExp = registrationCreativeExpRepository.GetRegistrationCreativeExpByRegistedCode(registedCode);
            if (registrationCreativeExp == null)
            {
                return RedirectToRoute("capnhattrainghiemsangtao");
            }
            List<Program> programs = programRepository.GetPrograms();

            List<School> schools = schoolRepository.GetSchoolByDistrictAndSchoolDegree(registrationCreativeExp.School.DistrictId, registrationCreativeExp.SchoolDegreeId);
            List<Class> classes = classesRepository.GetClassBySchoolDegree(registrationCreativeExp.SchoolDegreeId);
            List<District> districts = districtRepository.GetDistricts();
            List<SchoolDegree> schoolDegrees = schoolDegreeRepository.GetSchoolDegrees();
            List<Jobtitle> jobtitles = jobTitleRepository.GetJobtitles();
            List<SessionADay> sessionADays = sessionADayRepository.GetSessionADays();
            CreativeExpOneViewModel creativeExpOneViewModel = new CreativeExpOneViewModel(registrationCreativeExp,programs, schoolDegrees, districts, schools, classes, jobtitles, sessionADays);
            return View(creativeExpOneViewModel);
            
        }
        [Route("postCaphattrainghiemsangtao")]
        [HttpPost]
        public ActionResult PostUpdateCreativeExp(CreativeExpDTO creativeExpDTO, int id)
        {
            registrationCreativeExpRepository.ResetStudentQuantity(id);
            int validStudent = registrationCreativeExpRepository.CheckValidQuantityStudent(creativeExpDTO.ProgramId, creativeExpDTO.DaySessionId, creativeExpDTO.DateRegisted);
            if (creativeExpDTO.StudentQuantity > validStudent)
            {
                return Json("Số lượng học sinh đang kí tối đa là: " + validStudent);
            }
            RegistrationCreativeExp registrationCreativeExp = registrationCreativeExpRepository.UpdateRegistrationCreativeExp(creativeExpDTO, id);
            return Json("200");
        }
        [Route("capnhattrainghiemsangtaothanhcong")]
        [HttpGet]
        public ActionResult UpdateCreativeExpSuccessfull(CreativeExpDTO creativeExpDTO)
        {

            return View();
        }
    }
}