using printecxcore__Nikola_Kosanovic__assignment.Interfaces;
using System.Net;

namespace printecxcore__Nikola_Kosanovic__assignment.Services
{
    public class ResponseAPI <T>: IResponseAPI<T>
    {
        public T Content { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
