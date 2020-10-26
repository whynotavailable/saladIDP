using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TAuth.Models
{
   public class IdToken : Token
   {
      [JsonPropertyName("sub")]
      public string Subject { get; set; }
      
      [JsonPropertyName("name")]
      public string Name { get; set; }
      
      [JsonPropertyName("iss")]
      public string Issuer { get; set; }
      
      [JsonPropertyName("aud")]
      public string Audience { get; set; }
      
      [JsonPropertyName("exp")]
      public long Expiration { get; set; }
      
      [JsonExtensionData]
      public Dictionary<string, JsonElement> Extras { get; set; }
   }
}
