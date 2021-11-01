namespace Service.Converter.WebApi.Data.Repositories
{
    using System;
    using System.Linq;
    using Service.Converter.WebApi.Domain.Repositories;

    using Context;
    public class TemperatureConverter : ITemperatureConverter
    {
        private readonly ConverterContext _context;

        public TemperatureConverter(ConverterContext context)
        {
            _context = context;
        }

        public decimal ConvertFahrenheitToCelsius(decimal fahrenheitValue)
        {
            var constant = _context.Charts.FirstOrDefault(x => x.Unit == "FahrenheitToCelsius");

            var result = Math.Round((fahrenheitValue - constant.ConstantValue) * 5 / 9, 4);
            return result;
        }

        public decimal ConvertCelsiusToFahrenheit(decimal celsiusValue)
        {
            var constant = _context.Charts.FirstOrDefault(x => x.Unit == "FahrenheitToCelsius");

            var result = Math.Round((celsiusValue * 9 / 5) + constant.ConstantValue, 4);
            return result;
        }
    }
}
