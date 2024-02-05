using Application.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Orders.GetAllOrders
{
    public class GetAllOrdersCommand : IRequest<List<OrderResponseModel>>
    {
    }
}
