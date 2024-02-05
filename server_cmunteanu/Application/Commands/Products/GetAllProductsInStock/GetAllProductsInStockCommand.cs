using Application.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Products.GetAllProductsInStock
{
    public class GetAllProductsInStockCommand : IRequest<List<ProductModel>>
    {
    }
}
