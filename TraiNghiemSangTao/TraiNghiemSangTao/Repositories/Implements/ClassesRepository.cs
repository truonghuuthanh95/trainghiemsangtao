using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Repositories.Implements
{
    public class ClassesRepository : IClassesRepository
    {
        CreativeExpDB _db;

        public ClassesRepository(CreativeExpDB db)
        {
            _db = db;
        }

        public List<Class> GetClassBySchoolDegree(int? id)
        {
            List<Class> classes = _db.Classes.Where(s => s.SchoolDegreeId == id).ToList();
            return classes;
        }
    }
}