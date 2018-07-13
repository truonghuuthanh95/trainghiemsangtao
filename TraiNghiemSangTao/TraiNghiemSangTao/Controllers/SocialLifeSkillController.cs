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
    public class SocialLifeSkillController : Controller
    {
        ISchoolRepository schoolRepository;       
        IJobTitleRepository jobTitleRepository;        
        IDistrictRepository districtRepository;
        ISchoolDegreeRepository schoolDegreeRepository;
        IClassesRepository classesRepository;

        public SocialLifeSkillController(ISchoolRepository schoolRepository, IJobTitleRepository jobTitleRepository, IDistrictRepository districtRepository, ISchoolDegreeRepository schoolDegreeRepository, IClassesRepository classesRepository)
        {
            this.schoolRepository = schoolRepository;
            this.jobTitleRepository = jobTitleRepository;
            this.districtRepository = districtRepository;
            this.schoolDegreeRepository = schoolDegreeRepository;
            this.classesRepository = classesRepository;
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
            SocialLifeSkillOneViewModel socialLifeSkillOneViewModel = new SocialLifeSkillOneViewModel(schoolDegrees, schools, classes, jobtitles, districts);

            return View(socialLifeSkillOneViewModel);
        }
    }
}