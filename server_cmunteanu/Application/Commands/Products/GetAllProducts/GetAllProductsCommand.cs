using Application.Models.Response;
using MediatR;

namespace Application.Commands.Products.GetAllProducts
{
    public class GetAllProductsCommand : IRequest<List<ProductModel>>
    {

    }
}
