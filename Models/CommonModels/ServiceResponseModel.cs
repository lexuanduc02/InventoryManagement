
namespace InventoryManagement.Models.CommonModels
{
    public class ServiceResponseModel<T>
    {
        public bool isSuccess { get; set; }
        public string? Message { get; set; }
        public T? data { get; set; }
    }
}
