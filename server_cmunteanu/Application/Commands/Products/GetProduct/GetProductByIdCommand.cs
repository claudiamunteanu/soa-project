using Application.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Products.GetProduct
{
    public class GetProductByIdCommand : IRequest<ProductModel>
    {
        public required int ProductId { get; set; }
    }
}
