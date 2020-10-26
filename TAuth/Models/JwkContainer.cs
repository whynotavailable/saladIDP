using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TAuth.Models
{
   public class JwkContainer
   {
      [JsonPropertyName("keys")]
      public List<Jwk> Keys { get; set; }
   }
}
