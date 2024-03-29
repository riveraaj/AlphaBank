using Data.AlphaBank;
using Dtos.AlphaBank.Common;

namespace Mapper.Common
{
    public static class ContractMapper
    {

        public static ShowContractDto MapShowContractDto(Contract oContract)
            => new()
            {
                TypeContractDescription = oContract.TypeContract.Description,
                DateStart = oContract.DateStart,
                DateCompletion = oContract.DateCompletion,
                DateUpdate  = oContract.DateUpdate,
                PathFile = oContract.PathFile
            };

    }
}
