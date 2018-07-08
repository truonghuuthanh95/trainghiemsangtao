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

        public RegistrationRepository(CreativeExpDB db)
        {
            _db = db;
        }

        public bool CheckExistedFileBaikiemtra(string filebaikiemtra)
        {
            Registration existedFilebaikiemtra = _db.Registrations.Where(s => s.FileBaiKiemTra == filebaikiemtra).FirstOrDefault();
            if (existedFilebaikiemtra == null)
            {
                return false;
            }
            return true;

        }

        public bool CheckExistedFileKeHoach(string filekehoach)
        {
            Registration existedfilekehoach = _db.Registrations.Where(s => s.FileKeHoach == filekehoach).FirstOrDefault();
            if (existedfilekehoach == null)
            {
                return false;
            }
            return true;
        }

        public bool CheckExistedFiletailieuhocsinh(string filetailieuchohocsinh)
        {
            Registration existedFiletailieuhocsinh = _db.Registrations.Where(s => s.FileTaiLieuChoHS == filetailieuchohocsinh).FirstOrDefault();
            if (existedFiletailieuhocsinh == null)
            {
                return false;
            }
            return true;
        }

        public List<Registration> GetRegistrations()
        {
            List<Registration> registrations = _db.Registrations.Include("School")
                .Include("Jobtitle")
                .Include("Province")
                .Where(s => s.DateRegisted >= DateTime.Now).ToList();
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
    }
}