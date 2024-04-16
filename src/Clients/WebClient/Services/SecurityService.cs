using Interfaces.Security.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebClient.Services {
    public class SecurityService(IEmployeeService employeeService,
                                 IRoleService roleService) {

        private readonly IEmployeeService _employeeService = employeeService;
        private readonly IRoleService _roleService = roleService;

        public async Task<IEnumerable<SelectListItem>> GetEmployeeSelectListItems() {

            var employeeList = await _employeeService.GetAllForUser();

            return employeeList.Select(x => new SelectListItem {
                Value = x.Id.ToString(),
                Text = $"{x.Person.Name} {x.Person.FirstName} {x.Person.SecondName}"
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetRoleSelectListItems() {

            var roleList = await _roleService.GetAll();

            return roleList.Select(x => new SelectListItem {
                Value = x.Id.ToString(),
                Text = x.Description
            });
        }
    }
}