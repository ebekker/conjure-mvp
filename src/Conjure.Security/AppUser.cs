using System;
using System.Collections.Generic;
using System.Text;

namespace Conjure.Security
{
    public class AppUser
    {
        public const string AuthenticationType = "appsecurity";

        public bool IsAuthenticated { get; set; }

        public string Name { get; set; }
    }
}
