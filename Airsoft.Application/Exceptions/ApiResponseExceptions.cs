
using Airsoft.Application.DTOs.Response;
using System.Net;

namespace Airsoft.Application.Exceptions
{
    public class ApiResponseExceptions: Exception
    {
        public int StatusCode { get; }
        public ApiResponse<string> Response { get; }

        public ApiResponseExceptions(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = (int)statusCode;

            Response = new ApiResponse<string>
            {
                Success = statusCode == HttpStatusCode.OK,
                Message = message,
                Data = null // aquí siempre null
            };
        }
    }
}
