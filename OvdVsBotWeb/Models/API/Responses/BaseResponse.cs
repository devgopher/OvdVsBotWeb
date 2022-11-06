namespace OvdVsBotWeb.Models.API.Responses
{
    public class BaseResponse<TBody>
        where TBody :IResponseBody
    {
        public Guid Id { get; set; }
        public TBody Body { get; set; }
    }
}
