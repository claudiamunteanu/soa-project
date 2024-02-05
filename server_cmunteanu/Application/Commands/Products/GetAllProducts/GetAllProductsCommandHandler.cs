using Application.Models.Response;
using Application.Repositories.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Products.GetAllProducts
{
    public class GetAllProductsCommandHandler : IRequestHandler<GetAllProductsCommand, List<ProductModel>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public Task<List<ProductModel>> Handle(GetAllProductsCommand request, CancellationToken cancellationToken)
        {
            var products = _productRepository.GetAll();
            return Task.FromResult(_mapper.Map<List<ProductModel>>(products));
        }
    }
}
