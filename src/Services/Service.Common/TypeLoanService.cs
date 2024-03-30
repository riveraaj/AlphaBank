using Data.AlphaBank;
using Interfaces.Common.Repositories;
using Interfaces.Common.Services;

namespace Service.Common
{
    public class TypeLoanService (ITypeLoanRepository typeLoanRepository)
                                    : ITypeLoanService
    {
        private readonly ITypeLoanRepository _typeLoanRepository
            = typeLoanRepository;

        public async Task<List<TypeLoan>> GetAll()
        {
            try
            {
                //Retrieve all TypeLoan asynchronously from the TypeLoanRepository.
                return (List<TypeLoan>)await _typeLoanRepository.GetAllAsync();
            }
            catch
            {
                // If there's an exception during the process, return an empty list.
                return [];
            }
        }


    }
}
