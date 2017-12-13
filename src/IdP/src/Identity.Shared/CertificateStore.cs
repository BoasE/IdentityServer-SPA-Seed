using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Identity.Shared
{
    public static class CertificateStore
    {
        public static X509Certificate2 LoadFromConfiguration(IConfigurationRoot configuration)
        {
            var pw = configuration["Identity:Certificate:Password"];
            var base64 = configuration["Identity:Certificate:Content"];
            var content = Convert.FromBase64String(base64);
            return new X509Certificate2(content, pw);
        }
    }
}
