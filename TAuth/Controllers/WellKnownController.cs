using System;
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
      private readonly JwtHandler _handler;

      public WellKnownController(JwtHandler handler)
      {
         _handler = handler;
      }
      
      [HttpGet("wot")]
      public JwkContainer GetKeys()
      {
         return _handler.GetJwks();
      }
   }
}
