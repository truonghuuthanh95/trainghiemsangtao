using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Repositories.Implements
{
    public class ProvinceRepository : IProvinceRepository
    {
        CreativeExpDB _db;

        public ProvinceRepository(CreativeExpDB db)
        {
            _db = db;
        }

        public List<Province> GetProvinces()
        {
            List<Province> provinces = _db.Provinces.Where(s => s.CountryId == 237).OrderBy(s => s.Name).ToList();
            return provinces;
        }
    }
}