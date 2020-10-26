using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;

namespace TAuth.Models
{
   public class Jwk
   {
      [JsonPropertyName("alg")]
      public string Algorithm { get; set; }
      
      [JsonPropertyName("kty")]
      public string KeyType { get; set; }
      
      [JsonPropertyName("use")]
      public string Use { get; set; }
      
      [JsonPropertyName("n")]
      public string Moduluos { get; set; }
      
      [JsonPropertyName("e")]
      public string Exponent { get; set; }
      
      [JsonPropertyName("kid")]
      public string KeyId { get; set; }
      
      [JsonPropertyName("x5t")]
      public string Thumbprint { get; set; }

      [JsonPropertyName("x5c")]
      public List<string> Certs { get; set; }

      public static Jwk CreateFromKey(RsaSecurityKey key, RSAParameters parameters, List<string> certs, string thumbprint)
      {
         return new Jwk
         {
            Algorithm = "RSA256",
            KeyType = "RSA",
            Use = "sig",
            Moduluos = parameters.Modulus == null ? null : Convert.ToBase64String(parameters.Modulus),
            Exponent = parameters.Exponent == null ? null : Convert.ToBase64String(parameters.Exponent),
            KeyId = key.KeyId,
            Certs = certs,
            Thumbprint = thumbprint
         };
      }
   }
}
