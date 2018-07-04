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
    public class RegistrationCreativeExpRepository : IRegistrationCreativeExpRepository
    {
        CreativeExpDB _db;

        public RegistrationCreativeExpRepository(CreativeExpDB db)
        {
            _db = db;
        }

        public int CheckValidQuantityStudent(int programId, int sesstionAdayId, DateTime time)
        {
            int studentJoinedAllDayNumb = 0;
            int studentJoinedHaftDaynumb = 0;
            var maxStudent = _db.Programs.Where(x => x.Id == programId).Select(x => x.MaxAudience).First();
            List<RegistrationCreativeExp> studentJoinedAllDay = _db.RegistrationCreativeExps
                .Where(x => x.DateRegisted == time)
                .Where(x => x.DaySessionId == 3).Where(s => s.ProgramId == programId).ToList();

            foreach (RegistrationCreativeExp registration in studentJoinedAllDay)
            {
                studentJoinedAllDayNumb += Convert.ToInt16(registration.StudentQuantity);
            }

            if (sesstionAdayId == 1 || sesstionAdayId == 2)
            {
                List<RegistrationCreativeExp> studentJoinedHaftDay = _db.RegistrationCreativeExps
                .Where(x => x.DateRegisted == time)
                .Where(x => x.DaySessionId == sesstionAdayId).Where(s => s.ProgramId == programId).ToList();
                foreach (RegistrationCreativeExp registration in studentJoinedHaftDay)
                {
                    studentJoinedHaftDaynumb += Convert.ToInt16(registration.StudentQuantity);
                }
            }
            else
            {
                int studentJoinedEvenningNumb = 0;
                int studentJoinedMorningNumb = 0;
                List<RegistrationCreativeExp> studentJoinedMorning = _db.RegistrationCreativeExps
                .Where(x => x.DateRegisted == time)
                .Where(x => x.DaySessionId == 1).Where(s => s.ProgramId == programId).ToList();

                List<RegistrationCreativeExp> studentJoinedEvenning = _db.RegistrationCreativeExps
                .Where(x => x.DateRegisted == time)
                .Where(x => x.DaySessionId == 2).Where(s => s.ProgramId == programId).ToList();

                foreach (RegistrationCreativeExp registration in studentJoinedEvenning)
                {
                    studentJoinedEvenningNumb += Convert.ToInt16(registration.StudentQuantity);
                }
                foreach (RegistrationCreativeExp registration in studentJoinedMorning)
                {
                    studentJoinedMorningNumb += Convert.ToInt16(registration.StudentQuantity);
                }

                if (studentJoinedMorningNumb >= studentJoinedEvenningNumb)
                {
                    studentJoinedHaftDaynumb = studentJoinedMorningNumb;
                }
                else
                {
                    studentJoinedHaftDaynumb = studentJoinedEvenningNumb;
                }

            }

            return Convert.ToInt16(maxStudent) - (studentJoinedAllDayNumb + studentJoinedHaftDaynumb);
        }

        public List<RegistrationCreativeExp> GetAllRegistrationCreativeExp()
        {
            throw new NotImplementedException();
        }

        public RegistrationCreativeExp GetRegistrationCreativeExpById(int id)
        {
            RegistrationCreativeExp registrationCreativeExp = _db.RegistrationCreativeExps.Where(s => s.Id == id).FirstOrDefault();
            return registrationCreativeExp;
        }

        public List<RegistrationCreativeExp> GetRegistrationCreativeExpByProgramId(int id)
        {
            throw new NotImplementedException();
        }

        public RegistrationCreativeExp GetRegistrationCreativeExpByRegistedCode(string registedCode)
        {
            RegistrationCreativeExp registrationCreativeExp = _db.RegistrationCreativeExps.Include("School").Where(s => s.CodeRegisted == registedCode).FirstOrDefault();
            return registrationCreativeExp;
        }

        public bool GetValidRegistedCode(string registedCode)
        {
            var isValid = _db.RegistrationCreativeExps.Where(s => s.CodeRegisted == registedCode).FirstOrDefault();
            if (isValid != null)
            {
                return true;
            }
            return false;
        }

        public RegistrationCreativeExp SaveRegistrationCreativeExp(CreativeExpDTO creativeExpDTO)
        {
            RegistrationCreativeExp registrationCreativeExp = new RegistrationCreativeExp();
            registrationCreativeExp.ClassId = creativeExpDTO.ClassId;
            registrationCreativeExp.CreatedAt = DateTime.Now;
            registrationCreativeExp.Creator = creativeExpDTO.Creator;
            registrationCreativeExp.DateRegisted = creativeExpDTO.DateRegisted;
            registrationCreativeExp.DaySessionId = creativeExpDTO.DaySessionId;
            registrationCreativeExp.Email = creativeExpDTO.Email;
            registrationCreativeExp.JobTiteId = creativeExpDTO.JobTiteId;
            registrationCreativeExp.PhoneNumber = creativeExpDTO.PhoneNumber;
            registrationCreativeExp.ProgramId = creativeExpDTO.ProgramId;
            registrationCreativeExp.SchoolDegreeId = creativeExpDTO.SchoolDegreeId;
            registrationCreativeExp.SchoolId = creativeExpDTO.SchoolId;
            registrationCreativeExp.StudentQuantity = creativeExpDTO.StudentQuantity;
            registrationCreativeExp.ActivitiTitle = creativeExpDTO.ActivitiTitle;
            registrationCreativeExp.CodeRegisted = Utils.RandomCodeRegisted.GetRandomString();
            var existedCodeRegisted = _db.RegistrationCreativeExps.Where(s => s.CodeRegisted == registrationCreativeExp.CodeRegisted);
            if (existedCodeRegisted != null)
            {                                
                do
                {
                    registrationCreativeExp.CodeRegisted = Utils.RandomCodeRegisted.GetRandomString();
                    var existedCodeRegistedSecond = _db.RegistrationCreativeExps.Where(s => s.CodeRegisted == registrationCreativeExp.CodeRegisted);
                } while (existedCodeRegisted == null);
            }
            _db.RegistrationCreativeExps.Add(registrationCreativeExp);
            _db.SaveChanges();
            return registrationCreativeExp;

        }

        public RegistrationCreativeExp UpdateRegistrationCreativeExp(CreativeExpDTO creativeExpDTO, int id)
        {
            RegistrationCreativeExp registrationCreativeExp = GetRegistrationCreativeExpById(id);
            registrationCreativeExp.ClassId = creativeExpDTO.ClassId;
            registrationCreativeExp.UpdatedAt = DateTime.Now;
            registrationCreativeExp.Creator = creativeExpDTO.Creator;
            registrationCreativeExp.DateRegisted = creativeExpDTO.DateRegisted;
            registrationCreativeExp.DaySessionId = creativeExpDTO.DaySessionId;
            registrationCreativeExp.Email = creativeExpDTO.Email;
            registrationCreativeExp.JobTiteId = creativeExpDTO.JobTiteId;
            registrationCreativeExp.PhoneNumber = creativeExpDTO.PhoneNumber;
            registrationCreativeExp.ProgramId = creativeExpDTO.ProgramId;
            registrationCreativeExp.SchoolDegreeId = creativeExpDTO.SchoolDegreeId;
            registrationCreativeExp.SchoolId = creativeExpDTO.SchoolId;
            registrationCreativeExp.StudentQuantity = creativeExpDTO.StudentQuantity;
            registrationCreativeExp.ActivitiTitle = creativeExpDTO.ActivitiTitle;
            _db.Entry(registrationCreativeExp).State = EntityState.Modified;
            try
            {
                _db.SaveChanges();
            }
            catch (Exception)
            {

                return registrationCreativeExp = null;
            }
            
            return registrationCreativeExp;
        }
    }
}