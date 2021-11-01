namespace Service.Converter.WebApi.Domain.Repositories
{
    public interface IMassConverter
    {
        public decimal ConvertPoundToKilograms(decimal poundValue);
        public decimal ConvertKilogramToPounds(decimal kilogramValue);
    }
}
