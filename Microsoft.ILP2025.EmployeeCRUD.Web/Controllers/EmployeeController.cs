using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ILP2025.EmployeeCRUD.Repositores;
using Microsoft.ILP2025.EmployeeCRUD.Servcies;
using Microsoft.ILP2025.EmployeeCRUD.Entities;

namespace Microsoft.ILP2025.EmployeeCRUD.Web.Controllers
{
    public class EmployeeController : Controller
    {
        public IEmployeeService employeeService { get; set; }

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        // GET: EmployeeController
        public async Task<ActionResult> Index()
        {
            var employees = await this.employeeService.GetAllEmployees();
            return View(employees);
        }

        // GET: EmployeeController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var employee = await this.employeeService.GetEmployee(id);
            return View(employee);
        }      
      
    public IActionResult Create(EmployeeEntity employee)
    {
        if (ModelState.IsValid)
        {
            employeeService.Create(employee); // adds employee to list
            return RedirectToAction("Index"); // go back to list
        }
        return View(employee); // re-display form with validation errors
    }

    // GET: Edit
     [HttpGet]
     public async Task<ActionResult> Edit(int id)
      {
         var employee = await employeeService.GetEmployee(id);
         if (employee == null){
              return NotFound();
        }
          return View(employee);
     }

// POST: Edit
    [HttpPost]
    public IActionResult Edit(EmployeeEntity employee)
    {
        if (ModelState.IsValid)
        {
            employeeService.UpdateEmployee(employee);
            return RedirectToAction("Index");
        }
        return View(employee);
    }

// POST: Confirm Delete
    //  [HttpPost, ActionName("Delete")]
     public IActionResult Delete(int id)
     {
        employeeService.DeleteEmployee(id);
         return View();
     }
    }
}
