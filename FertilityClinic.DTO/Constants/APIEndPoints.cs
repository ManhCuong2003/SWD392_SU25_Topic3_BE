using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Constants
{
    public static class APIEndPoints
    {
        public const string Base = "api";

        public static class Auth
        {
            public const string Login = $"{Base}/auth/login";
            public const string Register = $"{Base}/auth/register";
        }

        public static class Debug
        {
            public const string CheckDb = $"{Base}/debug/db-check";
        }
    }
}
