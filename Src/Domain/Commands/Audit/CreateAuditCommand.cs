namespace Service.Converter.WebApi.Domain.Commands.Audit
{
    using Contracts;

    public class CreateAuditCommand : ICommand
    {
        public CreateAuditCommand() { }
        public CreateAuditCommand(string userName, string requestUrl, string requestData, string responseData)
        {
            UserName = userName;
            RequestUrl = requestUrl;
            RequestData = requestData;
            ResponseData = responseData;
        }

        public string UserName { get; set; }
        public string RequestUrl { get; set; }
        public string RequestData{ get; set; }
        public string ResponseData { get; set; }
    }
}
