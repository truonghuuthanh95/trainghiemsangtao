using System;
using System.Collections.Generic;
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

        public Registration SaveRegistration(RegistrationDTO registrationDTO)
        {
            string[] arraySubject = registrationDTO.SubjectSelected.Split(new char[] { ',' });

            Registration registration = new Registration();
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
            _db.Registrations.Add(registration);
            _db.SaveChanges();
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