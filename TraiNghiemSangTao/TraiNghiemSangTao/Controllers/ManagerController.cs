using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Models.DTO;
using TraiNghiemSangTao.Models.DTO.KHKT;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Controllers
{
    [RoutePrefix("quanly")]
    public class ManagerController : Controller
    {
        IRegistrationCreativeExpRepository registrationCreativeExpRepository;
        IProgramRepository programRepository;
        IRegistrationRepository registrationRepository;
        ISubjectRegistedRepository subjectRegistedRepository;
        ISocialLifeSkillRepository socialLifeSkillRepository;
        IKHKTKhoaHocKiThuatRepository kHKTKhoaHocKiThuatRepository;

        public ManagerController(IRegistrationCreativeExpRepository registrationCreativeExpRepository, IProgramRepository programRepository, IRegistrationRepository registrationRepository, ISubjectRegistedRepository subjectRegistedRepository, ISocialLifeSkillRepository socialLifeSkillRepository, IKHKTKhoaHocKiThuatRepository kHKTKhoaHocKiThuatRepository)
        {
            this.registrationCreativeExpRepository = registrationCreativeExpRepository;
            this.programRepository = programRepository;
            this.registrationRepository = registrationRepository;
            this.subjectRegistedRepository = subjectRegistedRepository;
            this.socialLifeSkillRepository = socialLifeSkillRepository;
            this.kHKTKhoaHocKiThuatRepository = kHKTKhoaHocKiThuatRepository;
        }


        // GET: Manager 
        [Route("")]
        public ActionResult Index()
        {
            Account account = (Account)Session[Utils.CommonConstant.USER_SESSION];
            if (account == null)
            {
                return RedirectToRoute("login");
            }
            return View();
        }
        [Route("trainghiemsangtao/{dateFrom}/{dateTo}/{programId}")]
        [HttpGet]
        public ActionResult TraiNghiemSangTao(DateTime dateFrom, DateTime dateTo, int programId)
        {
            Account account = (Account)Session[Utils.CommonConstant.USER_SESSION];
            if (account == null || account.RoleId != 2 && account.RoleId != 4 && account.RoleId != 5 && account.RoleId != 6 && account.RoleId != 7)
            {
                return RedirectToRoute("login");
            }
            //Program registrationCreativeExp = registrationCreativeExpRepository.GetRegistrationFirstIndex(); 
            List<RegistrationCreativeExp> registrationCreativeExps = registrationCreativeExpRepository.GetAllRegistrationCreativeExpByDateAndProgramId(dateFrom, dateTo, programId);
            List<Program> programs = programRepository.GetPrograms();
            ListRegistrationCreativeExpOneViewModel listRegistrationCreativeExpOneViewModel = new ListRegistrationCreativeExpOneViewModel(registrationCreativeExps, programs, dateFrom, dateTo, programId);
            return View(listRegistrationCreativeExpOneViewModel);
        }

        [Route("noidungkhac/{dateFrom}/{dateTo}")]
        [HttpGet]
        public ActionResult NoiDungKhac(DateTime dateFrom, DateTime dateTo)
        {
            Account account = (Account)Session[Utils.CommonConstant.USER_SESSION];
            if (account == null || account.RoleId != 2 && account.RoleId != 3)
            {
                return RedirectToRoute("login");
            }
            List<Registration> registrations = registrationRepository.GetRegistrationsByDate(dateFrom, dateTo);
            ManagerNoiDungKhacOneViewModel managerNoiDungKhacOneViewModel = new ManagerNoiDungKhacOneViewModel(registrations, dateFrom, dateTo);
            return View(managerNoiDungKhacOneViewModel);
        }
        [Route("getNoiDungKhacById/{id}")]
        [HttpGet]
        public ActionResult GetNoiDungKhacById(int id)
        {
            Account account = (Account)Session[Utils.CommonConstant.USER_SESSION];
            if (account == null || account.RoleId != 2 && account.RoleId != 3)
            {
                return RedirectToRoute("login");
            }
            Registration registration = registrationRepository.GetRegistrationById(id);
            string subjectRegisted = string.Join(", ", subjectRegistedRepository.GetSubjectsRegistedsByRegistrationId(registration.Id).Select(s => s.Subject.Name));
            NoiDungKhacJson noiDungKhacJson = new NoiDungKhacJson(registration, subjectRegisted);
            var jsonRegistration = JsonConvert.SerializeObject(noiDungKhacJson,
           Formatting.None,
           new JsonSerializerSettings()
           {
               ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
           });
            return Json(jsonRegistration, JsonRequestBehavior.AllowGet);
        }
        [Route("chitiettrainghiemsangtao/{id}")]
        [HttpGet]
        public ActionResult GetDetailCreativeExp(int id)
        {
            Account account = (Account)Session[Utils.CommonConstant.USER_SESSION];
            if (account == null || account.RoleId != 2 && account.RoleId != 4 && account.RoleId != 5 && account.RoleId != 6 && account.RoleId != 7)
            {
                return RedirectToRoute("login");
            }
            RegistrationCreativeExp registrationCreativeExp = registrationCreativeExpRepository.GetRegistrationCreativeExpById(id);
            var jsonRegistrationCreativeExp = JsonConvert.SerializeObject(registrationCreativeExp,
           Formatting.None,
           new JsonSerializerSettings()
           {
               ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
           });
            return Json(jsonRegistrationCreativeExp, JsonRequestBehavior.AllowGet);
        }

        [Route("xuatexceltrainghiemsangtao/{dateFrom}/{dateTo}/{programId}")]
        [HttpGet]
        public async Task<ActionResult> ExportExcelCreativeExp(DateTime dateFrom, DateTime dateTo, int programId)
        {
            Account account = (Account)Session[Utils.CommonConstant.USER_SESSION];
            if (account == null || account.RoleId != 2 && account.RoleId != 4)
            {
                return RedirectToRoute("login");
            }
            List<RegistrationCreativeExp> registrationCreativeExps = registrationCreativeExpRepository.GetAllRegistrationCreativeExpByDateAndProgramId(dateFrom, dateTo, programId);
            string fileName = string.Concat("ds-trainghiemsangtao.xlsx");
            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Utils/Files/" + fileName);
            await Utils.ExportExcel.GenerateXLSRegistrationCreativeExp(registrationCreativeExps, dateFrom, dateTo, filePath);
            return File(filePath, "application/vnd.ms-excel", fileName);
        }

        [Route("xuatexcelnoidungkhac/{dateFrom}/{dateTo}")]
        [HttpGet]
        public async Task<ActionResult> ExportExcelRegistration(DateTime dateFrom, DateTime dateTo)
        {
            Account account = (Account)Session[Utils.CommonConstant.USER_SESSION];
            if (account == null || account.RoleId != 2 && account.RoleId != 3)
            {
                return RedirectToRoute("login");
            }
            List<Registration> registrations = registrationRepository.GetRegistrationsByDate(dateFrom, dateTo);
            string fileName = string.Concat("ds-noidungkhac.xlsx");
            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Utils/Files/" + fileName);

            List<SubjectsRegisted> subjectsRegisteds = new List<SubjectsRegisted>();
            foreach (var item in registrations)
            {
                List<SubjectsRegisted> subjectsRegistedsTmp = subjectRegistedRepository.GetSubjectsRegistedsByRegistrationId(item.Id);
                foreach (var item01 in subjectsRegistedsTmp)
                {
                    subjectsRegisteds.Add(item01);
                }

            }
            await Utils.ExportExcel.GenerateXLSRegistration(registrations, subjectsRegisteds, dateFrom, dateTo, filePath);
            return File(filePath, "application/vnd.ms-excel", fileName);
        }
        [Route("xuatexcelkinangsong/{dateFrom}/{dateTo}")]
        [HttpGet]
        public async Task<ActionResult> ExportSocialLifeSkill(DateTime dateFrom, DateTime dateTo)
        {
            Account account = (Account)Session[Utils.CommonConstant.USER_SESSION];
            if (account == null || account.RoleId != 2 && account.RoleId != 3)
            {
                return RedirectToRoute("login");
            }
            List<SocialLifeSkill> socialLifeSkills = socialLifeSkillRepository.GetSocialLifeSkillsByDate(dateFrom, dateTo);
            string fileName = string.Concat("ds-kynangsong.xlsx");
            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Utils/Files/" + fileName);
            await Utils.ExportExcel.GenerateXLSSocialLifeSkill(socialLifeSkills, dateFrom, dateTo, filePath);
            return File(filePath, "application/vnd.ms-excel", fileName);
        }

        [Route("downloadPDF/{fileName}")]
        [HttpGet]
        public ActionResult DownloadPDF(string fileName)
        {
            Account account = (Account)Session[Utils.CommonConstant.USER_SESSION];
            if (account == null)
            {
                return RedirectToRoute("login");
            }
            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/UploadedFiles/" + fileName.Trim() + ".pdf");
            return File(filePath, "application/pdf", fileName.Trim() + ".pdf");
        }
        [Route("kynangsong/{dateFrom}/{dateTo}")]
        [HttpGet]
        public ActionResult SocialLifeSkills(DateTime dateFrom, DateTime dateTo)
        {
            Account account = (Account)Session[Utils.CommonConstant.USER_SESSION];
            if (account == null || account.RoleId != 2 && account.RoleId != 3)
            {
                return RedirectToRoute("login");
            }
            List<SocialLifeSkill> socialLifeSkills = socialLifeSkillRepository.GetSocialLifeSkillsByDate(dateFrom, dateTo);
            ManagerSocialLifeSkillOneViewModel managerSocialLifeSkillOneViewModel = new ManagerSocialLifeSkillOneViewModel(socialLifeSkills, dateFrom, dateTo);
            return View(managerSocialLifeSkillOneViewModel);
        }
        [Route("chitietkynangsong/{id}")]
        [HttpGet]
        public ActionResult GetDetailSocialLifeSkill(int id)
        {
            Account account = (Account)Session[Utils.CommonConstant.USER_SESSION];
            if (account == null || account.RoleId != 2 && account.RoleId != 3)
            {
                return RedirectToRoute("login");
            }
            SocialLifeSkill socialLifeSkill = socialLifeSkillRepository.GetSocialLifeSkillById(id);
            var jsonSocialLifeSkill = JsonConvert.SerializeObject(socialLifeSkill,
           Formatting.None,
           new JsonSerializerSettings()
           {
               ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
           });
            return Json(jsonSocialLifeSkill, JsonRequestBehavior.AllowGet);
        }
        [Route("downloadPDFKynangsong/{fileName}")]
        [HttpGet]
        public ActionResult DownloadPDFSocialLifeSkill(string fileName)
        {
            Account account = (Account)Session[Utils.CommonConstant.USER_SESSION];
            if (account == null)
            {
                return RedirectToRoute("login");
            }
            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/UploadedFiles/SocialSkill/" + fileName.Trim() + ".pdf");
            return File(filePath, "application/pdf", fileName.Trim() + ".pdf");
        }
        [Route("dskhoahockithuat")]
        [HttpGet]
        public ActionResult DsKhoaHocKiThuat()
        {
            Account account = (Account)Session[Utils.CommonConstant.USER_SESSION];
            if (account == null || account.RoleId != 2 && account.RoleId != 1)
            {
                return RedirectToRoute("login");
            }
            List<KhoaHocKiThuatDetailDTO> khoaHocKiThuatDetailDTOs = kHKTKhoaHocKiThuatRepository.GetKhoaHocKiThuats();
            ViewBag.KHKT = khoaHocKiThuatDetailDTOs;
            return View();
        }
        [Route("taidskhoahockithuat")]
        [HttpGet]
        public async Task<ActionResult> TaoDsKhoaHocKithuat()
        {
            Account account = (Account)Session[Utils.CommonConstant.USER_SESSION];
            if (account == null && account.RoleId != 2 && account.RoleId != 1)
            {
                return RedirectToRoute("login");
            }
            List<KhoaHocKiThuatDetailDTO> khoaHocKiThuatDetailDTOs = kHKTKhoaHocKiThuatRepository.GetKhoaHocKiThuats();
            ViewBag.KHKT = khoaHocKiThuatDetailDTOs;
            string fileName = string.Concat("ds-khoahockythuat.xlsx");
            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Utils/Files/" + fileName);
            await Utils.ExportExcel.GenerateXLSKhoaHocKiThuat(khoaHocKiThuatDetailDTOs, filePath);
            return File(filePath, "application/vnd.ms-excel", fileName);
            
        }
    }
}