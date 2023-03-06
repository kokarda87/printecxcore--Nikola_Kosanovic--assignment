using System.Net;

namespace printecxcore__Nikola_Kosanovic__assignment.Interfaces
{
    public interface IResponseAPI<T>
    {
        T Content { get; set; }
        HttpStatusCode StatusCode { get; set; }
        string Message { get; set; }
    }
}
