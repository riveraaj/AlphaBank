using Interfaces.GrantingLoans.Services;

namespace Service.GrantingLoans
{
    public class GrantingLoansService : IGrantingLoansService
    {

        public async Task<bool> GrantingLoan(int idLoanApplication)
        {
			try
			{

				return true;
			}
			catch (Exception)
			{

				return false;
			}
        }

    }
}
