using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Repositories.Implements
{
    public class SchoolDegreeRepository : ISchoolDegreeRepository
    {
        CreativeExpDB _db;

        public SchoolDegreeRepository(CreativeExpDB db)
        {
            _db = db;
        }

        public List<SchoolDegree> GetSchoolDegrees()
        {
            List<SchoolDegree> schoolDegrees = _db.SchoolDegrees.Where(s => s.IsActive == true).ToList();
            return schoolDegrees;
        }
    }
}