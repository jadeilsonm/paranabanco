using Application.UseCases;
using Application.UseCases.GetByIdProposal;
using Core.Entities;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreditPoposalController(
    ILogger<CreditPoposalController> logger,
    IGetAllProposalUseCase getAllProposalUseCase,
    IGetByIdProposalUseCase getByIdProposalUseCase)
    : ControllerBase
{   
    private readonly ILogger<CreditPoposalController> _logger = logger;
    private IGetAllProposalUseCase _getAllProposalUseCase = getAllProposalUseCase;
    private IGetByIdProposalUseCase _getByIdProposalUseCase = getByIdProposalUseCase;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CreditProposal>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllCustomers()
    {
        try
        {
            var customersOutput = await _getAllProposalUseCase.ExecuteAsync();
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
    [ProducesResponseType(typeof(CreditProposal), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetbyIdCustomers(Guid id)
    {
        try
        {
            var customersOutput = await _getByIdProposalUseCase.ExecuteAsync(id);
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