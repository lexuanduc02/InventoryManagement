using AutoMapper;
using InventoryManagement.Commons.Enums;
using InventoryManagement.Domains.EF;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.CustomerModels;
using InventoryManagement.Models.ProductModels;
using InventoryManagement.Models.SaleInvoiceModels;
using InventoryManagement.Services.Contractors;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class SaleInvoiceService : ISaleInvoiceService
    {
		private readonly IProductService _productService;
		private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public SaleInvoiceService(IProductService productService, 
            ICustomerService customerService,
            IMapper mapper,
            DataContext context)
        {
            _productService = productService;
            _customerService = customerService;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponseModel<List<SaleInvoiceViewModel>>> AllAsync(InvoiceTypeEnum invoiceType)
        {
            var response = new ServiceResponseModel<List<SaleInvoiceViewModel>>() 
            {
                isSuccess = false,
            };

            try
            {
                var query = from m in _context.Merchandises
                            join ms in _context.MerchandiseSaleInvoices on m.Id equals ms.MerchandiseId into mms
                            from merchandise in mms.DefaultIfEmpty()
                            join si in _context.SaleInvoices on merchandise.SaleInvoiceId equals si.Id into msi
                            from sale in msi.DefaultIfEmpty()
                            join u in _context.Users on sale.UserId equals u.Id into usi
                            from user in usi.DefaultIfEmpty()
                            join c in _context.Customers on sale.CustomerId equals c.Id into csi
                            from customer in csi.DefaultIfEmpty()
                            join p in _context.Partners on sale.PartnerId equals p.Id into psi
                            from partner in psi.DefaultIfEmpty()
                            where sale.IsActive == ActiveEnum.Active && sale.InvoiceType == invoiceType
                            group new { merchandise, m, sale, user, customer, partner } 
                            by new { sale.Id, 
                                user.FullName, 
                                cid = customer.Id, 
                                pid = partner.Id, 
                                pn = partner.FullName, 
                                pp = partner.PhoneNumber, 
                                pcn = partner.Company,
                                cm = customer.FullName, 
                                customer.PhoneNumber, 
                                sale.PaymentMethod, 
                                sale.Status, 
                                sale.Note, 
                                sale.ShippingCarrier, 
                                sale.CreateAt, 
                                sale.UpdateAt 
                            } into grouped
                            select new
                            {
                                SaleInvoiceId = grouped.Key.Id,
                                UserName = grouped.Key.FullName,
                                PartnerName = grouped.Key.pn,
                                PartnerId = grouped.Key.pid,
                                PartnerCompany = grouped.Key.pcn,
                                CustomerName = grouped.Key.cm,
                                CustomerPhoneNumber = grouped.Key.PhoneNumber,
                                PaymentMethod = grouped.Key.PaymentMethod,
                                Status = grouped.Key.Status,
                                Note = grouped.Key.Note,
                                ShippingCarrier = grouped.Key.ShippingCarrier,
                                CreateAt = grouped.Key.CreateAt,
                                UpdateAt = grouped.Key.UpdateAt,
                                Total = grouped.Sum(x => x.merchandise.Quantity * x.merchandise.SellingPrice * (100 - x.merchandise.Voucher) / 100)
                            };

                var data = await query 
                    .Select(x => new SaleInvoiceViewModel()
                    {
                        Id = x.SaleInvoiceId.ToString(),
                        UserName = x.UserName,
                        CustomerName = x.CustomerName,
                        CustomerPhoneNumber= x.CustomerPhoneNumber,
                        PartnerName = x.PartnerName,
                        PartnerId = x.PartnerId.ToString(),
                        PartnerCompany = x.PartnerCompany,
                        PaymentMethod = x.PaymentMethod,
                        Status = x.Status,
                        Note = x.Note,
                        ShippingCarrier = x.ShippingCarrier,
                        CreateAt = x.CreateAt,
                        UpdateAt = x.UpdateAt,
                        Total = x.Total,
                    }).ToListAsync();

                if (data == null)
                    return response;

                response.isSuccess = true;
                response.data = data;

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<bool>> CreateAsync(CreateSaleInvoiceRequest request)
        {
            var response = new ServiceResponseModel<bool>()
            {
                Message = "Lỗi hệ thống",
                isSuccess = false,
            };

            try
            {
                var customer = await _customerService.GetByPhoneAsync(request.Customer.PhoneNumber.ToString());

                if (customer.data == null)
                {
                    var newCustomer = new CreateCustomerRequest()
                    {
                        FullName = request.Customer.FullName,
                        Address = request.Customer.Address,
                        PhoneNumber = request.Customer.PhoneNumber,
                        Email = request.Customer.Email,
                    };

                    var result = await _customerService.CreateAsync(newCustomer);

                    if (result.isSuccess == false)
                        return response;

                    request.CustomerId = result.data.Id.ToString();
                } else
                {
                    request.CustomerId = customer.data.Id.ToString();
                }

                var invoiceDetails = request.MerchandiseSaleInvoices;
                var saleInvoice = new SaleInvoice()
                {
                    UserId = new Guid(request.UserId),
                    CustomerId = new Guid(request.CustomerId),
                    PaymentMethod = request.PaymentMethod,
                    Status = request.Status,
                    Note = request.Note,
                    ShippingCarrier = request.ShippingCarrier,
                    CreateAt = request.CreateAt,
                    IsActive = request.IsActive,

                    MerchandiseSaleInvoices = invoiceDetails.Select(x => new MerchandiseSaleInvoice()
                    {
                        MerchandiseId = x.MerchandiseId,
                        Quantity = x.Quantity,
                        SellingPrice = x.SellingPrice,
                        Voucher = x.Voucher,
                        IsActive = x.IsActive,
                    }).ToList(),
                };

                _context.SaleInvoices.Add(saleInvoice);

                var insertResult = await _context.SaveChangesAsync();

                if (insertResult == 0)
                    return response;

                var updateQuantityList = request.MerchandiseSaleInvoices.Select(x => new UpdateProductQuantityRequest()
                {
                    Id = x.MerchandiseId.ToString(),
                    Quantity = -x.Quantity,
                }).ToList();

                var updateProductResult = await _productService.UpdateQuantityAsync(updateQuantityList);

                if(updateProductResult.isSuccess)
                {
                    response.isSuccess = true;
                    response.Message = "Tạo phiếu thành công";

                    return response;
                }

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<bool>> CreateReturnAsync(CreateSaleReturnInvoiceRequest request)
        {
            var response = new ServiceResponseModel<bool>()
            {
                Message = "Lỗi hệ thống",
                isSuccess = false,
            };

            try
            {
                var customer = await _context.Partners.FirstOrDefaultAsync(x => x.Id.ToString() == request.PartnerId);

                if (customer == null)
                {
                    response.Message = "Không tìm thấy thông tin đối tác!";
                    return response;
                }

                var invoiceDetails = request.MerchandiseSaleInvoices;
                var saleInvoice = new SaleInvoice()
                {
                    UserId = new Guid(request.UserId),
                    PartnerId = new Guid(request.PartnerId),
                    Status = request.Status,
                    Note = request.Note,
                    CreateAt = request.CreateAt,
                    IsActive = request.IsActive,
                    InvoiceType = InvoiceTypeEnum.ReturnInvoice,

                    MerchandiseSaleInvoices = invoiceDetails.Select(x => new MerchandiseSaleInvoice()
                    {
                        MerchandiseId = x.MerchandiseId,
                        Quantity = x.Quantity,
                        IsActive = x.IsActive,
                        SellingPrice = x.SellingPrice,
                    }).ToList(),
                };

                _context.SaleInvoices.Add(saleInvoice);

                var insertResult = await _context.SaveChangesAsync();

                if (insertResult == 0)
                    return response;

                var updateQuantityList = request.MerchandiseSaleInvoices.Select(x => new UpdateProductQuantityRequest()
                {
                    Id = x.MerchandiseId.ToString(),
                    Quantity = -x.Quantity,
                }).ToList();

                var updateProductResult = await _productService.UpdateQuantityAsync(updateQuantityList);

                if (updateProductResult.isSuccess)
                {
                    response.isSuccess = true;
                    response.Message = "Tạo phiếu thành công";

                    return response;
                }

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<bool>> DeleteAsync(string id)
        {
            var response = new ServiceResponseModel<bool>()
            {
                Message = "Lỗi hệ thống",
                isSuccess = false,
            };

            try
            {
                var saleInvoice = await _context.SaleInvoices
                    .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.IsActive == Commons.Enums.ActiveEnum.Active);

                if(saleInvoice == null)
                {
                    response.Message = "Không tìm thấy hóa đơn!";
                    return response;
                }

                saleInvoice.IsActive = Commons.Enums.ActiveEnum.InActive;

                _context.SaleInvoices.Update(saleInvoice);

                var result = await _context.SaveChangesAsync();

                if(result != 0)
                {
                    response.isSuccess = true;
                    response.Message = "Xóa thành công!";
                }

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<List<SaleInvoiceViewModel>>> GetReturnInvoiceByPartnerIdAsync(string id)
        {
            var response = new ServiceResponseModel<List<SaleInvoiceViewModel>>()
            {
                isSuccess = false,
            };

            try
            {
                var query = from m in _context.Merchandises
                            join ms in _context.MerchandiseSaleInvoices on m.Id equals ms.MerchandiseId into mms
                            from merchandise in mms.DefaultIfEmpty()
                            join si in _context.SaleInvoices on merchandise.SaleInvoiceId equals si.Id into msi
                            from sale in msi.DefaultIfEmpty()
                            join u in _context.Users on sale.UserId equals u.Id into usi
                            from user in usi.DefaultIfEmpty()
                            join p in _context.Partners on sale.PartnerId equals p.Id into psi
                            from partner in psi.DefaultIfEmpty()
                            where sale.PartnerId.ToString() == id
                            && sale.InvoiceType == InvoiceTypeEnum.ReturnInvoice
                            group new { merchandise, m, sale, user, partner } by new { sale.Id, user.FullName, cm = partner.FullName, partner.PhoneNumber, sale.PaymentMethod, sale.Status, sale.Note, sale.ShippingCarrier, sale.CreateAt, sale.UpdateAt, sale.InvoiceType } into grouped
                            select new
                            {
                                SaleInvoiceId = grouped.Key.Id,
                                UserName = grouped.Key.FullName,
                                PartnerName = grouped.Key.cm,
                                PartnerPhoneNumber = grouped.Key.PhoneNumber,
                                PaymentMethod = grouped.Key.PaymentMethod,
                                InvoiceType = grouped.Key.InvoiceType,
                                Status = grouped.Key.Status,
                                Note = grouped.Key.Note,
                                ShippingCarrier = grouped.Key.ShippingCarrier,
                                CreateAt = grouped.Key.CreateAt,
                                UpdateAt = grouped.Key.UpdateAt,
                                Total = grouped.Sum(x => x.merchandise.Quantity * x.merchandise.SellingPrice * (100 - x.merchandise.Voucher) / 100)
                            };

                var data = await query
                    .Select(x => new SaleInvoiceViewModel()
                    {
                        Id = x.SaleInvoiceId.ToString(),
                        UserName = x.UserName,
                        CustomerName = x.PartnerName,
                        CustomerPhoneNumber = x.PartnerName,
                        PaymentMethod = x.PaymentMethod,
                        InvoiceType = x.InvoiceType,
                        Status = x.Status,
                        Note = x.Note,
                        ShippingCarrier = x.ShippingCarrier,
                        CreateAt = x.CreateAt,
                        Total = x.Total,
                    }).ToListAsync();

                if (data != null)
                {
                    response.isSuccess = true;
                    response.data = data;
                }

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<SaleInvoiceViewModel>> GetAsync(string id)
        {
            var response = new ServiceResponseModel<SaleInvoiceViewModel>()
            {
                isSuccess = false,
            };

            try
            {
                var query = from m in _context.Merchandises
                            join ms in _context.MerchandiseSaleInvoices on m.Id equals ms.MerchandiseId into mms
                            from merchandise in mms.DefaultIfEmpty()
                            join si in _context.SaleInvoices on merchandise.SaleInvoiceId equals si.Id into msi
                            from sale in msi.DefaultIfEmpty()
                            where sale.Id.ToString() == id
                            join u in _context.Users on sale.UserId equals u.Id into usi
                            from user in usi.DefaultIfEmpty()
                            join c in _context.Customers on sale.CustomerId equals c.Id into csi
                            from customer in csi.DefaultIfEmpty()
                            join p in _context.Partners on sale.PartnerId equals p.Id into psi
                            from partner in psi.DefaultIfEmpty()
                            group new { merchandise, m, sale, user, customer, partner }
                            by new { sale.InvoiceType, sale.Id, user.FullName, cid = customer.Id, pid = partner.Id, pn = partner.FullName, pp = partner.PhoneNumber, pcn = partner.Company, cm = customer.FullName, customer.PhoneNumber, sale.PaymentMethod, sale.Status, sale.Note, sale.ShippingCarrier, sale.CreateAt, sale.UpdateAt } into grouped
                            select new
                            {
                                SaleInvoiceId = grouped.Key.Id,
                                InvoiceType = grouped.Key.InvoiceType,
                                UserName = grouped.Key.FullName,
                                PartnerName = grouped.Key.pn,
                                PartnerId = grouped.Key.pid,
                                ParnerCompany = grouped.Key.pcn,
                                CustomerName = grouped.Key.cm,
                                CustomerPhoneNumber = grouped.Key.PhoneNumber,
                                PaymentMethod = grouped.Key.PaymentMethod,
                                Status = grouped.Key.Status,
                                Note = grouped.Key.Note,
                                ShippingCarrier = grouped.Key.ShippingCarrier,
                                CreateAt = grouped.Key.CreateAt,
                                UpdateAt = grouped.Key.UpdateAt,
                                Total = grouped.Sum(x => x.merchandise.Quantity * x.merchandise.SellingPrice * (100 - x.merchandise.Voucher) / 100)
                            };

                var data = await query
                    .Select(x => new SaleInvoiceViewModel()
                    {
                        Id = x.SaleInvoiceId.ToString(),
                        UserName = x.UserName,
                        CustomerName = x.CustomerName,
                        CustomerPhoneNumber = x.CustomerPhoneNumber,
                        PartnerName = x.PartnerName,
                        PartnerId = x.PartnerId.ToString(),
                        PartnerCompany = x.ParnerCompany,
                        PaymentMethod = x.PaymentMethod,
                        Status = x.Status,
                        Note = x.Note,
                        ShippingCarrier = x.ShippingCarrier,
                        CreateAt = x.CreateAt,
                        UpdateAt = x.UpdateAt,
                        Total = x.Total,
                        InvoiceType = x.InvoiceType
                    }).FirstOrDefaultAsync();

                if (data != null)
                {
                    response.isSuccess = true;
                    response.data = data;
                }

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<List<SaleInvoiceViewModel>>> GetByCustomerIdAsync(string id, InvoiceTypeEnum invoiceType)
        {
            var response = new ServiceResponseModel<List<SaleInvoiceViewModel>>()
            {
                isSuccess = false,
            };

            try
            {
                var query = from m in _context.Merchandises
                            join ms in _context.MerchandiseSaleInvoices on m.Id equals ms.MerchandiseId into mms
                            from merchandise in mms.DefaultIfEmpty()
                            join si in _context.SaleInvoices on merchandise.SaleInvoiceId equals si.Id into msi
                            from sale in msi.DefaultIfEmpty()
                            join u in _context.Users on sale.UserId equals u.Id into usi
                            from user in usi.DefaultIfEmpty()
                            join c in _context.Customers on sale.CustomerId equals c.Id into csi
                            from customer in csi.DefaultIfEmpty()
                            where sale.CustomerId.ToString() == id
                            && sale.InvoiceType == invoiceType
                            group new { merchandise, m, sale, user, customer } by new { sale.Id, user.FullName, cm = customer.FullName, customer.PhoneNumber, sale.PaymentMethod, sale.Status, sale.Note, sale.ShippingCarrier, sale.CreateAt, sale.UpdateAt, sale.InvoiceType } into grouped
                            select new
                            {
                                SaleInvoiceId = grouped.Key.Id,
                                UserName = grouped.Key.FullName,
                                CustomerName = grouped.Key.cm,
                                CustomerPhoneNumber = grouped.Key.PhoneNumber,
                                PaymentMethod = grouped.Key.PaymentMethod,
                                InvoiceType = grouped.Key.InvoiceType,
                                Status = grouped.Key.Status,
                                Note = grouped.Key.Note,
                                ShippingCarrier = grouped.Key.ShippingCarrier,
                                CreateAt = grouped.Key.CreateAt,
                                UpdateAt = grouped.Key.UpdateAt,
                                Total = grouped.Sum(x => x.merchandise.Quantity * x.merchandise.SellingPrice * (100 - x.merchandise.Voucher) / 100)
                            };

                var data = await query
                    .Select(x => new SaleInvoiceViewModel()
                    {
                        Id = x.SaleInvoiceId.ToString(),
                        UserName = x.UserName,
                        CustomerName = x.CustomerName,
                        CustomerPhoneNumber = x.CustomerPhoneNumber,
                        PaymentMethod = x.PaymentMethod,
                        InvoiceType = x.InvoiceType,
                        Status = x.Status,
                        Note = x.Note,
                        ShippingCarrier = x.ShippingCarrier,
                        CreateAt = x.CreateAt,
                        Total = x.Total,
                    }).ToListAsync();

                if (data != null)
                {
                    response.isSuccess = true;
                    response.data = data;
                }

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<bool>> UpdateAsync(UpdateSaleInvoiceRequest request)
        {
            var response = new ServiceResponseModel<bool>()
            {
                Message = "Lỗi hệ thống",
                isSuccess = false,
            };

            try
            {
                var saleInvoice = await _context.SaleInvoices
                    .FirstOrDefaultAsync(x => x.Id.ToString() == request.SaleInvoiceViewModel.Id && x.IsActive == Commons.Enums.ActiveEnum.Active);

                if(saleInvoice == null)
                {
                    response.Message = "Hóa đơn không tồn tại";
                    return response;
                }

                _mapper.Map(request.SaleInvoiceViewModel, saleInvoice);

                _context.SaleInvoices.Update(saleInvoice);
                var result = await _context.SaveChangesAsync();

                if(result != 0)
                {
                    response.Message = "Cập nhật thành công";
                    response.isSuccess = true;
                }

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }
    }
}
