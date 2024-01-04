using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Sum(string firstNumber, string secondNumber)
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

        [HttpGet("multiplication/{firstNumber}/{secondNumber}")]
        public IActionResult Multiplication(string firstNumber, string secondNumber)
        {
            double referenciaVazia = 0;

            if (Double.TryParse(firstNumber, out referenciaVazia)
                    && Double.TryParse(secondNumber, out referenciaVazia)
                )
            {
                var sum = Convert.ToDecimal(firstNumber) * Convert.ToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

            return BadRequest("Input Invalid");
        }


        [HttpGet("subtraction/{firstNumber}/{secondNumber}")]
        public IActionResult Subtraction(string firstNumber, string secondNumber)
        {
            double referenciaVazia = 0;

            if (Double.TryParse(firstNumber, out referenciaVazia)
                    && Double.TryParse(secondNumber, out referenciaVazia)
                )
            {
                var sum = Convert.ToDecimal(firstNumber) - Convert.ToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

            return BadRequest("Input Invalid");
        }

        [HttpGet("Mean/{firstNumber}/{secondNumber}")]
        public IActionResult Mean(string firstNumber, string secondNumber)
        {
            double referenciaVazia = 0;

            if (Double.TryParse(firstNumber, out referenciaVazia)
                    && Double.TryParse(secondNumber, out referenciaVazia)
                )
            {
                var sum = (Convert.ToDecimal(firstNumber) + Convert.ToDecimal(secondNumber)) / 2;
                return Ok(sum.ToString());
            }

            return BadRequest("Input Invalid");
        }
    }
}