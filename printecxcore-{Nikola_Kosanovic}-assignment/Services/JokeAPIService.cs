using Newtonsoft.Json;
using printecxcore__Nikola_Kosanovic__assignment.Interfaces;
using printecxcore__Nikola_Kosanovic__assignment.Models;
using System.Net;

namespace printecxcore__Nikola_Kosanovic__assignment.Services
{
    public class JokeAPIService : IJokeAPIService
    {
        private readonly IConfiguration _configuration;

        public JokeAPIService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task <IResponseAPI<JokeAPI>> GetJokeAPIData(HttpClient client)
        {
            //Getting the data from Joke API using httpClient and Json Deserialization. In Content I got all props from API, not just the two that need displaying
            string apiBaseUrl = _configuration.GetValue<string>("ApiURL");
            var jokeAPIResponse = new ResponseAPI<JokeAPI>
            {
                Content = new JokeAPI()
            };
            try
            {
                var apiResponse = await client.GetAsync(apiBaseUrl);
                var responseAsString = await apiResponse.Content.ReadAsStringAsync();
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.OK:
                        jokeAPIResponse.Content = JsonConvert.DeserializeObject<JokeAPI>(responseAsString);
                        jokeAPIResponse.StatusCode = HttpStatusCode.OK;
                        break;
                    case HttpStatusCode.NoContent:
                        jokeAPIResponse.StatusCode = HttpStatusCode.NoContent;
                        break;
                    case HttpStatusCode.NotFound:
                        jokeAPIResponse.Message = responseAsString;
                        jokeAPIResponse.StatusCode = HttpStatusCode.NotFound;
                        break;
                    case HttpStatusCode.BadRequest:
                        jokeAPIResponse.Message = responseAsString;
                        jokeAPIResponse.StatusCode = HttpStatusCode.BadRequest;
                        break;
                    default:
                        jokeAPIResponse.Message = "Something Went Wrong";
                        jokeAPIResponse.StatusCode = HttpStatusCode.InternalServerError; 
                        break;
                }
            }
            catch (Exception ex)
            {
                jokeAPIResponse.Message = "Something Went Wrong";
                jokeAPIResponse.StatusCode = HttpStatusCode.InternalServerError;
            }
            return jokeAPIResponse;
        }
    }
}
