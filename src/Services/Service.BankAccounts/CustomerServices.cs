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

                var customerByPersonId = await _customerRepository.GetByPersonIdAsync(personId);
                if (customerByPersonId != null) return false;

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

        public async Task<bool> Update(UpdateCustomerDTO oUpdateCustomerDTO)
        {
            // Map UpdateCustomerDTO to a customer object using some mapper (CustomerMapper)
            var customer = CustomerMapper.MapUpdateCustomer(oUpdateCustomerDTO);

            try
            {

                _logger.LogInformation("----- Update Customer: Start updating and saving the customer in the database");

                // Attempt to update a customer through the CustomerRepository and save changes asynchronously.
                await _customerRepository.UpdateAsync(customer);

                await _customerRepository.SaveChangesAsync();

                _logger.LogInformation("----- Update Customer: Successfully completes the process");

                // Return true to indicate successful update.
                return true;
            }
            catch
            {
                _logger.LogError("----- Update Customer: An error occurred while updating and saving to the database.");
                //If there's an exception during the process, return false.
                return false;

            }
        }

        public async Task UpdateStatus(int id, byte customerStatusId)
        {
            try
            {
                _logger.LogInformation("----- Update Customer Status: Start updating and saving the customer in the database");

                await _customerRepository.UpdateStatusAsync(id, customerStatusId);
                await _customerRepository.SaveChangesAsync();

                _logger.LogInformation("----- Update Customer Status: Successfully completes the process");
            }
            catch (Exception e)
            {
                _logger.LogError($"----- Update Customer Status: An error occurred while updating and saving to the database. More about error: {e.Message}");
            }
        }

        public async Task<bool> Remove(int id)
        {
            try
            {
                _logger.LogInformation("----- Remove Customer: Start removing a customer and saving the changes in the database");

                // Attempt to remove a customer through the CustomerRepository and save changes asynchronously.
                await _customerRepository.RemoveAsync(id);

                await _customerRepository.SaveChangesAsync();

                _logger.LogInformation("----- Remove Customer: Successfully completes the process");

                // Return true to indicate successful update.
                return true;
            }
            catch
            {
                _logger.LogError("----- Remove Customer: An error occurred while removing a customer and saving the changes in the database.");
                //If there's an exception during the process, return false.
                return false;

            }
        }
    }
}