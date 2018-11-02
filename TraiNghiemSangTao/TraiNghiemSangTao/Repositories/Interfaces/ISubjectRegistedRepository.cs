using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;

namespace TraiNghiemSangTao.Repositories.Interfaces
{
    public interface ISubjectRegistedRepository
    {
        List<SubjectsRegisted> GetSubjectsRegistedsByRegistrationId(int id);
        Boolean RemoveSunjectRegistedByRegistrationId(int id);
    }
}