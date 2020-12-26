using CustomerApi.Infrastructure.Repository;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using CustomerApi.Infrastructure;
using CustomerApi.Infrastructure.Request;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepository customerRepo;

        public CustomerController(CustomerRepository customerRepo)
        {
            this.customerRepo = customerRepo;
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> Get()
        {
            var customers = await customerRepo.GetBy(x => true).ToListAsync();

            return Ok(customers);
        }

        [Route("GetById")]
        [HttpGet]
        public async Task<ActionResult<Customer>> Get([FromQuery]string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var customer = await customerRepo.GetByIdAsync(id);

            if (customer == null)
                return NotFound();


            return Ok(customer);
        }

        [Route("create")]
        [HttpPost]
        public async Task<ActionResult<string>> Create([FromBody]CreateCustomerRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Id))
                return NotFound("Id boş olamaz");

            await customerRepo.AddAsync(new Customer
            {
                Name = request.Name,
                Email = request.Email,
            });


            return Ok("Müşteri başarıyla eklendi");
        }

        [Route("update")]
        [HttpPut]
        public async Task<ActionResult<bool>> Update([FromBody]CreateCustomerRequest request)
        {
            var customer = await customerRepo.GetByIdAsync(request.Id);

            if (customer == null)
                return NotFound();

            customer.Email = request.Email;
            customer.Name = request.Name;

            await customerRepo.UpdateAsync(customer);

            return Ok(true);    
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete([FromBody]BaseIdRequest request)
        {
            var customer = await customerRepo.GetByIdAsync(request.Id);

            if (customer == null)
                return NotFound();

            await customerRepo.DeleteAsync(request.Id);

            return Ok(true);
        }
    }
}
