using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSJu.Utility
{
    public class ApiConst
    {
        public const string Issuer = "JUExam";
        public const string Audience = "ExamAuthenticator";
        public const string key = "28216481291932476";

        public const string AuthSchemes = "Identity.Application" + "," + JwtBearerDefaults.AuthenticationScheme;
    }
}
