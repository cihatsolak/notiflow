namespace Notiflow.Lib.Response.Models
{
    public class ResponseModel<TResponseData>
    {
        public bool Succeeded { get; init; }
        public TResponseData Data { get; init; }
        public int StatusCode { get; init; }
        public string StatusMessage { get; init; }
        public List<string> Errors { get; init; }

        public static ResponseModel<TResponseData> Success(int statusCode)
        {
            return new ResponseModel<TResponseData>
            {
                Data = default,
                StatusCode = statusCode,
                Succeeded = true
            };
        }

        public static ResponseModel<TResponseData> Success(TResponseData data)
        {
            return new ResponseModel<TResponseData>
            {
                Data = data,
                StatusCode = 9001,
                Succeeded = true
            };
        }

        public static ResponseModel<TResponseData> Success(int statusCode, TResponseData data)
        {
            return new ResponseModel<TResponseData>
            {
                Data = data,
                StatusCode = statusCode,
                Succeeded = true
            };
        }

        public static ResponseModel<TResponseData> Success(int statusCode, string statusMessage, TResponseData data)
        {
            return new ResponseModel<TResponseData>
            {
                Data = data,
                StatusCode = statusCode,
                Succeeded = true,
                StatusMessage = statusMessage
            };
        }

        public static ResponseModel<TResponseData> Fail(int statusCode)
        {
            return new ResponseModel<TResponseData>
            {
                Errors = default,
                StatusCode = statusCode,
                Succeeded = false
            };
        }

        public static ResponseModel<TResponseData> Fail(int statusCode, string error)
        {
            return new ResponseModel<TResponseData>
            {
                Errors = new List<string>() { error },
                StatusCode = statusCode,
                Succeeded = false
            };
        }

        public static ResponseModel<TResponseData> Fail(int statusCode, List<string> errors)
        {
            return new ResponseModel<TResponseData>
            {
                Errors = errors,
                StatusCode = statusCode,
                Succeeded = false
            };
        }
    }
}
