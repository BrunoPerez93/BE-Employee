using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using React_ASPNet.Models;
using Microsoft.EntityFrameworkCore;

namespace React_ASP.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ReactAspNetContext dbContext;

        public EmployeeController(ReactAspNetContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("List Employees")]
        public async Task<IActionResult> GetEmployee()
        {
            var listEmployee = await dbContext.Employees.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, listEmployee);
        }

        [HttpGet]
        [Route("Obtain/{id:int}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            return StatusCode(StatusCodes.Status200OK, employee);
        }

        [HttpPost]
        [Route("New")]
        public async Task<IActionResult> NewEmployee([FromBody] Employee employee)
        {
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> EditEmployee([FromBody] Employee employee)
        {
            dbContext.Employees.Update(employee);
            await dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> DeleteEmployeeById(int id)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            dbContext.Employees.Remove(employee);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
        }
    }
}
