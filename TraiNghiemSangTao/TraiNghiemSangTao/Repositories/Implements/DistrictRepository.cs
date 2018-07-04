using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Repositories.Implements
{
    public class DistrictRepository : IDistrictRepository
    {
        CreativeExpDB _db;

        public DistrictRepository(CreativeExpDB db)
        {
            _db = db;
        }

        public List<District> GetDistricts()
        {
            List<District> districts = _db.Districts.Where(s => s.ProvinceId == 79).OrderBy(s => s.Name).ToList();
            return districts;
        }
    }
}