using Application.Models.Response;
using Application.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Products.GetAllProductsInStock
{
    public class GetAllProductsInStockCommandHandler : IRequestHandler<GetAllProductsInStockCommand, List<ProductModel>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsInStockCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public Task<List<ProductModel>> Handle(GetAllProductsInStockCommand request, CancellationToken cancellationToken)
        {
            var products = _productRepository.GetAll().Where(p => p.Stock > 0);
            return Task.FromResult(_mapper.Map<List<ProductModel>>(products));
        }
    }
}
