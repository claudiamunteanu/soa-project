using Application.Interfaces;
using Application.Repositories.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Orders.AddOrder
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AddOrderCommandHandler(IOrderRepository orderRepository, IOrderProductRepository orderProductRepository, IMapper mapper, IUserRepository userRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task<Order> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var orderModel = request.OrderModel;
            var user = await _userRepository.GetByEmail(orderModel.PlacedByEmail);

            var order = _mapper.Map<Order>(orderModel);
            order.PlacedById = user.Id;
            await _orderRepository.Save(order);
            await _orderRepository.SaveChanges();

            foreach(var p in orderModel.OrderedProducts)
            {
                var product = await _productRepository.GetById(p.ProductId);
                product.Stock -= p.Quantity;
                await _productRepository.Update(product);
            }
            await _productRepository.SaveChanges();

            var orderProducts = _mapper.Map<List<OrderProduct>>(orderModel.OrderedProducts);
            foreach(var p in orderProducts)
            {
                p.OrderId = order.Id;
                await _orderProductRepository.Save(p);
            }
            await _orderProductRepository.SaveChanges();

            return await _orderRepository.GetByIdWithUser(order.Id);
        }
    }
}
