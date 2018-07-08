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
    public class NoidungkhacController : Controller
    {
        ISchoolRepository schoolRepository;
        ISubjectRepository subjectRepository;
        IJobTitleRepository jobTitleRepository;       
        IProvinceRepository provinceRepository;
        IDistrictRepository districtRepository;
        ISchoolDegreeRepository schoolDegreeRepository;
        IClassesRepository classesRepository;
        IRegistrationRepository registrationRepository;

        public NoidungkhacController(ISchoolRepository schoolRepository, ISubjectRepository subjectRepository, IJobTitleRepository jobTitleRepository, IProvinceRepository provinceRepository, IDistrictRepository districtRepository, ISchoolDegreeRepository schoolDegreeRepository, IClassesRepository classesRepository, IRegistrationRepository registrationRepository)
        {
            this.schoolRepository = schoolRepository;
            this.subjectRepository = subjectRepository;
            this.jobTitleRepository = jobTitleRepository;
            this.provinceRepository = provinceRepository;
            this.districtRepository = districtRepository;
            this.schoolDegreeRepository = schoolDegreeRepository;
            this.classesRepository = classesRepository;
            this.registrationRepository = registrationRepository;
        }





        // GET: Noidungkhac
        [Route("noidungkhac")]
        [HttpGet]
        public ActionResult Index()
        {
            List<Subject> subjects = subjectRepository.GetSubjects();
            List<School> schools = schoolRepository.GetSchoolByDistrictAndSchoolDegree(760, 3);
            List<Province> provinces = provinceRepository.GetProvinces();
            List<SchoolDegree> schoolDegrees = schoolDegreeRepository.GetSchoolDegrees();
            List<District> districts = districtRepository.GetDistricts();
            List<Class> classes = classesRepository.GetClassBySchoolDegree(3);
            List<Jobtitle> jobtitles = jobTitleRepository.GetJobtitles();
            NoiDungKhacOneViewModel noiDungKhacOneViewModel = new NoiDungKhacOneViewModel(null, schoolDegrees, provinces, schools, classes, jobtitles, subjects, districts);

            return View(noiDungKhacOneViewModel);
        }
        [Route("uploadFileNoiDungKhac")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult UploadFile(HttpPostedFileBase filekehoach, HttpPostedFileBase filebaikiemtra, HttpPostedFileBase filetailieuchohocsinh)
        {
           
            try
            {
                if (filekehoach.ContentLength > 0 && filebaikiemtra.ContentLength > 0 && filetailieuchohocsinh.ContentLength > 0)
                {
                    string _filebaikiemtra = Path.GetFileName(filebaikiemtra.FileName);
                    string _filekehoach = Path.GetFileName(filekehoach.FileName);
                    string _filetailieuchohocsinh = Path.GetFileName(filetailieuchohocsinh.FileName);
                    bool existedFilebaikiemtra = registrationRepository.CheckExistedFileBaikiemtra(_filebaikiemtra);
                    bool existedFilekehoach = registrationRepository.CheckExistedFileKeHoach(_filekehoach);
                    bool existedFiletailieuhs = registrationRepository.CheckExistedFiletailieuhocsinh(_filetailieuchohocsinh);

                    if (existedFilebaikiemtra == true)
                    {
                        return Json("failedfilebaikiemtra");
                    }
                    if (existedFilekehoach == true)
                    {
                        return Json("failedfilekehoach");
                    }
                    if (existedFiletailieuhs == true)
                    {
                        return Json("failedfiletailieuchohocsinh");
                    }
                    string _path1 = Path.Combine(Server.MapPath("~/UploadedFiles"), _filebaikiemtra);
                    string _path2 = Path.Combine(Server.MapPath("~/UploadedFiles"), _filekehoach);
                    string _path3 = Path.Combine(Server.MapPath("~/UploadedFiles"), _filetailieuchohocsinh);

                    filebaikiemtra.SaveAs(_path1);
                    filekehoach.SaveAs(_path2);
                    filetailieuchohocsinh.SaveAs(_path3);
                    Registration registration = registrationRepository.SaveFileUpload(_filekehoach, _filebaikiemtra, _filetailieuchohocsinh);
                    return Json(registration.Id);
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

        [HttpPost]
        [Route("postNoiDungKhac")]
        [ValidateAntiForgeryToken]
        public ActionResult PostNoiDungKhac(RegistrationDTO registrationDTO, int Id)
        {
            Registration registration = registrationRepository.SaveRegistration(registrationDTO, Id);
            return Json(registration.CodeRegisted);
        }
    }
}