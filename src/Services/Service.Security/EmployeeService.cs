using Dtos.AlphaBank.Security;
using Interfaces.Security;
using Mapper.Common;
using Mapper.Security;
using Microsoft.Extensions.Logging;

namespace Service.Security {
    public class EmployeeService(IEmployeeRepository employeeRepository,
                                    IPersonRepository personRepository,
                                    IPhoneRepository phoneRepository,
                                    IUnitOfWork unitOfWork,
                                    IUserService userService,
                                    ILogger<EmployeeService> logger) : IEmployeeService {

        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IPersonRepository _personRepository = personRepository;
        private readonly IPhoneRepository _phoneRepository = phoneRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IUserService _userService = userService;
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

            oCreateEmployeeDto.Phone.PersonId = (int)oCreateEmployeeDto
                                                            .Person.PersonId!;

            // Map CreateEmployeeDto to employee, person, and phone objects using EmployeeMapper.
            var employee = EmployeeMapper.MapEmployee(oCreateEmployeeDto);
            var person = PersonMapper.MapPerson(oCreateEmployeeDto.Person);
            var phone = PhoneMapper.MapPhone(oCreateEmployeeDto.Phone);

            // Set the status of the employee to true.
            employee.Status = true;

            // Set the Deceased of the person to false.
            person.Deceased = false;

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

                _logger.LogInformation("---- Correctly completes the transaction.");

                var result = await CreateUser(oCreateEmployeeDto);

                if (!result) return false;


                // Return true to indicate successful creation.
                return true;
            }
            catch (Exception e){

                _logger.LogError($"--- An error occurred while creating and saving to the database. More about error: {e.Message}");

                //If there's an exception during the process, rollback the transaction and return false.
                return false;
            }
        }

        private async Task<bool> CreateUser(CreateEmployeeDto oCreateEmployeeDto) {

            var employeeList = await _employeeRepository.GetAllAsync();
            var employeeId = employeeList.Last().Id;

            oCreateEmployeeDto.User.EmployeeId = employeeId;

            var result = await _userService.Create(oCreateEmployeeDto.User);

            if (!result) return false;
            return true;
        }
    }
}