using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using Dtos.AlphaBank.BankAccounts;
using Interfaces.BankAccounts.Repositories;
using Interfaces.BankAccounts.Services;
using Interfaces.Common.Services;
using Mapper.BankAccounts;
using Microsoft.Extensions.Logging;

namespace Service.BankAccounts {
    public class CustomerService(IPersonService personService,
                                 ICustomerRepository customerRepository,
                                 ILogger<CustomerService> logger) : ICustomerService {

        private readonly IPersonService _personService = personService;
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly ILogger<CustomerService> _logger = logger;

        public async Task<List<ShowCustomerDTO>> GetAll() {
            try {
                //Retrieve all Customers asynchronously from the CustomerRepository.
                var customerList = await _customerRepository.GetAllAsync();

                //Initialize a list to store ShowCustomerDto objects.
                var showCustomerDtoList = new List<ShowCustomerDTO>();

                //Map each customer to a ShowCustomerDto and add it to the list.
                foreach (var customer in customerList)
                    showCustomerDtoList.Add(CustomerMapper.MapShowCustomerDTO(customer));

                // Return the list of ShowCustomerDto objects.
                return showCustomerDtoList;
            } catch {
                return [];
            }
        }

        public async Task<bool> Create(CreateCustomerDTO oCreateCustomerDTO) {
            try {
                //Get personId
                var personId = (int) oCreateCustomerDTO.Person.PersonId!;

                //The id of the person is added to the reference of phone
                oCreateCustomerDTO.Phone.PersonId = personId;

                //Map CreateCustomerDto to customer object using CustomerMapper.
                var customer = CustomerMapper.MapCustomer(oCreateCustomerDTO);

                // Set the status of the customer to true.
                customer.Status = true;

                //Customer status value is set to regular
                customer.CustomerStatusId = 1;

                //Search person by id
                var person = await _personService.GetById(personId);

                //Validate that the person is not exempt in order to create it.
                if (person == null) {
                    var result = await _personService.Create(oCreateCustomerDTO.Person, oCreateCustomerDTO.Phone);
                    if (!result) return false;
                }

                var customerByPersonId = await _customerRepository.GetByPersonIdAsync(personId);

                if (customerByPersonId != null) return false;

                _logger.LogInformation("----- Create Customer: Start the creation of an employee registry");

                await _customerRepository.CreateAsync(customer);
                await _customerRepository.SaveChangesAsync();

                _logger.LogInformation("----- Create Customer: Creation completed and saved successfully.");

                //Return true to indicate successful creation.
                return true;
            } catch (Exception e) {
                _logger.LogError($"----- Create Customer: An error occurred while creating and saving to the database. More about error: {e.Message}");

                //If there's an exception during the process, return false.
                return false;
            }
        }

        public async Task Update(int id, byte customerStatusId) {
            try {
                _logger.LogInformation("----- Create Customer: Start the creation of an employee registry");
                
                await _customerRepository.UpdateAsync(id, customerStatusId);
                await _customerRepository.SaveChangesAsync();

                _logger.LogInformation("----- Create Customer: Creation completed and saved successfully.");
            }
            catch (Exception e) {
                _logger.LogError($"----- Update Customer: An error occurred while updating and saving to the database. More about error: {e.Message}");
            }
        }

        public async Task<ShowCustomerLoanDTO?> GetByIdForLoan(int id) {
            try {
                var customer = await _customerRepository.GetByPersonIdAsync(id);

                if (customer != null) return CustomerMapper.MapShowCustomerLoan(customer);

                return null;
            }
            catch {
                return null;
            }
        }

        public async Task<ShowCustomerDTO?> GetByIdForAccount(int id) {
            try {
                var customer = await _customerRepository.GetByPersonIdAsync(id);

                if (customer != null) return CustomerMapper.MapShowCustomerDTO(customer);

                return null;
            } catch {
                return null;
            }
        }
    }
}