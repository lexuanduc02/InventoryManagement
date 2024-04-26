using InventoryManagement.Domains.EF;
using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.MerchandiseModels;
using InventoryManagement.Models.ReportModels;
using InventoryManagement.Repositories.Contractors;
using InventoryManagement.Services.Contractors;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class ReportService : IReportService
    {
        private readonly DataContext _dataContext;
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork,
            DataContext dataContext)
        {
            _unitOfWork = unitOfWork;
            _dataContext = dataContext;
        }

        public async Task<ServiceResponseModel<List<InventoryReportViewModel>>> InventoryReport()
        {
            var response = new ServiceResponseModel<List<InventoryReportViewModel>>()
            {
                Message = "Cố lỗi hệ thống!",
                isSuccess = false,
            };

            try
            {
                var data = await _dataContext.Warehouses
                    .Select(x => new InventoryReportViewModel()
                    {
                        WarehouseName = x.Name,
                        products = x.Merchandises.Select(m => new ProductViewModel()
                        {
                            Id = m.Id.ToString(),
                            Name = m.Name,
                            Price = m.Price,
                            Quantity = m.Quantity,
                            Unit = m.Unit,
                            Image = m.Image,
                        }).ToList(),
                    }).ToListAsync();

                if(data != null)
                {
                    response.Message = "Lấy dữ liệu thành công!";
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

        public async Task<ServiceResponseModel<List<MonthlyProductReportViewModel>>> MonthlyProductReport(DateTime date)
        {
            var response = new ServiceResponseModel<List<MonthlyProductReportViewModel>>()
            {
                isSuccess = false,
            };

            try
            {
                var data = await _unitOfWork.ReportRepository.MonthlyProductReport(date);

                if(!data.Any()) 
                {
                    response.Message = "Không có thông tin báo cáo";

                    return response;
                }

                response.data = data;
                response.Message = "Lấy thông tin thành công!";
                response.isSuccess = true;

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<InvoiceReportViewModel>> PurchaseReport(DateTime startDate, DateTime endDate)
        {
            var response = new ServiceResponseModel<InvoiceReportViewModel>()
            {
                isSuccess = false,
            };

            try
            {
                var data = await _unitOfWork.ReportRepository.PurchaseReport(startDate, endDate);

                var totalAmount = (float)0;
                var totalReturnAmount = (float)0;

                foreach ( var item in data)
                {
                    if (item.InvoiceType == Commons.Enums.InvoiceTypeEnum.Invoice)
                    {
                        totalAmount += item.Total;
                    }
                    else
                    {
                        totalReturnAmount += item.Total;
                    }
                }

                var reportData = new InvoiceReportViewModel()
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    Details = data,
                    TotalAmount = totalAmount,
                    TotalReturnAmount = totalReturnAmount
                };

                response.data = reportData;
                response.Message = "Lấy thông tin thành công!";
                response.isSuccess = true;

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public async Task<ServiceResponseModel<InvoiceReportViewModel>> SaleReport(DateTime startDate, DateTime endDate)
        {
            var response = new ServiceResponseModel<InvoiceReportViewModel>()
            {
                isSuccess = false,
            };

            try
            {
                var data = await _unitOfWork.ReportRepository.SaleReport(startDate, endDate);

                var totalAmount = (float)0;
                var originalTotalAmount = (float)0;
                var totalReturnAmount = (float)0;

                foreach (var item in data)
                {
                    if (item.InvoiceType == Commons.Enums.InvoiceTypeEnum.Invoice)
                    {
                        totalAmount += item.Total;
                        originalTotalAmount += item.OriginalTotal;
                    } else
                    {
                        totalReturnAmount += item.Total;
                    }
                }

                var reportData = new InvoiceReportViewModel()
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    Details = data,
                    TotalAmount = totalAmount,
                    TotalReturnAmount = totalReturnAmount,
                    OriginalTotalAmount = originalTotalAmount,
                };

                response.data = reportData;
                response.Message = "Lấy thông tin thành công!";
                response.isSuccess = true;

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }
    }
}
