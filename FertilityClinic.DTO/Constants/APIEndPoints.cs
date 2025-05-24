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
        public static class Users
        {
            public const string BaseUser = $"{Base}/user";
            public const string Create = $"{Base}/create";
            public const string GetAll = $"{BaseUser}/all";
            public const string GetById = $"{BaseUser}/{{id}}";
            public const string Update = $"{BaseUser}/update";
            public const string Delete = $"{BaseUser}/delete/{{id}}";
            public const string HardDelete = $"{BaseUser}/delete-hard/{{userId}}";
        }

        public static class Doctor
        {
            public const string BaseUser = $"{Base}/doctor";
            public const string Create = $"{Base}/create/{{id}}";
            public const string GetAll = $"{BaseUser}/all";
            public const string GetById = $"{BaseUser}/{{id}}";
            public const string Update = $"{BaseUser}/update";
            public const string Delete = $"{BaseUser}/delete/{{id}}";
        }

    }
}
