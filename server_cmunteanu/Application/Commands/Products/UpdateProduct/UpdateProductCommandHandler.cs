using Application.Exceptions;
using Application.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository) 
        {
            _productRepository = productRepository;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = request.ProductModel;
            var oldProduct = await _productRepository.GetById(request.ProductId);
            if(oldProduct == null)
            {
                throw new ProductNotFound("Could not find product!");
            }

            oldProduct.Name = product.Name;
            oldProduct.Price = product.Price;
            oldProduct.Stock = product.Stock;
            oldProduct.Photo = product.Photo;

            await _productRepository.Update(oldProduct);
            await _productRepository.SaveChanges();
        }
    }
}
