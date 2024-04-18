using InventoryManagement.Models.CommonModels;
using InventoryManagement.Models.ReportModels;
using InventoryManagement.Repositories.Contractors;
using InventoryManagement.Services.Contractors;

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

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }
    }
}
