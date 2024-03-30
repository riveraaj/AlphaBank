using Data.AlphaBank;
using Interfaces.BankAccounts.Repositories;
using Interfaces.BankAccounts.Services;
using System.Collections.Generic;

namespace Service.BankAccounts
{
    public class CustomerStatusService(ICustomerStatusRepository customerStatusRepository)
                                        : ICustomerStatusService {

        private readonly ICustomerStatusRepository _customerStatusRepository
            = customerStatusRepository;

        public async Task<List<CustomerStatus>> GetAll() {
            try {
                //Retrieve all CustomerStatus asynchronously from the CustomerStatusRepository.
                return (List<CustomerStatus>) await _customerStatusRepository.GetAllAsync();
            }
            catch {
                // If there's an exception during the process, return an empty list.
                return [];
            }
        }
    }
}