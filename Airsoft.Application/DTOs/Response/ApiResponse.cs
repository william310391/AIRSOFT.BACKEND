using System.Net;
using System.Text.Json.Serialization;

namespace Airsoft.Application.DTOs.Response
{
    public class ApiResponse<T>
    {
        public ApiResponse() {
            Success = false;
            Message = "Ocurrio un error";
            Data = default(T);

        }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        [JsonIgnore]
        public int StatusCode => Success
            ? (int)HttpStatusCode.OK
            : (int)HttpStatusCode.InternalServerError;
    }
}
