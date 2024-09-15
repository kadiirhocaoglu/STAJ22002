using System.Collections.Generic;
using System.Threading.Tasks;
using HST.Business.Services.Abstract;
using HST.DTO.DTOs.CustomerAccountDtos;
using Microsoft.AspNetCore.Mvc;

namespace HST.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAccountController : ControllerBase
    {
        private readonly ICustomerAccountService customerAccountService;

        public CustomerAccountController(ICustomerAccountService customerAccountService)
        {
            this.customerAccountService = customerAccountService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerAccountDto>>> GetAllDto()
        {
            var customers = await customerAccountService.GetAllCustomerAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerAccountDto>> GetCustomerById(int id)
        {
            var customer = await customerAccountService.GetCustomerAccountByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerAccountDto>> CreateCustomerAccount(CreateCustomerAccountDto createCustomerAccountDto)
        {
            var createdCustomerAccount = await customerAccountService.CreateCustomerAccountAsync(createCustomerAccountDto);
            return CreatedAtAction(nameof(GetCustomerById), new { id = createdCustomerAccount.Id }, createdCustomerAccount);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerAccount(int id, CreateCustomerAccountDto updateCustomerAccountDto)
        {
            var result = await customerAccountService.UpdateCustomerAccountAsync(id, updateCustomerAccountDto);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAccount(int id)
        {
            var result = await customerAccountService.DeleteCustomerAccountAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
