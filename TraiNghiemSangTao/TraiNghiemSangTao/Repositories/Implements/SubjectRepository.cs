using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Repositories.Implements
{
    public class SubjectRepository : ISubjectRepository
    {
        CreativeExpDB _db;

        public SubjectRepository(CreativeExpDB db)
        {
            _db = db;
        }

        public List<Subject> GetSubjects()
        {
            List<Subject> subjects = _db.Subjects.Where(s => s.IsActive == true).ToList();
            return subjects;
        }
    }
}