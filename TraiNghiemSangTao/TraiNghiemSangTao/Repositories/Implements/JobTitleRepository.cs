using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Repositories.Interfaces;

namespace TraiNghiemSangTao.Repositories.Implements
{
    public class JobTitleRepository : IJobTitleRepository
    {
        CreativeExpDB _db;

        public JobTitleRepository(CreativeExpDB db)
        {
            _db = db;
        }

        public List<Jobtitle> GetJobtitles()
        {
            List<Jobtitle> jobtitles = _db.Jobtitles.Where(s => s.IsActive == true).ToList();
            return jobtitles;
        }
    }
}