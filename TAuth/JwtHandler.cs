using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using TAuth.Models;

namespace TAuth
{
   public class JwtHandler
   {
      private readonly IMemoryCache _memoryCache;
      private readonly List<string> _certs = new List<string>();
      private readonly ConcurrentDictionary<string, byte[]> _certData = new ConcurrentDictionary<string, byte[]>();
      private readonly ConcurrentDictionary<string, string> _certPassword = new ConcurrentDictionary<string, string>();
      
      public JwtHandler(IMemoryCache memoryCache)
      {
         _memoryCache = memoryCache;
      }
      
      public void AddCert(string location)
      {
         AddCert(location, null);
      }

      // ReSharper disable once MemberCanBePrivate.Global
      public void AddCert(string location, string password)
      {
         _certs.Add(location);
         _certData.AddOrUpdate(location, File.ReadAllBytes(location), (s, bytes) => File.ReadAllBytes(location));
         
         if (password != null)
         {
            _certPassword.TryAdd(location, password);
         }
      }

      // TODO: cache this whole thing for 5 minutes
      public JwkContainer GetJwks()
      {
         return _memoryCache.GetOrCreate("jwks", entry =>
         {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            
            return new JwkContainer
            {
               Keys = _certs.Select(certLocation =>
               {
                  var cert = new X509Certificate2(_certData.GetValueOrDefault(certLocation) ?? new byte[] { }, _certPassword.GetValueOrDefault(certLocation));
                  var chain = new X509Chain();
                  chain.Build(cert);

                  var certs = new List<string>();

                  foreach (var c in chain.ChainElements)
                  {
                     certs.Add(Convert.ToBase64String(c.Certificate.GetRawCertData()));
                  }

                  var rsa = cert.GetRSAPublicKey();

                  var key = new RsaSecurityKey(rsa) {KeyId = cert.Thumbprint};

                  return Jwk.CreateFromKey(key, rsa.ExportParameters(false), certs, cert.Thumbprint);
               }).ToList()
            };
         });
      }

      public string GetJwt<T>(T claimsObject)
      {
         var certLocation = _certs.Last();
         var cert = new X509Certificate2(_certData.GetValueOrDefault(certLocation) ?? new byte[] {}, _certPassword.GetValueOrDefault(certLocation));
         
         var rsa = cert.GetRSAPrivateKey();

         var key = new RsaSecurityKey(rsa)
         {
            KeyId = cert.Thumbprint
         };

         var signingCredentials = new SigningCredentials(key, 
            SecurityAlgorithms.RsaSha512, SecurityAlgorithms.RsaSha512);

         var header = new JwtHeader(signingCredentials).Base64UrlEncode();

         var body = Base64UrlEncoder.Encode(JsonSerializer.SerializeToUtf8Bytes(claimsObject));

         var firstParts = header + "." + body;

         var signature = JwtTokenUtilities.CreateEncodedSignature(firstParts, signingCredentials);

         return firstParts + "." + signature;
      }
   }
}
