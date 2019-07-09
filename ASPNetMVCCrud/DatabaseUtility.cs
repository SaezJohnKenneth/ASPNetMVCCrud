using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ASPNetMVCCrud
{
    static class DatabaseUtility
    {
        public static string ConnectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
    }
}