using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;

namespace RestASPNET.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {


        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Get(string firstNumber, string secondNumber)
        {
            double referenciaVazia = 0;

            if (Double.TryParse(firstNumber, out referenciaVazia) 
                    && Double.TryParse(secondNumber, out referenciaVazia)
                )
            {
                var sum = Convert.ToDecimal(firstNumber) + Convert.ToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

            return BadRequest("Input Invalid");
        }
    }
}