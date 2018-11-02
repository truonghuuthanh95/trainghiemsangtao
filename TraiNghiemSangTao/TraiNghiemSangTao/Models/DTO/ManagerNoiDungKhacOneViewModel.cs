using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;

namespace TraiNghiemSangTao.Models.DTO
{
    public class ManagerNoiDungKhacOneViewModel
    {
        public List<Registration> Registrations { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public ManagerNoiDungKhacOneViewModel(List<Registration> registrations, DateTime dateFrom, DateTime dateTo)
        {
            Registrations = registrations;
            DateFrom = dateFrom;
            DateTo = dateTo;
        }
    }
}