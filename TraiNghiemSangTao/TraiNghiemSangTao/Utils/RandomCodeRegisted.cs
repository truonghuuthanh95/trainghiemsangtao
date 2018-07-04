using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TraiNghiemSangTao.Utils
{
    public class RandomCodeRegisted
    {
        public static string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path;
        }
    }
}