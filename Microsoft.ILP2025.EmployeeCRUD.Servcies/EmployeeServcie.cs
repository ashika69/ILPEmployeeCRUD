using Microsoft.ILP2025.EmployeeCRUD.Entities;
using Microsoft.ILP2025.EmployeeCRUD.Repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ILP2025.EmployeeCRUD.Servcies
{
    public class EmployeeServcie : IEmployeeService
    {
        public IEmployeeRepository employeeRepository { get; set; }

        public EmployeeServcie(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeEntity>> GetAllEmployees()
        {
            return await employeeRepository.GetAllEmployees();
        }

        public async Task<EmployeeEntity> GetEmployee(int id)
        {
            return await employeeRepository.GetEmployee(id);
        }

        public void Create(EmployeeEntity employee){
            employeeRepository.Create(employee);
        }
        public void UpdateEmployee(EmployeeEntity employee){
            employeeRepository.UpdateEmployee(employee);
        }
        public void DeleteEmployee(int id){
            employeeRepository.DeleteEmployee(id);
        }
    }
}
