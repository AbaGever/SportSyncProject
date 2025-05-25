using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportSyncApp.dataConstLink
{
    internal class Api
    {
        private const string linkurl = "https://16dpzg74-7112.uks1.devtunnels.ms/api/Login/";

        public const string loginurl = linkurl + "Log";
        public const string regurl = linkurl + "Register";
        public const string deleteurl = linkurl + "Del";
        public const string geturl = linkurl + "GetDailyW/";
        public const string ChangeURL = linkurl + "Ch";
    }
}
