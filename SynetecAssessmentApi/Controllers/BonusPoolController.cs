using Microsoft.AspNetCore.Mvc;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Services;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    [Route("api/[controller]")]
    public class BonusPoolController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bonusPoolService = new BonusPoolService();

            return Ok(await bonusPoolService.GetEmployeesAsync());
        }

        [HttpPost()]
        public async Task<IActionResult> CalculateBonus([FromBody] CalculateBonusDto request)
        {
            //if user not pass the employee id and bonus percentage return not found  - by suresh
            if (!ModelState.IsValid)
                return BadRequest("Please pass totalBonusPoolAmount and SelectedEmployeeId.");

            //if (string.IsNullOrEmpty(request.SelectedEmployeeId.ToString()))
            //    return BadRequest("Please pass SelectedEmployeeId.");

            //if (string.IsNullOrEmpty(request.TotalBonusPoolAmount.ToString()))
            //    return BadRequest("Please pass TotalBonusPoolAmount.");

            var bonusPoolService = new BonusPoolService();
            BonusPoolCalculatorResultDto bpcrDto = null;
            bpcrDto = await bonusPoolService.CalculateAsync(request.TotalBonusPoolAmount, request.SelectedEmployeeId);

            //if empoyee not found it will retunr not found  - by suresh
            if (bpcrDto == null)
                return BadRequest("Empoyee details not found.");

            return Ok(bpcrDto);
        }
    }
}
