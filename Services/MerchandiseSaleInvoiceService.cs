using AutoMapper;
using InventoryManagement.Domains.EF;
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
                var data = await _context.MerchandiseSaleInvoices
                    .Where(x => x.SaleInvoiceId.ToString() == invoiceId)
                    .Select(x => _mapper.Map<MerchandiseSaleInvoiceViewModel>(x))
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
