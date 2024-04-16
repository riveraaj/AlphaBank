using Data.AlphaBank;
using Dtos.AlphaBank.Security;
using Interfaces.Common.Services;
using Interfaces.Security.Repositories;
using Interfaces.Security.Services;
using Mapper.Common;
using Mapper.Security;
using Microsoft.Extensions.Logging;

namespace Service.Security {
    public class EmployeeService(IEmployeeRepository employeeRepository,
                                    IPersonService personService,
                                    ILogger<EmployeeService> logger) : IEmployeeService {

        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IPersonService _personService = personService;
        private readonly ILogger<EmployeeService> _logger = logger;

        public async Task<UpdateEmployeeDTO?> GetByIdForUpdate(int id) {
            try {
                var employee = await _employeeRepository.GetByIdAsync(id);

                if (employee != null) return EmployeeMapper.MapUpdateEmployeeDTOForShow(employee);

                return null;
            } catch {
                return null;
            }
        }

        public async Task<List<Employee>> GetAllForUser() {
            try {
                //Retrieve all employees asynchronously from the EmployeeRepository.
                var employeeList = await _employeeRepository.GetAllAsync();

                var filteredList = employeeList.Where(x => x.Users.FirstOrDefault() == null).ToList();

                // Return the list of employee objects.
                return filteredList;
            } catch {
                // If there's an exception during the process, return an empty list.
                return [];
            }
        }

        public async Task<List<ShowEmployeeDTO>> GetAll() {
            try {
                //Retrieve all employees asynchronously from the EmployeeRepository.
                var employeeList = await _employeeRepository.GetAllAsync();

                //Initialize a list to store ShowEmployeeDto objects.
                var showEmployeeDtoList = new List<ShowEmployeeDTO>();

                //Map each employee to a ShowEmployeeDto and add it to the list.
                foreach ( var employee in employeeList)
                    showEmployeeDtoList.Add(EmployeeMapper.MapShowEmployeeDTO(employee));

                // Return the list of ShowEmployeeDto objects.
                return showEmployeeDtoList;

            } catch (Exception){
                // If there's an exception during the process, return an empty list.
                return [];
            }
        }

        public async Task<bool> Create(CreateEmployeeDTO oCreateEmployeeDTO) {
            try {
                //Get personId
                var personId = (int) oCreateEmployeeDTO.Person.PersonId!;

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

                _logger.LogInformation("----- Create Employee: Creation completed and saved successfully.");

                //Return true to indicate successful creation.
                return true;
            } catch {
                _logger.LogError("----- Create Employee: An error occurred while creating and saving to the database.");

                //If there's an exception during the process, return false.
                return false;
            }    
        }

        public async Task<bool> Update(UpdateEmployeeDTO oUpdateEmployeeDTO) {
            try {
                var employee = EmployeeMapper.MapUpdateEmployee(oUpdateEmployeeDTO);
                var phone = PhoneMapper.MapPhoneForEmployee(oUpdateEmployeeDTO);

                if (employee == null || phone == null) return false;

                var result = await _personService.CreatePhone(phone);

                if(!result) return false;

                _logger.LogInformation("----- Update Employee: Start the creation of an employee registry");

                await _employeeRepository.UpdateAsync(employee);
                await _employeeRepository.SaveChangesAsync();

                _logger.LogInformation("----- Update Employee: Creation completed and saved successfully.");

                //Return true to indicate successful creation.
                return true;
            }
            catch (Exception){
                _logger.LogError("----- Update Employee: An error occurred while creating and saving to the database.");

                //If there's an exception during the process, return false.
                return false;
            }
        }

        public async Task<bool> Remove(int id) {
            try {

                _logger.LogInformation("----- Delete Employee: Start the creation of an employee registry");

                await _employeeRepository.RemoveAsync(id);
                await _employeeRepository.SaveChangesAsync();

                _logger.LogInformation("----- Delete Employee: Creation completed and saved successfully.");

                //Return true to indicate successful creation.
                return true;
            }
            catch (Exception) {
                _logger.LogError("----- Delete Employee: An error occurred while creating and saving to the database.");

                //If there's an exception during the process, return false.
                return false;
            }
        }
    }
}