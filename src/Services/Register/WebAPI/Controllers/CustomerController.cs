using Aplication.DTOs;
using Aplication.UseCases.Create;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    
    private readonly ICreateCustomer _createCustomer;

    public CustomerController(ICreateCustomer createCustomer)
    {
        _createCustomer = createCustomer ?? throw new ArgumentNullException(nameof(createCustomer));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCustomer(CustomerRequest customer)
    {
        var response = await _createCustomer.ExecuteAsync(customer);
        return CreatedAtAction(nameof(CreateCustomer), null, response);
    }
}