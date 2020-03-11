using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CGI_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CGI_Project.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        //Wanneer de form wordt gesubmit wordt de HttpPost geactiveerd en begint hij met het uitvoeren van de functie
        [HttpPost]
        public async Task<IActionResult>Register(UserModel model)
        {
            //worden de gegevens binnen de form in de model gestopt
            var applicationUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
            };
            try
            {
                //gaat de applicatie checken of de wachtwoord voldoet aan de standaard eisen.
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                if (result.Succeeded)
                {
                    //Zodra de wachtwoord voldoet aan de eisen maakt de applicatie een account aan
                    return View(result);
                }
                else
                {
                    //anders geeft hij een errormelding met de juiste validations voor de wachtwoord
                    return View(result.Errors);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}