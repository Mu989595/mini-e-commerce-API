namespace Mini_E_Commerce_API.Controllers
{
    public class GeneralResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; internal set; }
    }
}