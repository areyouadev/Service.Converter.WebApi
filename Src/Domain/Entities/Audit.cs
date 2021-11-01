namespace Service.Converter.WebApi.Domain.Entities
{
    using System;

    public class Audit
    {
        public Guid Id { get; set; }

        public  DateTimeOffset Created { get; set; }

        public string UserName { get; set; }

        public string RequestUrl { get; set; }

        public string RequestData { get; set; }

        public string ResponseData { get; set; }
    }
}
