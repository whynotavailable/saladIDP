using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TAuth.Models;

namespace TAuth.Controllers
{
   [ApiController]
   public class WellKnownController : ControllerBase
   {
      [HttpGet("wot")]
      public List<Jwk> GetKeys()
      {
         X509Certificate2 cert = new X509Certificate2("c:\\dev\\cert.pem");
         X509Chain chain = new X509Chain();
         chain.Build(cert);
         
         List<string> certs = new List<string>();

         foreach (var c in chain.ChainElements)
         {
            certs.Add(Base64UrlEncoder.Encode(c.Certificate.GetRawCertData()));
         }
         
         var rsa = cert.GetRSAPublicKey();

         var key = new RsaSecurityKey(rsa)
         {
            KeyId = "1"
         };
         
         return new List<Jwk>()
         {
            Jwk.CreateFromKey(key, rsa.ExportParameters(false), certs, cert.Thumbprint)
         };
      }
   }
}
