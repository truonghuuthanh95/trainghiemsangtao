using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Repositories.Implements
{
    public class SubjectRegistedRepository : ISubjectRegistedRepository
    {
        CreativeExpDB _db;

        public SubjectRegistedRepository(CreativeExpDB db)
        {
            _db = db;
        }

        public List<SubjectsRegisted> GetSubjectsRegistedsByRegistrationId(int id)
        {
            List<SubjectsRegisted> subjectsRegisteds = _db.SubjectsRegisteds.Include("Subject").Where(s => s.RegistrationId == id).ToList();
            return subjectsRegisteds;
        }

        public bool RemoveSunjectRegistedByRegistrationId(int id)
        {
            try
            {
                List<SubjectsRegisted> subjectsRegisteds = _db.SubjectsRegisteds.Where(s => s.RegistrationId == id).ToList();
                foreach (var item in subjectsRegisteds)
                {
                    _db.SubjectsRegisteds.Remove(item);
                    _db.SaveChanges();
                }
                
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
    }
}