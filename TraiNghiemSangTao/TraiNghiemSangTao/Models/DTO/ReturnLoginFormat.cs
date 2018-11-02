using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraiNghiemSangTao.Models.DTO
{
    public class ReturnLoginFormat
    {
        public int StatusCode { get; set; }
        public String Message { get; set; }
        public object Results { get; set; }

        public ReturnLoginFormat(int statusCode, string message, object results)
        {
            StatusCode = statusCode;
            Message = message;
            Results = results;
        }
    }
}