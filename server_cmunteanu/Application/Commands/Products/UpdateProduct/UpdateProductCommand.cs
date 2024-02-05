using Application.Models.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Products.UpdateProduct
{
    public class UpdateProductCommand : IRequest
    {
        public required int ProductId { get; set; }
        public required ProductUpdateModel ProductModel { get; set; }
    }
}
