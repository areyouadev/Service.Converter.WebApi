namespace Service.Converter.WebApi.Data.Repositories
{
    using System;
    using System.Linq;
    using Service.Converter.WebApi.Domain.Repositories;

    using Context;

    public class LengthConverter: ILengthConverter
    {
        private readonly ConverterContext _context;

        public LengthConverter(ConverterContext context)
        {
            _context = context;
        }

        public decimal ConvertMilesToKilometers(decimal mileValue)
        {
            var constant = _context.Charts.FirstOrDefault(x => x.Unit == "MilesToKilometers");

            var result = Math.Round(mileValue / constant.ConstantValue, 4);
            return result;
        }

        public decimal ConvertKilometersToMiles(decimal kilometerValue)
        {
            var constant = _context.Charts.FirstOrDefault(x => x.Unit == "MilesToKilometers");

            var result = Math.Round(kilometerValue * constant.ConstantValue, 4);
            return result;
        }
    }
}
