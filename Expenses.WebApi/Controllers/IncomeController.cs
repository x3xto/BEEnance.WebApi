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
    }
}
