namespace Service.Converter.WebApi.Domain.Repositories
{
    public interface ITemperatureConverter
    {
        decimal ConvertFahrenheitToCelsius(decimal fahrenheitValue); 
        decimal ConvertCelsiusToFahrenheit(decimal celsiusValue);
    }
}
