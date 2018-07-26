using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Models.DTO;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Controllers
{
    public class SocialLifeSkillController : Controller
    {
        ISchoolRepository schoolRepository;       
        IJobTitleRepository jobTitleRepository;        
        IDistrictRepository districtRepository;
        ISchoolDegreeRepository schoolDegreeRepository;
        IClassesRepository classesRepository;
        ISocialLifeSkillRepository socialLifeSkillRepository;

        public SocialLifeSkillController(ISchoolRepository schoolRepository, IJobTitleRepository jobTitleRepository, IDistrictRepository districtRepository, ISchoolDegreeRepository schoolDegreeRepository, IClassesRepository classesRepository, ISocialLifeSkillRepository socialLifeSkillRepository)
        {
            this.schoolRepository = schoolRepository;
            this.jobTitleRepository = jobTitleRepository;
            this.districtRepository = districtRepository;
            this.schoolDegreeRepository = schoolDegreeRepository;
            this.classesRepository = classesRepository;
            this.socialLifeSkillRepository = socialLifeSkillRepository;
        }


        // GET: SocialLifeSkill
        [Route("kynangsong")]
        public ActionResult Index()
        {
            
            List<School> schools = schoolRepository.GetSchoolByDistrictAndSchoolDegree(760, 3);           
            List<SchoolDegree> schoolDegrees = schoolDegreeRepository.GetSchoolDegrees();
            List<District> districts = districtRepository.GetDistricts();
            List<Class> classes = classesRepository.GetClassBySchoolDegree(3);
            List<Jobtitle> jobtitles = jobTitleRepository.GetJobtitles();
            SocialLifeSkillOneViewModel socialLifeSkillOneViewModel = new SocialLifeSkillOneViewModel(null,schoolDegrees, schools, classes, jobtitles, districts);

            return View(socialLifeSkillOneViewModel);
        }
        [Route("uploadfilekynangsong")]
        [HttpPost]
        public ActionResult UploadFileSocialLifeSkill(HttpPostedFileBase fileKeHoach)
        {
            try
            {
                if (fileKeHoach.ContentLength > 0)
                {

                    string _filekehoach = Path.GetFileName(fileKeHoach.FileName);

                    bool existedFilekehoach = socialLifeSkillRepository.CheckExistedFileKeHoach(_filekehoach);
                    if (existedFilekehoach == true)
                    {
                        return Json("failedFileKeHoach");
                    }
                    string _path2 = Path.Combine(Server.MapPath("~/UploadedFiles/SocialSkill"), _filekehoach);
                    fileKeHoach.SaveAs(_path2);
                    SocialLifeSkill socialLifeSkill = socialLifeSkillRepository.CreateSocialLifeSkillWithFile(_filekehoach);
                    return Json(socialLifeSkill.Id);
                }
                else
                {
                    return Json("failed");
                }
            }
            catch
            {
                return Json("failed");
            }
        }

        [Route("taomoikynangsong")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSocialLifeSkill(SocialLifeSkillDTO socialLifeSkillDTO, int id)
        {
            SocialLifeSkill socialLifeSkill = socialLifeSkillRepository.UpdateSocialLifeSkill(socialLifeSkillDTO, id);           
            return Json(socialLifeSkill.CodeRegisted);
        }

        [Route("capnhatkinangsong", Name = "capnhatkinangsong")]
        [HttpGet]
        public ActionResult CheckCodeRegisted()
        {
            return View();
        }

        [Route("kiemtramakinangsong/{codeRegisted}")]
        [HttpGet]
        public Boolean CheckCodeRegisted(string codeRegisted)
        {
           return socialLifeSkillRepository.CheckValidCodeRegisted(codeRegisted.Trim());
                  
        }
        [Route("capnhatkinangsong/{codeRegisted}")]
        [HttpGet]
        public ActionResult UpdateSocialLifeSkill(string codeRegisted)
        {
            SocialLifeSkill socialLifeSkill = socialLifeSkillRepository.GetSocialLifeSkillByRegistedCode(codeRegisted.Trim());
            if (socialLifeSkill == null)
            {
                return RedirectToRoute("capnhatkinangsong");
            }
            List<School> schools = schoolRepository.GetSchoolByDistrictAndSchoolDegree(socialLifeSkill.School.DistrictId, socialLifeSkill.SchoolDegreeId);
            List<SchoolDegree> schoolDegrees = schoolDegreeRepository.GetSchoolDegrees();
            List<District> districts = districtRepository.GetDistricts();
            List<Class> classes = classesRepository.GetClassBySchoolDegree(socialLifeSkill.SchoolDegreeId);
            List<Jobtitle> jobtitles = jobTitleRepository.GetJobtitles();
            
            SocialLifeSkillOneViewModel socialLifeSkillOneViewModel = new SocialLifeSkillOneViewModel(socialLifeSkill, schoolDegrees, schools, classes, jobtitles, districts);
            return View(socialLifeSkillOneViewModel);
        }
        [Route("postcapnhatkinangsong")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Postcapnhattrainghiemsangtao(SocialLifeSkillDTO socialLifeSkillDTO, int id)
        {
            SocialLifeSkill socialLifeSkill = socialLifeSkillRepository.UpdateSocialLifeSkillWithoutCode(socialLifeSkillDTO, id);
            return Json("200");
        }
        
        [Route("uploadnewfilekehoach")]
        [HttpPost]
        public ActionResult UploadNewFileKeHoach(HttpPostedFileBase fileKeHoach, int id)
        {
            try
            {
                if (fileKeHoach.ContentLength > 0)
                {
                    string _filekehoach = Path.GetFileName(fileKeHoach.FileName);
                    bool existedFilekehoach = socialLifeSkillRepository.CheckExistedFileKeHoach(_filekehoach);
                    if (existedFilekehoach == true)
                    {
                        return Json("failedFileKeHoach");
                    }
                    string _path2 = Path.Combine(Server.MapPath("~/UploadedFiles/SocialSkill"), _filekehoach);
                    fileKeHoach.SaveAs(_path2);
                    SocialLifeSkill socialLifeSkill = socialLifeSkillRepository.UpdateSocialLifeSkillFileKeHoachById(id, _filekehoach);
                    return Json(socialLifeSkill.Id);
                }
                else
                {
                    return Json("failed");
                }
            }
            catch
            {
                return Json("failed");
            }
        }
        }
  
}