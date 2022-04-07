using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoard_CKT.Data;

namespace TaskBoard_CKT.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AuthController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Validate(string email, string password, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;

            var user = _db.Users.Where(u => u.Email == email).Where(u => u.Password == password)
            .Select(u => new {
                email       = u.Email,
                password    = u.Password,
                firstName   = u.FirstName,
                lastName    = u.LastName
            }).FirstOrDefault();

            if (user != null)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("email", user.email));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.email));
                claims.Add(new Claim(ClaimTypes.Name, user.firstName + ' ' + user.lastName));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return Redirect(returnUrl);
            }
            else
            {
                TempData["Error"] = "Error. Username or Password is invalid";
                return Redirect("/");
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}