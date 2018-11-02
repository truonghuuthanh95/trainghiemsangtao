using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;

namespace TraiNghiemSangTao.Models.DTO
{
    public class ListRegistrationCreativeExpOneViewModel
    {
        public List<RegistrationCreativeExp> RegistrationCreativeExps { get; set; }
        public List<Program> Programs { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int ProgramId { get; set; }
        public ListRegistrationCreativeExpOneViewModel(List<RegistrationCreativeExp> registrationCreativeExps, List<Program> programs, DateTime dateFrom, DateTime dateTo, int programId)
        {
            RegistrationCreativeExps = registrationCreativeExps;
            Programs = programs;
            DateFrom = dateFrom;
            DateTo = dateTo;
            ProgramId = programId;
        }
    }
}