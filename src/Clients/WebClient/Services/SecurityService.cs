using Interfaces.Security.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace WebClient.Services {
    public class SecurityService(IEmployeeService employeeService,
                                 IRoleService roleService,
                                 ISalaryCategoryService salaryCategoryService) {

        private readonly IEmployeeService _employeeService = employeeService;
        private readonly IRoleService _roleService = roleService;
        private readonly ISalaryCategoryService _salaryCategoryService = salaryCategoryService;

        public async Task<IEnumerable<SelectListItem>> GetSalarySelectListItems() {

            var salaryList = await _salaryCategoryService.GetAll();

            return salaryList.Select(x => new SelectListItem {
                Value = x.Id.ToString(),
                Text = "₡ " + x.Description.ToString("#,0", CultureInfo.InvariantCulture)
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetEmployeeSelectListItems() {

            var employeeList = await _employeeService.GetAllForUser();
            var filteredList = employeeList.Where(x => x.Status == true);

            return filteredList.Select(x => new SelectListItem {
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