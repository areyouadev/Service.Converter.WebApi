namespace Service.Converter.WebApi.Domain.Repositories
{
    public interface ILengthConverter
    {
        decimal ConvertMilesToKilometers(decimal mileValue);
        decimal ConvertKilometersToMiles(decimal kilometerValue);
    }
}
