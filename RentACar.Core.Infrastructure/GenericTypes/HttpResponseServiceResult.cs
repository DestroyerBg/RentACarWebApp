using System.Net;

namespace RentACar.Core.Infrastructure.GenericTypes
{
    public class HttpResponseServiceResult<T>
    {
        public T? Data { get; set; }

        public bool IsSuccess { get; set; }

        public string? ErrorMessage { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public static HttpResponseServiceResult<T> Success(T data)
        {
            return new HttpResponseServiceResult<T>
            {
                Data = data,
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK
            };
        }

        public static HttpResponseServiceResult<T> Failure(HttpStatusCode statusCode, string errorMessage)
        {
            return new HttpResponseServiceResult<T>
            {
                ErrorMessage = errorMessage,
                StatusCode = statusCode
            };
        }
    }
}
