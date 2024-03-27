using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkDemo.Core.Interfaces;
using UnitOfWorkDemo.Core.Models;
using UnitOfWorkDemo.Services.Interfaces;

namespace UnitOfWorkDemo.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateProduct(ProductDetails productDetails)
        {
            if (productDetails == null) return false;
            await _unitOfWork.Products.Add(productDetails);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            if (productId <= 0) return false;
            var productDetails = await _unitOfWork.Products.GetById(productId);
            if (productDetails == null) return false;
            _unitOfWork.Products.Delete(productDetails);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public async Task<IEnumerable<ProductDetails>> GetAllProducts()
        {
            var productDetailsList = await _unitOfWork.Products.GetAll();
            return productDetailsList;
        }

        public async Task<ProductDetails> GetProductById(int productId)
        {
            if (productId <= 0) return null;

            var productDetails = await _unitOfWork.Products.GetById(productId);
            return productDetails ?? null;
        }

        public async Task<bool> UpdateProduct(ProductDetails productDetails)
        {
            if (productDetails == null) return false;
            var product = await _unitOfWork.Products.GetById(productDetails.Id);
            if (product == null) return false;

            //consider using an automapper here            
            {
                product.ProductName = productDetails.ProductName;
                product.ProductDescription = productDetails.ProductDescription;
                product.ProductPrice = productDetails.ProductPrice;
                product.ProductStock = productDetails.ProductStock;
            }
            _unitOfWork.Products.Update(product);
            var result = _unitOfWork.Save();

            return result > 0;
        }
    }
}

