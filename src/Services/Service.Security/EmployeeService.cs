using Dtos.AlphaBank.Security;
using Interfaces.Security;
using Mapper.Security;

namespace Service.Security {
    public class EmployeeService(IEmployeeRepository employeeRepository,
                                    IPersonRepository personRepository,
                                    IUnitOfWork unitOfWork) {

        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IPersonRepository _personRepository = personRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> Create(CreateEmployeeDto oCreateEmployeeDto) {

            var employee = EmployeeMapper.MapEmployee(oCreateEmployeeDto);
            var person = EmployeeMapper.MapPerson(oCreateEmployeeDto);

            try {

                await _unitOfWork.BeginTransaction();

                await _employeeRepository.CreateAsync(employee);
                await _personRepository.CreateAsync(person);

                await _unitOfWork.CommitTransaction();

                return true;
            }
            catch (Exception){
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }
    }
}