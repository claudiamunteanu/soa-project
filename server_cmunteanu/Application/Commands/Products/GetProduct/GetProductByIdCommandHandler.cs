using Application.Exceptions;
using Application.Models.Response;
using Application.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Products.GetProduct
{
    public class GetProductByIdCommandHandler : IRequestHandler<GetProductByIdCommand, ProductModel>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductModel> Handle(GetProductByIdCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.ProductId);
            if (product == null)
            {
                throw new ProductNotFound("Could not find product!");
            }
            return _mapper.Map<ProductModel>(product);
        }
    }
}
