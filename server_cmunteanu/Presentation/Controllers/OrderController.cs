using Application.Commands.Orders.AddOrder;
using Application.Commands.Orders.GetAllOrders;
using Application.Commands.Products.AddProduct;
using Application.Commands.Products.GetAllProducts;
using Application.Models.Request;
using Application.Models.Response;
using Confluent.Kafka;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Presentation.Controllers
{
    [Route("api/orders/")]
    [ApiController]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;
        private readonly MonitoringService _monitoringService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public OrderController(IMediator mediator, IEmailService emailService, MonitoringService monitoringService, IHubContext<NotificationHub> hubContext)
        {
            _mediator = mediator;
            _emailService = emailService;
            _monitoringService = monitoringService;
            _hubContext =  hubContext;
        }   

        [HttpPost]
        [ProducesResponseType(200)]
        [Authorize(Roles = "User")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddOrder(OrderCreateModel orderModel)
        {
            if (User.IsInRole("Admin"))
            {
                return Unauthorized();
            }

            var command = new AddOrderCommand
            {
                OrderModel = orderModel
            };
            var order = await _mediator.Send(command);
            var response = await _emailService.SendConfirmationEmail(order);
            if(response.IsSuccessStatusCode)
            {
                await _monitoringService.MonitorEvent("An order has been sent successfully!");
                await _hubContext.Clients.All.SendAsync("ReceiveNotification", "orderAdded", "Order added successfully!", order.Id);
                Thread.Sleep(5000);
                await _hubContext.Clients.All.SendAsync("ReceiveNotification", "orderUpdate", $"Your payment for order no. {order.Id} has been processed", null);
                Thread.Sleep(10000);
                await _hubContext.Clients.All.SendAsync("ReceiveNotification", "orderUpdate", $"Your order no. {order.Id} is being processed", null);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<OrderResponseModel>), 200)]
        public async Task<IActionResult> GetAllOrders()
        {
            var command = new GetAllOrdersCommand();
            var orders = await _mediator.Send(command);
            return Ok(orders);
        }
    }
}
