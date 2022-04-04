namespace FleetManagement.Application.DTO.response
{
    public class ExceptionResponse
    {
        public Error Error { get; set; }

        public ExceptionResponse(int errorCode, string message)
        {
            Error = new Error { ErrorCode = errorCode, Message = message };
        }
    }

    public class Error
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
    }
}
