using Application.Models.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Products.AddProduct
{
    public class AddProductCommand : IRequest
    {
        public required ProductCreateModel ProductModel { get; set; }
    }
}
