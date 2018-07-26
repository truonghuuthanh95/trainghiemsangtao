using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Models.DTO;

namespace TraiNghiemSangTao.Repositories.Interfaces
{
    public interface IRegistrationRepository
    {
        Registration SaveRegistration(RegistrationDTO registrationDTO, int id);
        Registration SaveFileUpload(string filekehoach, string filebaikiemtra, string filetailieuchohocsinh);
        bool CheckExistedFileKeHoach(string filekehoach);
        bool CheckExistedFileBaikiemtra(string filebaikiemtra);
        bool CheckExistedFiletailieuhocsinh(string filetailieuchohocsinh);
        List<Registration> GetRegistrationsByDate(DateTime dateFrom, DateTime dateTo);
        Registration GetRegistrationById(int id);
        Registration UpdateRegistration(RegistrationDTO registrationDTO, int id);
        bool CheckValidCodeRegisted(string codeRegisted);
        Registration GetRegistrationByCodeRegisted(string codeRegisted);
        Registration UpdateFileUpload(string filekehoach, string filebaikiemtra, string filetailieuchohocsinh, int id);
    }
}