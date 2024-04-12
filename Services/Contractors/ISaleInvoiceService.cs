﻿using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.SaleInvoiceModels;

namespace InventoryManagement.Services.Contractors
{
    public interface ISaleInvoiceService
    {
        public Task<ServiceResponseModel<bool>> CreateAsync(CreateSaleInvoiceRequest request);
        public Task<ServiceResponseModel<List<SaleInvoiceViewModel>>> AllAsync();
        public Task<ServiceResponseModel<SaleInvoiceViewModel>> GetAsync(string id);
        public Task<ServiceResponseModel<bool>> UpdateAsync(UpdateSaleInvoiceRequest request);
        public Task<ServiceResponseModel<bool>> DeleteAsync(string id);

    }
}