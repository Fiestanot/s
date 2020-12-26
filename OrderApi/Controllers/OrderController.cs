using Entity;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using CustomerApi.Infrastructure.Repository;
using OrderApi.Infrastructure.Request;
using CustomerApi.Infrastructure;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository orderRepository;
        private readonly CustomerRepository customerRepository;

        public OrderController(
            OrderRepository orderRepository,
            CustomerRepository customerRepository
            )
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAll()
        {
            var orders = await orderRepository.GetBy(x => true).ToListAsync();
                
            return Ok(orders);
        }

        [Route("GetById")]
        [HttpGet]
        public async Task<ActionResult<Order>> GetById([FromQuery]string id)
        {
            var order = await orderRepository.GetByIdAsync(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [Route("GetOrdersOfCustomerById")]
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetOrdersOfCustomer([FromQuery]string id)
        {
            var customer = await customerRepository.GetByIdAsync(id);

            if (customer == null)
                return NotFound();

            var orders = await orderRepository.GetBy(x => x.CustomerId == customer.Id).OrderByDescending(x => x.CreatedAt).ToListAsync();

            return Ok(orders);
        }

        [Route("create")]
        [HttpPost]
        public async Task<ActionResult<string>> Create([FromBody]CreateOrderRequest request)
        {
            await orderRepository.AddAsync(new Order
            {
                //Authorize yok ama olsaydı böyle yapabilirdik (CurrentUserId adında prop oluştururduk her yerden erişebilecek şekilde => HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId").Value.ToString() ) 
                //CustomerId = CurrentUserId
                CustomerId =request.CustomerId,
                ImageUrl = request.ImageUrl,
                Price = request.Price,
                Quantity = request.Quantity,
                Status  = "Oluşturuldu"
            });

            return Ok("Tabrikler siparişiniz başarıyla oluşturuldu!");
        }

        [Route("update")]
        [HttpPut]
        public async Task<ActionResult<Order>> Update([FromBody]CreateOrderRequest request)
        {
            var order = await orderRepository.GetByIdAsync(request.Id);

            if (order == null)
                return NotFound();

            order.ImageUrl = request.ImageUrl;
            order.Price = request.Price;
            order.Quantity = request.Quantity;
            order.CustomerId = request.CustomerId;

            await orderRepository.UpdateAsync(order);

            return Ok(true);
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete ([FromBody]BaseIdRequest request)
        {
            var order = await orderRepository.GetByIdAsync(request.Id);
            
            if (order == null)
                return NotFound();

            await orderRepository.DeleteAsync(order.Id);

            return Ok(true);
        }

        [Route("changestatus")]
        [HttpPost]
        public async Task<ActionResult<bool>> ChangeStatus([FromBody]ChangeStatusRequest request)
        {
            var order = await orderRepository.GetByIdAsync(request.Id);

            if (order == null)
                return NotFound();

            order.Status = request.Status;

            await orderRepository.UpdateAsync(order);

            return Ok(true);
        }
    }
}
