namespace Service.Converter.WebApi.Domain.Entities
{
    using System;
    using System.Security.AccessControl;

    public class Chart
    {
        public Guid Id { get; set; }

        public string Unit { get; set; }

        public decimal ConstantValue { get; set; }
    }
}
