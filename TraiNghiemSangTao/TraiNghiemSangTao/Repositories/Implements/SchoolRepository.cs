using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Repositories.Implements
{
    public class SchoolRepository : ISchoolRepository
    {
        CreativeExpDB _db;

        public SchoolRepository(CreativeExpDB db)
        {
            _db = db;
        }

        public List<School> GetSchoolByDistrictAndSchoolDegree(int districtId, int schoolDegreeId)
        {
            List<School> schools = null;
            if (schoolDegreeId == 4)
            {
                schools = _db.Schools.Where(s => s.SchoolDegreeId == 4 || s.SchoolDegreeId == 7).Where(s => s.DistrictId == districtId).ToList();
            }
            else if (schoolDegreeId == 5)
            {
                schools = _db.Schools.Where(s => s.SchoolDegreeId == 5 || s.SchoolDegreeId == 8).Where(s => s.DistrictId == districtId).ToList();
            }
            else
            {
                schools = _db.Schools.Where(s => s.SchoolDegreeId == schoolDegreeId).Where(s => s.DistrictId == districtId).ToList();
            }
            return schools;
        }
    }
}