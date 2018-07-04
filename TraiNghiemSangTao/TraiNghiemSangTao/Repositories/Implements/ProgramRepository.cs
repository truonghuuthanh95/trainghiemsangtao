using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Repositories.Implements
{
    public class ProgramRepository : IProgramRepository
    {
        CreativeExpDB _db;

        public ProgramRepository(CreativeExpDB db)
        {
            _db = db;
        }

        public List<Program> GetPrograms()
        {
            List<Program> programs = _db.Programs.Where(s => s.IsActive == true).ToList();
            return programs;
        }
    }
}