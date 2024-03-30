using Dtos.AlphaBank.Security;
using Interfaces.Common.Services;
using Interfaces.Security.Repositories;
using Interfaces.Security.Services;
using Mapper.Security;
using Microsoft.Extensions.Logging;

namespace Service.Security {
    public class EmployeeService(IEmployeeRepository employeeRepository,
                                    IUserService userService,
                                    IPersonService personService,
                                    ILogger<EmployeeService> logger) : IEmployeeService {

        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IPersonService _personService = personService;
        private readonly IUserService _userService = userService;
        private readonly ILogger<EmployeeService> _logger = logger;

        public async Task<List<ShowEmployeeDTO>> GetAll() {
            try {
                //Retrieve all employees asynchronously from the EmployeeRepository.
                var employeeList = await _employeeRepository.GetAllAsync();

                //Initialize a list to store ShowEmployeeDTO objects.
                var showEmployeeDtoList = new List<ShowEmployeeDTO>();

                //Map each employee to a ShowEmployeeDTO and add it to the list.
                foreach ( var employee in employeeList)
                    showEmployeeDtoList.Add(EmployeeMapper.MapShowEmployeeDTO(employee));

                // Return the list of ShowEmployeeDTO objects.
                return showEmployeeDtoList;

            } catch (Exception){
                // If there's an exception during the process, return an empty list.
                return [];
            }
        }

        public async Task<bool> Create(CreateEmployeeDTO oCreateEmployeeDTO) {
            try {
                //Get personId
                var personId = (int)oCreateEmployeeDTO.Person.PersonId!;

                //The id of the person is added to the reference of; phone
                oCreateEmployeeDTO.Phone.PersonId = personId;

                // Map CreateEmployeeDto to employee
                var employee = EmployeeMapper.MapEmployee(oCreateEmployeeDTO);

                // Set the status of the employee to true.
                employee.Status = true;

                //Search person by id
                var person = await _personService.GetById(personId);

                //Validate that the person is not exempt in order to create it.
                if (person == null)  {
                    var result = await _personService.Create(oCreateEmployeeDTO.Person, oCreateEmployeeDTO.Phone);
                    if (!result) return false;       
                }

                _logger.LogInformation("----- Create Employee: Start the creation of an employee registry");

                await _employeeRepository.CreateAsync(employee);
                await _employeeRepository.SaveChangesAsync();

                //Perform User Setup to start the user creation afterwards
                var userSetupResult = await _userService.UserSetup(oCreateEmployeeDTO, _employeeRepository);

                if (!userSetupResult) {
                    _logger.LogError("----- Create Employee: An error occurred during user setup.");

                    //Delete the previously created employee record
                    var lastEmployee = await _employeeRepository.GetLastEmployeeAsync();
                    if (lastEmployee != null)
                        await _employeeRepository.RemoveAsync(lastEmployee.Id);            

                    //If there's an exception during the process, return false.
                    return false;
                }

                _logger.LogInformation("----- Create Employee: Creation completed and saved successfully.");

                //Return true to indicate successful creation.
                return true;
            } catch (Exception e) {
                _logger.LogError($"----- Create Employee: An error occurred while creating and saving to the database. More about error: {e.Message}");

                //If there's an exception during the process, return false.
                return false;
            }    
        }
    }
}