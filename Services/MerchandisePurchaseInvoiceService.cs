using AutoMapper;
using InventoryManagement.Domains.EF;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.MerchandisePurchaseModels;
using InventoryManagement.Models.MerchandiseSaleInvoiceModels;
using InventoryManagement.Services.Contractors;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class MerchandisePurchaseInvoiceService : IMerchandisePurchaseInvoiceService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MerchandisePurchaseInvoiceService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponseModel<List<MerchandisePurchaseViewModel>>> GetByInvoiceAsync(string invoiceId)
        {
            var response = new ServiceResponseModel<List<MerchandisePurchaseViewModel>>()
            {
                isSuccess = false,
            };

            try
            {
                var data = await _context.MerchandisePurchaseInvoices
                    .Where(x => x.PurchaseInvoiceId.ToString() == invoiceId)
                    .Include(x => x.Merchandise)
                    .Select(x => new MerchandisePurchaseViewModel()
                    {
                        PurchaseInvoiceId = x.PurchaseInvoiceId.ToString(),
                        MerchandiseId = x.MerchandiseId.ToString(),
                        MerchandiseName = x.Merchandise.Name,
                        IsActive = x.IsActive,
                        MerchandisePrice = x.MerchandisePrice,
                        Quantity = x.Quantity,
                        Unit = x.Merchandise.Unit,
                        PurchasePrice = x.PurchasePrice,
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
