using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Customers.Commands.CreateCustomer;
using CleanArchitecture.Application.Customers.Commands.DeleteCustomer;
using CleanArchitecture.Application.Customers.Commands.UpdateCustomer;
using CleanArchitecture.Application.Customers.Queries.GetCustomerDetail;
using CleanArchitecture.Application.Customers.Queries.GetCustomersList;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : BaseController
    {
        // GET api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await Mediator.Send(new GetCustomersListQuery()));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var customer = await Mediator.Send(new GetCustomerDetailQuery { Id = id });
                return customer != null ? Ok(customer) : (ActionResult)NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateCustomerCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateCustomerCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteCustomerCommand { Id = id });
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
