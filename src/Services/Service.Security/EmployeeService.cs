using Dtos.AlphaBank.Security;
using Interfaces.Security;
using Mapper.Security;

namespace Service.Security {
    public class EmployeeService(IEmployeeRepository employeeRepository,
                                    IPersonRepository personRepository,
                                    IPhoneRepository phoneRepository,
                                    IUnitOfWork unitOfWork) : IEmployeeService {

        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IPersonRepository _personRepository = personRepository;
        private readonly IPhoneRepository _phoneRepository = phoneRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<List<ShowEmployeeDto>> GetAll() {
            try {

                var employeeList = await _employeeRepository.GetAllAsync();

                var showEmployeeDtoList = new List<ShowEmployeeDto>();

                foreach ( var employee in employeeList)
                    showEmployeeDtoList.Add(EmployeeMapper.MapShowEmployeeDto(employee));   

                return showEmployeeDtoList;

            } catch (Exception){
                return [];
                throw;
            }
        }

        public async Task<bool> Create(CreateEmployeeDto oCreateEmployeeDto) {

            var employee = EmployeeMapper.MapEmployee(oCreateEmployeeDto);
            var person = EmployeeMapper.MapPerson(oCreateEmployeeDto);
            var phone = EmployeeMapper.MapPhone(oCreateEmployeeDto);

            employee.Status = true;

            try {

                //await _unitOfWork.BeginTransaction();

                await _personRepository.CreateAsync(person);
                await _employeeRepository.CreateAsync(employee);
                await _phoneRepository.CreateAsync(phone);

                await _unitOfWork.CommitTransaction();
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception){
                //await _unitOfWork.RollbackAsync();
                return false;
            }
        }
    }
}