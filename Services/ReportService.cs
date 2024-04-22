using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.ReportModels;
using InventoryManagement.Repositories.Contractors;
using InventoryManagement.Services.Contractors;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventoryManagement.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public async Task<ServiceResponseModel<PurchaseReportViewModel>> PurchaseReport(DateTime? startDate, DateTime? endDate)
        {
            var response = new ServiceResponseModel<PurchaseReportViewModel>()
            {
                isSuccess = false,
            };

            try
            {
                var data = await _unitOfWork.ReportRepository.PurchaseReport(startDate, endDate);

                var totalAmount = (float)0;

                foreach ( var item in data)
                {
                    if(item.InvoiceType == Commons.Enums.InvoiceTypeEnum.Invoice)
                    {
                        totalAmount += item.Total;
                    }
                }

                var reportData = new PurchaseReportViewModel()
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    Details = data,
                    TotalAmount = totalAmount,
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
