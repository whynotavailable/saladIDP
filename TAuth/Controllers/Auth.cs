﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TAuth.Models;

namespace TAuth.Controllers
{
   [Controller]
   [Route("authorize")]
   public class AuthController : Controller
   {
      // GET
      public IActionResult Index()
      {
         var token = new IdToken()
         {
            Issuer = "https://t-auth",
            Audience = "arn:lol",
            Expiration = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds(),
            Subject = "yolo",
            Extras = new Dictionary<string, JsonElement>()
            {
               { "https://asd", JsonSerializer.Deserialize<JsonElement>("[1]") }
            }
         };

         Console.WriteLine(token.GetJwt());
         return View("~/Pages/Auth/Index.cshtml");
      }
   }
}
