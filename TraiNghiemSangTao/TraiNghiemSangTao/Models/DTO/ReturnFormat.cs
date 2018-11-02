using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraiNghiemSangTao.Models.DTO
{
    public class ReturnFormat
    {
        public int StatusCode { get; set; }
        public String Message { get; set; }
        public object Result { get; set; }

        public ReturnFormat(int statusCode, string message, object result)
        {
            StatusCode = statusCode;
            Message = message;
            Result = result;
        }
    }
}