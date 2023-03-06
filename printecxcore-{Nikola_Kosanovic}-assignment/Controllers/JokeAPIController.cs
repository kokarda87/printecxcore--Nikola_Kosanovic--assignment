using Microsoft.AspNetCore.Mvc;
using System.Net;
using printecxcore__Nikola_Kosanovic__assignment.Interfaces;
using printecxcore__Nikola_Kosanovic__assignment.Services;
using printecxcore__Nikola_Kosanovic__assignment.Models;

namespace printecxcore__Nikola_Kosanovic__assignment.Controllers
{
    public class JokeAPIController : Controller
    {
        //Inject API Service and config Interface so I can get what i need
        private readonly IJokeAPIService _jokeAPIService;
        private readonly IConfiguration _configuration;

        public JokeAPIController(IJokeAPIService jokeAPIService, IConfiguration configuration) 
        {
            _jokeAPIService = jokeAPIService;
            _configuration = configuration;
        }
        public async Task <IActionResult> JokeAPI(UtilityTemplate model)
        {
            //If amount values are out of bounds that are defined in appsettings, redirect to a new view and display messages
            var minLimit = decimal.Parse(_configuration.GetValue<string>("MinAllowedAmount"));
            var maxLimit = decimal.Parse(_configuration.GetValue<string>("MaxAllowedAmount"));

            if (model.Amount < minLimit )
            {
                ViewBag.Message = "Do not be so cheap, you have enough money to overcome your minimal limit of " + minLimit + "!!!";
                return View("~/Views/JokeAPI/AmountErrorView.cshtml");
            }

            if (model.Amount > maxLimit)
            {
                ViewBag.Message = "Take it easy dude, you have overreached your maximum limit of " + maxLimit + "!!!";
                return View("~/Views/JokeAPI/AmountErrorView.cshtml");
            }

            //Call service that gets the API data
            var client = new HttpClient();
            var response = await _jokeAPIService.GetJokeAPIData(client);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return View(response.Content);
                default:
                    return Redirect("~Error.cshtml");
            }

            
        }
    }
}
