using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace IdentityServer
{
    public class SigningCertificate
    {
        internal static X509Certificate2 GetSigningCertificate()
        {
            var fileName = Path.Combine("/app", "cert.pfx");
            
            if(!File.Exists(fileName)) {
                throw new FileNotFoundException("Signing Certificate is missing!");
            }
            var cert = new X509Certificate2(fileName);
            return cert;
        }
    }
}