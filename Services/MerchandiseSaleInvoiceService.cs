using AutoMapper;
using InventoryManagement.Domains.EF;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.MerchandiseSaleInvoiceModels;
using InventoryManagement.Services.Contractors;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class MerchandiseSaleInvoiceService : IMerchandiseSaleInvoiceService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MerchandiseSaleInvoiceService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponseModel<List<MerchandiseSaleInvoiceViewModel>>> GetByInvoiceAsync(string invoiceId)
        {
            var response = new ServiceResponseModel<List<MerchandiseSaleInvoiceViewModel>>()
            {
                isSuccess = false,
            };

            try
            {
                var query = from ms in _context.MerchandiseSaleInvoices
                            where ms.SaleInvoiceId.ToString() == invoiceId
                            join m in _context.Merchandises on ms.MerchandiseId equals m.Id
                            select new
                            {
                                ms,
                                MerchandiseName = m.Name,
                            };

                var data = await query
                    .Select(x => new MerchandiseSaleInvoiceViewModel()
                    {
                        SaleInvoiceId = x.ms.Id.ToString(),
                        MerchandiseId = x.ms.MerchandiseId.ToString(),
                        MerchandiseName = x.MerchandiseName,
                        Quantity = x.ms.Quantity,
                        SellingPrice = x.ms.SellingPrice,
                        Voucher = x.ms.Voucher,
                        IsActive = x.ms.IsActive,
                    })
                    .ToListAsync();

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
    }
}
