using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportSyncApp.dataConstLink
{
    internal class Api
    {
        private const string linkurl = "https://lnvm4w7m-7112.euw.devtunnels.ms/api/Login/";

        public const string loginurl = linkurl + "Log";
        public const string regurl = linkurl + "Register";
        public const string deleteurl = linkurl + "Del";
        public const string geturl = linkurl + "GetDailyW/";
        public const string ChangeURL = linkurl + "Ch";
    }
}
