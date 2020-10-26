using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace TAuth.Models
{
   public abstract class Token
   {
      public string GetJwt()
      {
         var cert = new X509Certificate2("c:\\dev\\out.pfx", "test");
         var rsa = cert.GetRSAPrivateKey();

         var key = new RsaSecurityKey(rsa)
         {
            KeyId = cert.Thumbprint
         };

         var signingCredentials = new SigningCredentials(key, 
            SecurityAlgorithms.RsaSha512, SecurityAlgorithms.RsaSha512);

         var header = new JwtHeader(signingCredentials).Base64UrlEncode();

         // Need to do the memberwise clone or it gets confused and doesn't serialize the child fields.
         var body = Base64UrlEncoder.Encode(JsonSerializer.SerializeToUtf8Bytes(this.MemberwiseClone()));

         var firstParts = header + "." + body;

         var signature = JwtTokenUtilities.CreateEncodedSignature(firstParts, signingCredentials);

         return firstParts + "." + signature;
      }
   }
}
