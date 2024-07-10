using Aplication.DTOs;
using Aplication.UseCases.Create;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController(ICreateCustomer createCustomer) : ControllerBase
{
    
    private readonly ICreateCustomer _createCustomer = createCustomer ?? throw new ArgumentNullException(nameof(createCustomer));

    [HttpPost]
    [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCustomer(CustomerRequest customer)
    {
        try
        {
            var response = await _createCustomer.ExecuteAsync(customer);
            return CreatedAtAction(nameof(CreateCustomer), null, response);
        }
        catch (DataContractValidationException e)
        {
            return BadRequest(new { message = e.Message, errors = e.ValidationErrors });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
        
    }
}