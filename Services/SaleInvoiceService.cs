using AutoMapper;
using InventoryManagement.Domains.EF;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.CustomerModels;
using InventoryManagement.Models.SaleInvoiceModels;
using InventoryManagement.Services.Contractors;

namespace InventoryManagement.Services
{
    public class SaleInvoiceService : ISaleInvoiceService
    {
		private readonly IProductService _productService;
		private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;

        public SaleInvoiceService(IProductService productService, 
            ICustomerService customerService,
            IMapper mapper,
            DataContext dataContext)
        {
            _productService = productService;
            _customerService = customerService;
            _mapper = mapper;
            _dataContext = dataContext;
        }

        public Task<ServiceResponseModel<bool>> CreateAsync(CreateSaleInvoiceRequest request)
        {
            throw new NotImplementedException();
        }

        //public async Task<ServiceResponseModel<bool>> CreateAsync(CreateSaleInvoiceRequest request)
        //{
        //    var response = new ServiceResponseModel<bool>()
        //    {
        //        isSuccess = false,
        //    };

        //    try
        //    {
        //        var invoiceDetails = request.MerchandiseSaleInvoices;

        //        foreach (var invoice in invoiceDetails)
        //        {
        //            var product = await _productService.Get(invoice.MerchandiseId.ToString());

        //            if (product == null)
        //                continue;

        //            invoice.SellingPrice = product.data.Price * (100 % -invoice.Voucher);
        //        }

        //        var saleInvoice = new SaleInvoice()
        //        {
        //            UserId = new Guid(request.UserId),

        //        };

        //        var customer = await _customerService.GetByPhoneAsync(request.Customer.PhoneNumber.ToString());

        //        if(customer == null)
        //        {
        //            var newCustomer = new CreateCustomerRequest()
        //            {
        //                FullName = request.Customer.FullName,
        //                Address = request.Customer.Address,
        //                PhoneNumber = request.Customer.PhoneNumber,
        //                Email = request.Customer.Email,
        //            };

        //            var result = await _customerService.CreateAsync(newCustomer);

        //            if (result.isSuccess == false)
        //                return response;

        //            request.CustomerId = result.data.Id.ToString();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
