using OrderApp.DAL;
using OrderApp.DAL.Entities;
using OrderApp.DAL.Repositories;
using OrderApp.Domain.Model;
using OrderApp.Service.Taxations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace OrderApp.Service.Commands
{
    public class CalculateOrderCommand : Command
    {
        #region Constructors


        public CalculateOrderCommand(string[] parameters, ITaxationService taxationSerive, IProductItemRepository productItemRepository)
        {
            base.Parameters = parameters;
            _taxationService = taxationSerive;
            _productItemRepository = productItemRepository;
        }

        public CalculateOrderCommand(string[] parameters)
        {
            base.Parameters = parameters;

            //TODO: inject these dependencies
            _taxationService = new TaxationService();

            string csvFile = base.Parameters[0];
            _productItemRepository = ProductItemRepositoryFactory.CreateInstance(csvFile);
        }

        #endregion

        #region Fields

        private ITaxationService _taxationService = null;
        private IProductItemRepository _productItemRepository = null;
        private List<ProductOrderItem> _productOrderItems = null;

        #endregion

        #region Public Methods

        public override OperationReturn<string> ValidateParameters()
        {
            OperationReturn<string> response = new OperationReturn<string>();

            // Checks if csv file exists
            string csvFile = Parameters[0];
            if (File.Exists(csvFile) == false)
            {
                response.MessageList.Add(new OperationInfo("02", $"Csv file doesn't exist at {csvFile}"));
                response.Success = false;
                return response;
            }

            _productOrderItems = GetProductOrderItemsFromParameters();

            // Checks if product identifier exists
            if (_productOrderItems.Any(x => _productItemRepository.GetProductItemById(x.ProductId) == null))
            {
                response.MessageList.Add(new OperationInfo("03", "All products in the order must be present in the csv source file."));
            }

            // Checks if the "quantity" parameter is valid
            if (_productOrderItems.Any(x => x.Quantity == 0 || x.Quantity < 0))
            {
                response.MessageList.Add(new OperationInfo("04", "All products in the order must have a valid quantity."));
            }

            response.Success = (response.MessageList.Count == 0);

            return response;
        }

        public override OperationReturn<string> Process()
        {
            OperationReturn<string> response = new OperationReturn<string>();

            try
            {
                List<ProductOrderItem> _productOrderItems = GetProductOrderItemsFromParameters();

                decimal totalAmount = 0;

                foreach (ProductOrderItem productOrderItem in _productOrderItems)
                {
                    ProductItem productItem = _productItemRepository.GetProductItemById(productOrderItem.ProductId);

                    if (productOrderItem.Quantity > productItem.QuantityInStock)
                    {
                        response.MessageList.Add(new OperationInfo(code: "01", message: $"Product '{productOrderItem.ProductId}' is out of stock. Ordered quantity: {productOrderItem.Quantity}; Available in stock: {productItem.QuantityInStock}"));
                        response.Success = false;
                        return response;
                    }

                    totalAmount += (productOrderItem.Quantity * productItem.Price);
                }

                // Applies VAT 
                decimal totalTaxations = 0;

                foreach (Taxation taxation in _taxationService.GetTaxations())
                {
                    totalTaxations += taxation.Apply(totalAmount);
                }

                response.Data = $"Total: {(totalAmount + totalTaxations).ToString("F", new CultureInfo("pt-PT"))}";
                response.Success = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageList.Add(new OperationInfo("500", ex.Message));

                return response;
            }
        }

        #endregion

        #region Private Methods

        private List<ProductOrderItem> GetProductOrderItemsFromParameters()
        {
            int counter = 1;
            int productOrderItemParameters = 2;

            List<ProductOrderItem> productOrderItems = new List<ProductOrderItem>();

            while (counter != Parameters.Length)
            {
                List<string> arguments = Parameters.Skip(counter).Take(productOrderItemParameters).ToList();

                productOrderItems.Add(new ProductOrderItem(productId: arguments[0], quantity: arguments[1]));

                counter += productOrderItemParameters;
            }

            return productOrderItems;
        }

        #endregion
    }
}
