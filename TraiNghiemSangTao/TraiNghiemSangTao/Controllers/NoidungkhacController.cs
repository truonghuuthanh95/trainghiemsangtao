using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Models.DTO;
using TraiNghiemSangTao.Repositories.Interfaces;
using TraiNghiemSangTao.Utils;

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
        ISubjectRegistedRepository subjectRegistedRepository;

        public NoidungkhacController(ISchoolRepository schoolRepository, ISubjectRepository subjectRepository, IJobTitleRepository jobTitleRepository, IProvinceRepository provinceRepository, IDistrictRepository districtRepository, ISchoolDegreeRepository schoolDegreeRepository, IClassesRepository classesRepository, IRegistrationRepository registrationRepository, ISubjectRegistedRepository subjectRegistedRepository)
        {
            this.schoolRepository = schoolRepository;
            this.subjectRepository = subjectRepository;
            this.jobTitleRepository = jobTitleRepository;
            this.provinceRepository = provinceRepository;
            this.districtRepository = districtRepository;
            this.schoolDegreeRepository = schoolDegreeRepository;
            this.classesRepository = classesRepository;
            this.registrationRepository = registrationRepository;
            this.subjectRegistedRepository = subjectRegistedRepository;
        }

        // GET: Noidungkhac
        [Route("noidungkhac")]
        [HttpGet]
        public ActionResult Index()
        {
            List<Subject> subjects = subjectRepository.GetSubjects();
            List<School> schools = schoolRepository.GetSchoolByDistrictAndSchoolDegree(760, 2);
            List<Province> provinces = provinceRepository.GetProvinces();
            List<SchoolDegree> schoolDegrees = schoolDegreeRepository.GetSchoolDegrees();
            List<District> districts = districtRepository.GetDistricts();
            List<Class> classes = classesRepository.GetClassBySchoolDegree(2);
            List<Jobtitle> jobtitles = jobTitleRepository.GetJobtitles();
            NoiDungKhacOneViewModel noiDungKhacOneViewModel = new NoiDungKhacOneViewModel(null, null, schoolDegrees, provinces, schools, classes, jobtitles, subjects, districts);

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
            SendMailService sendMailService = new SendMailService();
            Registration registrationDetail = registrationRepository.GetRegistrationById(Convert.ToInt32(registration.Id));
            sendMailService.SendMailRegistration(registrationDetail);
            return Json(registration.CodeRegisted);
        }
        [Route("capnhatnoidungkhac", Name = "capnhatnoidungkhac")]
        [HttpGet]
        public ActionResult CheckCodeRegisted()
        {
            return View();
        }
        [Route("kiemtramadangkinoidungkhac/{codeRegisted}")]
        [HttpGet]
        public Boolean CheckValidCodeRegisted(string codeRegisted)
        {
            return registrationRepository.CheckValidCodeRegisted(codeRegisted.Trim());
        }

        [Route("capnhatnoidungkhac/{codeRegisted}")]
        [HttpGet]
        public ActionResult UpdateNoiDungKhac(string codeRegisted)
        {
            Registration registration = registrationRepository.GetRegistrationByCodeRegisted(codeRegisted.Trim());
            if (registration == null)
            {
                return RedirectToRoute("capnhatnoidungkhac");
            }
            List<SubjectsRegisted> subjectsRegisteds = subjectRegistedRepository.GetSubjectsRegistedsByRegistrationId(registration.Id);
            List<Subject> subjects = subjectRepository.GetSubjects();
            List<School> schools = schoolRepository.GetSchoolByDistrictAndSchoolDegree(registration.School.DistrictId, registration.SchoolDegreeId);
            List<Province> provinces = provinceRepository.GetProvinces();
            List<SchoolDegree> schoolDegrees = schoolDegreeRepository.GetSchoolDegrees();
            List<District> districts = districtRepository.GetDistricts();
            List<Class> classes = classesRepository.GetClassBySchoolDegree(registration.SchoolDegreeId);
            List<Jobtitle> jobtitles = jobTitleRepository.GetJobtitles();
            NoiDungKhacOneViewModel noiDungKhacOneViewModel = new NoiDungKhacOneViewModel(registration, subjectsRegisteds, schoolDegrees, provinces, schools, classes, jobtitles, subjects, districts);

            return View(noiDungKhacOneViewModel);
        }
        [Route("updatefileupload")]
        [HttpPost]
        public ActionResult UpdateFileUpload(HttpPostedFileBase filekehoach, HttpPostedFileBase filebaikiemtra, HttpPostedFileBase filetailieuchohocsinh, int id)
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

                    Registration registration = registrationRepository.UpdateFileUpload(_filekehoach, _filebaikiemtra, _filetailieuchohocsinh, id);
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
        [Route("postcapnhatnoidungkhac")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostCapNhatNoiDungKhac(RegistrationDTO registrationDTO, int id)
        {
            registrationRepository.UpdateRegistration(registrationDTO, id);
            return Json("200");
        }
        [Route("xoahoatdongkhac/{id}")]
        [HttpGet]
        public ActionResult XoaHoatDongKhac(int id)
        {
            Registration registrationCreativeExp = registrationRepository.GetRegistrationById(id);
            if (registrationCreativeExp == null)
            {
                return Json("404", JsonRequestBehavior.AllowGet);
            }
            bool deleted = registrationRepository.DeleteRegistration(id);
            if (deleted == true)
            {
                return Json("200");
            }
            return Json("400");
        }

    }
}