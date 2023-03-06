using printecxcore__Nikola_Kosanovic__assignment.Models;

namespace printecxcore__Nikola_Kosanovic__assignment.Interfaces
{
    public interface IJokeAPIService
    {
        Task<IResponseAPI<JokeAPI>> GetJokeAPIData(HttpClient httpClient);
    }
}
