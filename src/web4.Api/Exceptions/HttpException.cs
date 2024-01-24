namespace Events.Api.Exceptions
{
    public class HttpException : Exception
    {
        public int StatusCode { get; set; }
        public object Errors { get; set; }
    }
}
