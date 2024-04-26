using InventoryManagement.Models.CommonModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InventoryManagement.Ultility
{
    public class BreadcrumbAttribute : ActionFilterAttribute, IActionFilter
    {
        private readonly string _controllerName;
        private readonly string _title;

        public BreadcrumbAttribute(string title, string controllerName)
        {
            _controllerName = controllerName;
            _title = title;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Controller is Controller controller)
            {
                string controllerN = controller.GetType().Name.Replace("Controller", "");
                string path = string.IsNullOrEmpty(_title) ? $"{_controllerName}" : $"{_controllerName}/{_title}";

                controller.ViewData["Breadcrumb"] = new BreadcrumbViewModel()
                {
                    Title = _title,
                    Path = path,
                    ControllerName = _controllerName,
                    Controller = controllerN,
                };
            }
        }
    }
}
