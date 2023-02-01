using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        //Implementing DBContext
        private readonly EmployeeDataContext _employeeDataContext;
        public EmployeeController(EmployeeDataContext employeeDataContext)
        {
            _employeeDataContext = employeeDataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            if (_employeeDataContext.Employees == null)
            {
                return NotFound();
            }
            return await _employeeDataContext.Employees.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            if (_employeeDataContext.Employees == null)
            {
                return NotFound();
            }

            var employee = await _employeeDataContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _employeeDataContext.Employees.Add(employee);
            await _employeeDataContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new
            {
                id = employee.Id
            }, employee
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            _employeeDataContext.Entry(employee).State = EntityState.Modified;
            try
            {
                await _employeeDataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }

            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            if (_employeeDataContext.Employees == null)
            {
                return NotFound();
            }
            var employee = await _employeeDataContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _employeeDataContext.Employees.Remove(employee);
            await _employeeDataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
