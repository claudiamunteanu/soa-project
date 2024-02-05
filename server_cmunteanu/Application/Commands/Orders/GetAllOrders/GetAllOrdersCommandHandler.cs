using Application.Interfaces;
using Application.Models.Response;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Orders.GetAllOrders
{
    public class GetAllOrdersCommandHandler : IRequestHandler<GetAllOrdersCommand, List<OrderResponseModel>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetAllOrdersCommandHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public Task<List<OrderResponseModel>> Handle(GetAllOrdersCommand request, CancellationToken cancellationToken)
        {
            var orders = _orderRepository.GetAll().Include(e => e.OrdersProducts);
            var ordersModel = _mapper.Map<List<OrderResponseModel>>(orders);
            return Task.FromResult(ordersModel);
        }
    }
}
