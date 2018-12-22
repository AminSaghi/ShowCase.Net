using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Util.StaticClasses
{
    public static class SecuritySettings
    {
        public static string JwtIssuer => "http://localhost:46372/";
        public static string JwtAudience => "http://localhost:46372/";
        public static string JwtSecret => "some-secret-string";
    }
}
