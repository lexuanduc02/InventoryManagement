namespace InventoryManagement.Services.Contractors
{
    public interface IPartialViewService
    {
        Task<string> RenderPartialToStringAsync<TModel>(string partialName, TModel model);
    }
}
