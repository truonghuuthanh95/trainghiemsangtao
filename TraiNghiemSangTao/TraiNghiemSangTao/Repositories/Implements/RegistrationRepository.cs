using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Models.DTO;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Repositories.Implements
{
    public class RegistrationRepository : IRegistrationRepository
    {
        CreativeExpDB _db;
        SubjectRegistedRepository SubjectRegistedRepository;

        public RegistrationRepository(CreativeExpDB db, SubjectRegistedRepository subjectRegistedRepository)
        {
            _db = db;
            SubjectRegistedRepository = subjectRegistedRepository;
        }

        public bool CheckExistedFileBaikiemtra(string filebaikiemtra)
        {
            Registration existedFilebaikiemtra = _db.Registrations
                .Where(s => s.FileBaiKiemTra == filebaikiemtra || s.FileTaiLieuChoHS == filebaikiemtra || s.FileKeHoach == filebaikiemtra).FirstOrDefault();
            if (existedFilebaikiemtra == null)
            {
                return false;
            }
            return true;

        }

        public bool CheckExistedFileKeHoach(string filekehoach)
        {
            Registration existedfilekehoach = _db.Registrations
                .Where(s => s.FileKeHoach == filekehoach || s.FileBaiKiemTra == filekehoach || s.FileTaiLieuChoHS == filekehoach)
                .FirstOrDefault();
            if (existedfilekehoach == null)
            {
                return false;
            }
            return true;
        }

        public bool CheckExistedFiletailieuhocsinh(string filetailieuchohocsinh)
        {
            Registration existedFiletailieuhocsinh = _db.Registrations
                .Where(s => s.FileTaiLieuChoHS == filetailieuchohocsinh || s.FileKeHoach == filetailieuchohocsinh || s.FileBaiKiemTra == filetailieuchohocsinh).FirstOrDefault();
            if (existedFiletailieuhocsinh == null)
            {
                return false;
            }
            return true;
        }

        public bool CheckValidCodeRegisted(string codeRegisted)
        {
            var isValid = _db.Registrations.Where(s => s.CodeRegisted == codeRegisted).FirstOrDefault();
            if (isValid != null)
            {
                return true;
            }
            return false;
        }

        public Registration GetRegistrationByCodeRegisted(string codeRegisted)
        {
            Registration registration = _db.Registrations
                .Include("School")
                .Include("Jobtitle")
                .Include("Province")
                .Include("Class").Where(s => s.CodeRegisted == codeRegisted).SingleOrDefault();
            return registration;
        }

        public Registration GetRegistrationById(int id)
        {
            Registration registration = _db.Registrations
                .Include("School")
                .Include("Jobtitle")
                .Include("Class")
                .Include("Province").Where(s => s.Id == id).FirstOrDefault();
            return registration;
        }

        public List<Registration> GetRegistrationsByDate(DateTime dateFrom, DateTime dateTo)
        {
            List<Registration> registrations = _db.Registrations.Include("School")
                .Include("Jobtitle")
                .Include("Province")
                .Include("Class")
                .Where(s => s.DateRegisted >= dateFrom && s.DateRegisted <= dateTo).OrderBy(s => s.DateRegisted).ToList();
            return registrations;
        }

        public Registration SaveFileUpload(string filekehoach, string filebaikiemtra, string filetailieuchohocsinh)
        {
            Registration registration = new Registration();
            registration.FileBaiKiemTra = filebaikiemtra;
            registration.FileKeHoach = filekehoach;
            registration.FileTaiLieuChoHS = filetailieuchohocsinh;
            _db.Registrations.Add(registration);
            _db.SaveChanges();
            return registration;
        }

        public Registration SaveRegistration(RegistrationDTO registrationDTO, int id)
        {
            Registration registration = _db.Registrations.Where(s => s.Id == id).FirstOrDefault();
            string[] arraySubject = registrationDTO.SubjectSelected.Split(new char[] { ',' });

            registration.ClassId = registrationDTO.ClassId;
            registration.CreatedAt = DateTime.Now;
            registration.Creator = registrationDTO.Creator;
            registration.DateRegisted = registrationDTO.DateRegisted;
            registration.Email = registrationDTO.Email;
            registration.JobTitleId = registrationDTO.JobTitleId;
            registration.PhoneNumber = registrationDTO.PhoneNumber;
            registration.ProgramName = registrationDTO.ProgramName;
            registration.ProvinceId = registrationDTO.ProvinceId;
            registration.SchoolDegreeId = registrationDTO.SchoolDegreeId;
            registration.SchoolId = registrationDTO.SchoolId;
            registration.StudentQuantity = registrationDTO.StudentQuantity;
            registration.ViTriKienThuc = registrationDTO.ViTriKienThuc;
            registration.TomTatNoiDungCT = registrationDTO.TomTatNoiDungCT;
            registration.CodeRegisted = Utils.RandomCodeRegisted.GetRandomString();
            var existedCodeRegisted = _db.Registrations.Where(s => s.CodeRegisted == registration.CodeRegisted);
            if (existedCodeRegisted != null)
            {
                do
                {
                    registration.CodeRegisted = Utils.RandomCodeRegisted.GetRandomString();
                    var existedCodeRegistedSecond = _db.RegistrationCreativeExps.Where(s => s.CodeRegisted == registration.CodeRegisted);
                } while (existedCodeRegisted == null);
            }
            _db.Entry(registration).State = EntityState.Modified;
            try
            {
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return registration = null;
            }
            
            foreach (var item in arraySubject)
            {
                SubjectsRegisted subjectsRegisted = new SubjectsRegisted();
                subjectsRegisted.RegistrationId = registration.Id;
                subjectsRegisted.SubjectId = Convert.ToInt32(item);
                _db.SubjectsRegisteds.Add(subjectsRegisted);
                _db.SaveChanges();
            }
            return registration;

        }

        public Registration UpdateFileUpload(string filekehoach, string filebaikiemtra, string filetailieuchohocsinh, int id)
        {
            Registration registration = GetRegistrationById(id);
            registration.FileBaiKiemTra = filebaikiemtra;
            registration.FileKeHoach = filekehoach;
            registration.FileTaiLieuChoHS = filetailieuchohocsinh;
            _db.Entry(registration).State = EntityState.Modified;
            try
            {
                _db.SaveChanges();
            }
            catch (Exception)
            {

                return registration = null;

            }
            return registration;
        }

        public Registration UpdateRegistration(RegistrationDTO registrationDTO, int id)
        {
            SubjectRegistedRepository.RemoveSunjectRegistedByRegistrationId(id);
            Registration registration = _db.Registrations.Where(s => s.Id == id).FirstOrDefault();
            string[] arraySubject = registrationDTO.SubjectSelected.Split(new char[] { ',' });
            registration.ClassId = registrationDTO.ClassId;
            registration.CreatedAt = DateTime.Now;
            registration.Creator = registrationDTO.Creator;
            registration.DateRegisted = registrationDTO.DateRegisted;
            registration.Email = registrationDTO.Email;
            registration.JobTitleId = registrationDTO.JobTitleId;
            registration.PhoneNumber = registrationDTO.PhoneNumber;
            registration.ProgramName = registrationDTO.ProgramName;
            registration.ProvinceId = registrationDTO.ProvinceId;
            registration.SchoolDegreeId = registrationDTO.SchoolDegreeId;
            registration.SchoolId = registrationDTO.SchoolId;
            registration.StudentQuantity = registrationDTO.StudentQuantity;
            registration.ViTriKienThuc = registrationDTO.ViTriKienThuc;
            registration.TomTatNoiDungCT = registrationDTO.TomTatNoiDungCT;
            
            _db.Entry(registration).State = EntityState.Modified;
            try
            {
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return registration = null;
            }
            
            foreach (var item in arraySubject)
            {
                SubjectsRegisted subjectsRegisted = new SubjectsRegisted();
                subjectsRegisted.RegistrationId = registration.Id;
                subjectsRegisted.SubjectId = Convert.ToInt32(item);
                _db.SubjectsRegisteds.Add(subjectsRegisted);
                _db.SaveChanges();
            }
            return registration;

        }

       
    }
}