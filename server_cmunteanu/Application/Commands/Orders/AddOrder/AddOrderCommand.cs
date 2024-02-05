using Application.Models.Request;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Orders.AddOrder
{
    public class AddOrderCommand : IRequest<Order>
    {
        public required OrderCreateModel OrderModel { get; set; }
    }
}
