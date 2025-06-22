using Microsoft.ILP2025.EmployeeCRUD.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microsoft.ILP2025.EmployeeCRUD.Repositores
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static List<EmployeeEntity> employeesList = new List<EmployeeEntity>();
        private readonly string _jsonFilePath = "App_Data/employees.json";

        public EmployeeRepository()
        {
            if (!Directory.Exists("App_Data"))
                Directory.CreateDirectory("App_Data");

            if (!File.Exists(_jsonFilePath))
                File.WriteAllText(_jsonFilePath, "[]");

            // Load data from JSON file at startup
            employeesList = GetEmployeeDetails();
        }

        //  Get all employees 
        public async Task<List<EmployeeEntity>> GetAllEmployees()
        {
            return await Task.FromResult(this.GetEmployees());
        }

        //  Get employee by ID
        public async Task<EmployeeEntity> GetEmployee(int id)
        {
            var employees = this.GetEmployees();
            return await Task.FromResult(employees.FirstOrDefault(e => e.Id == id));
        }

        private List<EmployeeEntity> GetEmployees()
        {
            return employeesList;
        }

        //  Create employee
        public void Create(EmployeeEntity employee)
        {
            employee.Id = employeesList.Count + 1;
            employeesList.Add(employee);
            SaveEmployees(); // 🔄 serialize to JSON
        }

        //  Update employee
        public void UpdateEmployee(EmployeeEntity employee)
        {
            var existing = employeesList.FirstOrDefault(e => e.Id == employee.Id);
            if (existing != null)
            {
                existing.Name = employee.Name;
                existing.Department = employee.Department;
                existing.Age = employee.Age;
                existing.Salary = employee.Salary;
                SaveEmployees(); // 🔄 serialize updated list
            }
        }

        //  Delete employee
        public void DeleteEmployee(int id)
        {
            var emp = employeesList.FirstOrDefault(e => e.Id == id);
            if (emp != null)
            {
                employeesList.Remove(emp);
                SaveEmployees(); // 🔄 serialize updated list
            }
        }

        //Serialization Method: Save to JSON
        private void SaveEmployees()
        {
            var json = JsonSerializer.Serialize(employeesList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_jsonFilePath, json);
        }

        // Deserialization Method: Load from JSON
        private List<EmployeeEntity> GetEmployeeDetails()
        {
            try
            {
                string json = File.ReadAllText(_jsonFilePath);
                return JsonSerializer.Deserialize<List<EmployeeEntity>>(json) ?? new List<EmployeeEntity>();
            }
            catch
            {
                return new List<EmployeeEntity>();
            }
        }
    }
}
