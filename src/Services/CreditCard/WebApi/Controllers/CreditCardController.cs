using Application.UseCases.GetByIdCreditCard;
using Application.UseCases.GetCreditCard;
using Core.Entities;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreditCardController(IGetCrediCardUseCase getCrediCardUseCase, IGetByIdCreditCardUseCase getByIdCreditCardUseCase) : ControllerBase
{
    private readonly IGetCrediCardUseCase _getCrediCardUseCase = getCrediCardUseCase;
    private readonly IGetByIdCreditCardUseCase _getByIdCreditCardUseCase = getByIdCreditCardUseCase;
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CreditCard>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllCustomers()
    {
        try
        {
            var customersOutput = await getCrediCardUseCase.ExecuteAsync();
            return Ok(customersOutput);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CreditCard), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetbyIdCustomers(Guid id)
    {
        try
        {
            var customersOutput = await getByIdCreditCardUseCase.ExecuteAsync(id);
            return Ok(customersOutput);
        }
        catch (BadRequestException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }
}