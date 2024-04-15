using AutoMapper;
using InventoryManagement.Commons.Enums;
using InventoryManagement.Domains.EF;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.ProductModels;
using InventoryManagement.Models.PurchaseInvoiceModels;
using InventoryManagement.Services.Contractors;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class PurchaseInvoiceService : IPurchaseInvoiceService
    {
        private readonly IUserService _userService;
        private readonly IPartnerService _partnerService;
        private readonly IProductService _productService;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PurchaseInvoiceService(IUserService userService,
            IPartnerService partnerService,
            IProductService productService,
            DataContext context,
            IMapper mapper)
        {
            _userService = userService;
            _partnerService = partnerService;
            _productService = productService;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponseModel<List<PurchaseInvoiceViewModel>>> AllAsync()
        {
            var response = new ServiceResponseModel<List<PurchaseInvoiceViewModel>>()
            {
                isSuccess = false,
            };

            try
            {
                var query = from m in _context.Merchandises
                            join mp in _context.MerchandisePurchaseInvoices on m.Id equals mp.MerchandiseId into mmp
                            from merchandise in mmp.DefaultIfEmpty()
                            join pi in _context.PurchaseInvoices on merchandise.PurchaseInvoiceId equals pi.Id into mpi
                            from purchase in mpi.DefaultIfEmpty()
                            join u in _context.Users on purchase.UserId equals u.Id into usi
                            from user in usi.DefaultIfEmpty()
                            join p in _context.Partners on purchase.PartnerId equals p.Id into psi
                            from partner in psi.DefaultIfEmpty()
                            where purchase.IsActive == ActiveEnum.Active && purchase.InvoiceTypeEnum == InvoiceTypeEnum.Invoice
                            group new { merchandise, m, purchase, user, partner } by new { purchase.Id, user.FullName, pm = partner.FullName, purchase.PaymentMethod, purchase.Status, purchase.Note, purchase.CreateAt, purchase.UpdateAt } into grouped
                            select new
                            {
                                PurchaseInvoiceId = grouped.Key.Id,
                                UserName = grouped.Key.FullName,
                                PartnerName = grouped.Key.pm,
                                PaymentMethod = grouped.Key.PaymentMethod,
                                Status = grouped.Key.Status,
                                Note = grouped.Key.Note,
                                CreateAt = grouped.Key.CreateAt,
                                UpdateAt = grouped.Key.UpdateAt,
                                Total = grouped.Sum(x => x.merchandise.Quantity * x.merchandise.PurchasePrice)
                            };

                var data = await query
                    .Select(x => new PurchaseInvoiceViewModel()
                    {
                        Id = x.PurchaseInvoiceId.ToString(),
                        UserName = x.UserName,
                        PartnerName = x.PartnerName,
                        PaymentMethod = x.PaymentMethod,
                        Status = x.Status,
                        Note = x.Note,
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

        public async Task<ServiceResponseModel<string>> CreateAsync(CreatePurchaseInvoiceRequest request)
        {
            var response = new ServiceResponseModel<string>()
            {
                isSuccess = false,
            };

            try
            {
                var user = await _userService.Get(request.UserId);
                var partner = await _partnerService.Get(request.PartnerId);

                if(user == null || partner == null) 
                {
                    return response;
                }

                var invoiceDetails = request.MerchandisePurchaseInvoices;

                var purchaseInvoice = new PurchaseInvoice()
                {
                    UserId = new Guid(request.UserId),
                    PartnerId = new Guid(request.PartnerId),
                    PaymentMethod = request.PaymentMethod,
                    Status = request.Status,
                    Note = request.Note,
                    CreateAt = request.CreateAt,
                    IsActive = request.IsActive,

                    MerchandisePurchaseInvoices = invoiceDetails.Select(x => new MerchandisePurchaseInvoice()
                    {
                        MerchandiseId = x.MerchandiseId,
                        Quantity = x.Quantity,
                        PurchasePrice = x.PurchasePrice,
                        Unit = x.Unit ?? "Unknown",
                        IsActive = x.IsActive,
                    }).ToList()
                };

                _context.PurchaseInvoices.Add(purchaseInvoice);

                var insertResult = await _context.SaveChangesAsync();

                if (insertResult == 0)
                    return response;

                var updateProductsQuan = request.MerchandisePurchaseInvoices.Select(x => new UpdateProductQuantityRequest()
                {
                    Id = x.MerchandiseId.ToString(),
                    Quantity = x.Quantity,
                }).ToList();

                var updateProductResult = await _productService.UpdateQuantityAsync(updateProductsQuan);

                if (updateProductResult.isSuccess)
                {
                    response.isSuccess = true;
                    response.Message = "Tạo phiếu thành công";
                    response.data = purchaseInvoice.Id.ToString();
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
                isSuccess = false,
            };

            try
            {
                var saleInvoice = await _context.PurchaseInvoices
                    .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.IsActive == Commons.Enums.ActiveEnum.Active);

                if (saleInvoice == null)
                {
                    response.Message = "Không tìm thấy hóa đơn!";
                    return response;
                }

                saleInvoice.IsActive = Commons.Enums.ActiveEnum.InActive;

                _context.PurchaseInvoices.Update(saleInvoice);

                var result = await _context.SaveChangesAsync();

                if (result != 0)
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

        public async Task<ServiceResponseModel<PurchaseInvoiceViewModel>> GetAsync(string id)
        {
            var response = new ServiceResponseModel<PurchaseInvoiceViewModel>()
            {
                isSuccess = false,
            };

            try
            {
                var query = from m in _context.Merchandises
                            join mp in _context.MerchandisePurchaseInvoices on m.Id equals mp.MerchandiseId into mmp
                            from merchandise in mmp.DefaultIfEmpty()
                            join pi in _context.PurchaseInvoices on merchandise.PurchaseInvoiceId equals pi.Id into mpi
                            from purchase in mpi.DefaultIfEmpty()
                            join u in _context.Users on purchase.UserId equals u.Id into usi
                            from user in usi.DefaultIfEmpty()
                            join p in _context.Partners on purchase.PartnerId equals p.Id into psi
                            from partner in psi.DefaultIfEmpty()
                            where purchase.IsActive == ActiveEnum.Active 
                            && purchase.InvoiceTypeEnum == InvoiceTypeEnum.Invoice
                            && purchase.Id.ToString() == id
                            group new { merchandise, m, purchase, user, partner } by new { purchase.Id, user.FullName, pm = partner.FullName, pi = partner.Id, purchase.PaymentMethod, purchase.Status, purchase.Note, purchase.CreateAt, purchase.UpdateAt } into grouped
                            select new
                            {
                                PurchaseInvoiceId = grouped.Key.Id,
                                UserName = grouped.Key.FullName,
                                PartnerName = grouped.Key.pm,
                                PartnerId = grouped.Key.pi,
                                PaymentMethod = grouped.Key.PaymentMethod,
                                Status = grouped.Key.Status,
                                Note = grouped.Key.Note,
                                CreateAt = grouped.Key.CreateAt,
                                UpdateAt = grouped.Key.UpdateAt,
                                Total = grouped.Sum(x => x.merchandise.Quantity * x.merchandise.PurchasePrice)
                            };

                var data = await query
                    .Select(x => new PurchaseInvoiceViewModel()
                    {
                        Id = x.PurchaseInvoiceId.ToString(),
                        UserName = x.UserName,
                        PartnerName = x.PartnerName,
                        PartnerId = x.PartnerId.ToString(),
                        PaymentMethod = x.PaymentMethod,
                        Status = x.Status,
                        Note = x.Note,
                        CreateAt = x.CreateAt,
                        UpdateAt = x.UpdateAt,
                        Total = x.Total,
                    }).FirstOrDefaultAsync();

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

        public async Task<ServiceResponseModel<string>> UpdateAsync(UpdatePurchaseInvoiceRequest request)
        {
            var response = new ServiceResponseModel<string>()
            {
                isSuccess = false,
            };

            try
            {
                var purchaseInvoice = await _context.PurchaseInvoices
                    .FirstOrDefaultAsync(x => x.Id.ToString() == request.Id && x.IsActive == Commons.Enums.ActiveEnum.Active);

                if (purchaseInvoice == null)
                {
                    response.Message = "Hóa đơn không tồn tại";
                    return response;
                }

                _mapper.Map(request, purchaseInvoice);

                _context.PurchaseInvoices.Update(purchaseInvoice);
                var result = await _context.SaveChangesAsync();

                if (result != 0)
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
