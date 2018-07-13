﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        IRegistrationRepository registrationRepository;
        ISubjectRegistedRepository subjectRegistedRepository;

        public ManagerController(IRegistrationCreativeExpRepository registrationCreativeExpRepository, IProgramRepository programRepository, IRegistrationRepository registrationRepository, ISubjectRegistedRepository subjectRegistedRepository)
        {
            this.registrationCreativeExpRepository = registrationCreativeExpRepository;
            this.programRepository = programRepository;
            this.registrationRepository = registrationRepository;
            this.subjectRegistedRepository = subjectRegistedRepository;
        }


        // GET: Manager 
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
        [Route("trainghiemsangtao/{dateFrom}/{dateTo}/{programId}")]
        [HttpGet]
        public ActionResult TraiNghiemSangTao(DateTime dateFrom, DateTime dateTo, int programId)
        {
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
            List<Registration> registrations = registrationRepository.GetRegistrationsByDate(dateFrom, dateTo);
            ManagerNoiDungKhacOneViewModel managerNoiDungKhacOneViewModel = new ManagerNoiDungKhacOneViewModel(registrations, dateFrom, dateTo);
            return View(managerNoiDungKhacOneViewModel);
        }
        [Route("getNoiDungKhacById/{id}")]
        [HttpGet]
        public ActionResult GetNoiDungKhacById(int id)
        {
            Registration registration = registrationRepository.GetRegistrationById(id);
            string subjectRegisted = string.Join(", " ,subjectRegistedRepository.GetSubjectsRegistedsByRegistrationId(registration.Id).Select(s => s.Subject.Name));
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
        [Route("xuatexcelkinangsong/{dateFrom}/{dateTo}/{programId}")]
        [HttpGet]
        public async Task<ActionResult> ExportSocialLifeSkill(DateTime dateFrom, DateTime dateTo, int programId)
        {
            List<RegistrationCreativeExp> registrationCreativeExps = registrationCreativeExpRepository.GetAllRegistrationCreativeExpByDateAndProgramId(dateFrom, dateTo, programId);
            string fileName = string.Concat("ds-kynangsong.xlsx");
            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Utils/Files/" + fileName);
            await Utils.ExportExcel.GenerateXLSRegistrationCreativeExp(registrationCreativeExps, dateFrom, dateTo, filePath);
            return File(filePath, "application/vnd.ms-excel", fileName);
        }

        [Route("downloadPDF/{fileName}")]
        [HttpGet]
        public ActionResult DownloadPDF(string fileName)
        {            
            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/UploadedFiles/" + fileName.Trim()+".pdf");
            return File(filePath, "application/pdf", fileName.Trim()+".pdf");
        }

    }
}