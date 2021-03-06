﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraiNghiemSangTao.Models.DAO;
using TraiNghiemSangTao.Models.DTO;

namespace TraiNghiemSangTao.Repositories.Interfaces
{
    public interface IRegistrationCreativeExpRepository
    {
        List<RegistrationCreativeExp> GetAllRegistrationCreativeExpByDateAndProgramId(DateTime dateFrom, DateTime dateTo, int programId);
        List<RegistrationCreativeExp> GetRegistrationCreativeExpByProgramId(int id);
        RegistrationCreativeExp GetRegistrationCreativeExpById(int id);
        RegistrationCreativeExp GetRegistrationCreativeExpByRegistedCode(string registedCode);
        RegistrationCreativeExp SaveRegistrationCreativeExp(CreativeExpDTO creativeExpDTO);
        Program GetRegistrationFirstIndex();
        int CheckValidQuantityStudent(int programId, int sesstionAdayId, DateTime time);

        bool GetValidRegistedCode(string registedCode);
        RegistrationCreativeExp UpdateRegistrationCreativeExp(CreativeExpDTO creativeExpDTO, int id);
        void ResetStudentQuantity(int id);
        bool DeleteRegistrationExp(int id);
    }
}