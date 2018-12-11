using System.Threading.Tasks;
using CleanArchitecture.AkkaNET.Interfaces;
using CleanArchitecture.Application.Customers.Commands.CreateCustomer;
using CleanArchitecture.Application.Customers.Commands.UpdateCustomer;
using CleanArchitecture.Application.Customers.Queries.GetCustomerDetail;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : BaseController
    {

        /// <summary>
        /// Constructor to build actors 
        /// </summary>
        /// <param name="actor"></param>
        public ValuesController(ICustomerActorProvider actor) { }

        // GET api/values
        [HttpGet]
        public object Get()
        {
            return Ok("loaded...");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var customer = await Mediator.Send(new GetCustomerDetailQuery { Id = id });
            return customer != null ? Ok(customer) : (ActionResult)BadRequest();
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody] CreateCustomerCommand command)
        {
            await Mediator.Send(command);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody] UpdateCustomerCommand command)
        {
            await Mediator.Send(command);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
