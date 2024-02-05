using Application.Commands.Products.AddProduct;
using Application.Commands.Products.DeleteProduct;
using Application.Commands.Products.GetAllProducts;
using Application.Commands.Products.GetAllProductsInStock;
using Application.Commands.Products.GetProduct;
using Application.Commands.Products.UpdateProduct;
using Application.Models.Request;
using Application.Models.Response;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Presentation.Controllers
{
    [Route("api/products/")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly MonitoringService _monitoringService;
        

        public ProductController(IMediator mediator, MonitoringService monitoringService)
        {
            _mediator = mediator;
            _monitoringService = monitoringService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<ProductModel>), 200)]
        public async Task<IActionResult> GetAllProducts()
        {
            var command = new GetAllProductsCommand();
            var products = await _mediator.Send(command);
            await _monitoringService.MonitorEvent("All products fetched");
            return Ok(products);
        }

        [HttpGet("in-stock")]
        [Authorize(Policy = "AdminOrUser")]
        [ProducesResponseType(typeof(List<ProductModel>), 200)]
        public async Task<IActionResult> GetAllProductsInStock()
        {
            var command = new GetAllProductsInStockCommand();
            var products = await _mediator.Send(command);
            await _monitoringService.MonitorEvent("All products in stock fetched");
            return Ok(products);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrUser")]
        [ProducesResponseType(typeof(ProductModel), 200)]
        public async Task<IActionResult> GetProduct(int id)
        {
            var command = new GetProductByIdCommand
            {
                ProductId = id
            };
            var returnedProduct = await _mediator.Send(command);
            return Ok(returnedProduct);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddProduct(ProductCreateModel productModel)
        {
            if (User.IsInRole("User"))
            {
                return Unauthorized();
            }

            var command = new AddProductCommand
            {
                ProductModel = productModel
            };
            await _mediator.Send(command);
            await _monitoringService.MonitorEvent("A product has been added successfully!");
            return Ok(new EmptyResponse());
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateModel productModel)
        {
            var command = new UpdateProductCommand
            {
                ProductModel = productModel,
                ProductId = id
            };
            await _mediator.Send(command);
            await _monitoringService.MonitorEvent("A product has been updated successfully!");
            return Ok(new EmptyResponse());
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var command = new DeleteProductCommand
            {
                ProductId = id
            };
            await _mediator.Send(command);
            await _monitoringService.MonitorEvent("A product has been deleted successfully!");
            return Ok(new EmptyResponse());
        }
    }
}
