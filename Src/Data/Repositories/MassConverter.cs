namespace Service.Converter.WebApi.Data.Repositories
{
    using System;
    using System.Linq;

    using Context;
    using Service.Converter.WebApi.Domain.Repositories;
 
    public class MassConverter: IMassConverter
    {
        private readonly ConverterContext _context;

        public MassConverter(ConverterContext context)
        {
            _context = context;
        }

        public decimal ConvertPoundToKilograms(decimal poundValue)
        {
            var constant = _context.Charts.FirstOrDefault(x => x.Unit == "PoundToKilograms");

            var result = Math.Round(poundValue / constant.ConstantValue, 2);
            return result;
        }

        public decimal ConvertKilogramToPounds(decimal kilogramValue)
        {
            var constant = _context.Charts.FirstOrDefault(x => x.Unit == "PoundToKilograms");

            var result = Math.Round(kilogramValue * constant.ConstantValue, 2);
            return result;
        }
    }
}
