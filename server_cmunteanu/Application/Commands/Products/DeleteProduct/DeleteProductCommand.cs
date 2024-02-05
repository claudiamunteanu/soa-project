using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Products.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public required int ProductId { get; set; }
    }
}
