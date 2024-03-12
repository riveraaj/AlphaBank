using Dtos.AlphaBank.Security;
using Interfaces.Security;
using Mapper.Security;
using Microsoft.Extensions.Logging;

namespace Service.Security {
    public class EmployeeService(IEmployeeRepository employeeRepository,
                                    IPersonRepository personRepository,
                                    IPhoneRepository phoneRepository,
                                    IUnitOfWork unitOfWork,
                                    ILogger<EmployeeService> logger) : IEmployeeService {

        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IPersonRepository _personRepository = personRepository;
        private readonly IPhoneRepository _phoneRepository = phoneRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<EmployeeService> _logger = logger;

        public async Task<List<ShowEmployeeDto>> GetAll() {
            try {
                //Retrieve all employees asynchronously from the EmployeeRepository.
                var employeeList = await _employeeRepository.GetAllAsync();

                //Initialize a list to store ShowEmployeeDto objects.
                var showEmployeeDtoList = new List<ShowEmployeeDto>();

                //Map each employee to a ShowEmployeeDto and add it to the list.
                foreach ( var employee in employeeList)
                    showEmployeeDtoList.Add(EmployeeMapper.MapShowEmployeeDto(employee));

                // Return the list of ShowEmployeeDto objects.
                return showEmployeeDtoList;

            } catch (Exception){
                // If there's an exception during the process, return an empty list.
                return [];
            }
        }

        public async Task<bool> Create(CreateEmployeeDto oCreateEmployeeDto) {

            // Map CreateEmployeeDto to employee, person, and phone objects using EmployeeMapper.
            var employee = EmployeeMapper.MapEmployee(oCreateEmployeeDto);
            var person = EmployeeMapper.MapPerson(oCreateEmployeeDto);
            var phone = EmployeeMapper.MapPhone(oCreateEmployeeDto);

            // Set the status of the employee to true.
            employee.Status = true;

            try {

                _logger.LogInformation("---- Start the transaction to create and save in the database an employee, person and phone number.");

                //Begin a transaction using the unit of work.
                await _unitOfWork.BeginTransaction();

                //Create records in the PersonRepository, EmployeeRepository, and PhoneRepository.
                await _personRepository.CreateAsync(person);
                await _employeeRepository.CreateAsync(employee);
                await _phoneRepository.CreateAsync(phone);

                //Commit the transaction and save changes.
                await _unitOfWork.CommitTransaction();
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("---- Correctly completes the transaction.");

                // Return true to indicate successful creation.
                return true;
            }
            catch (Exception e){

                _logger.LogError($"--- An error occurred while creating and saving to the database. More about error: {e.Message}");

                //If there's an exception during the process, rollback the transaction and return false.
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }
    }
}