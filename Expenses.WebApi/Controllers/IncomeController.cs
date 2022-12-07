using Expenses.Core;
using Expenses.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class IncomeController : ControllerBase
    {
        private IIncomesServises _incomesServises;
        public IncomeController(IIncomesServises incomesServises)
        {
            _incomesServises = incomesServises;
        }

        [HttpGet]
        public IActionResult GetIncomes()
        {
            return Ok(_incomesServises.GetIncomes());
        }

        [HttpGet("{id}", Name = "GetIncome")]
        public IActionResult GetIncome(int id)
        {
            return Ok(_incomesServises.GetIncome(id));
        }

        [HttpPost]
        public IActionResult CreateIncome(DB.Income income)
        {
            var newIncome = _incomesServises.CreateIncome(income);
            return CreatedAtRoute("GetIncome", new {newIncome.Id}, newIncome);
        }

        [HttpDelete]
        public IActionResult DeleteIncome(Income income)
        {
            _incomesServises.DeleteIncome(income);
            return Ok();
        }

        [HttpPut]
        public IActionResult EditIncome(Income income)
        {
            return Ok(_incomesServises.EditIncome(income));
        }
    }
}
