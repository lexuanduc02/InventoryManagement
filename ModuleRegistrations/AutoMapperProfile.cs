using AutoMapper;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Models.CategoryModels;
using InventoryManagement.Models.PartnerModels;
using InventoryManagement.Models.RoleModels;
using InventoryManagement.Models.UserModels;
using InventoryManagement.Models.WarehouseModels;

namespace InventoryManagement.ModuleRegistrations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateWarehouseRequest, Warehouse>().ReverseMap();
            CreateMap<Warehouse, WarehouseViewModel>().ReverseMap();
            CreateMap<UpdateWarehouseRequest, Warehouse>().ReverseMap();

            CreateMap<CreateCategoryRequest, Category>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<UpdateCategoryRequest, Category>().ReverseMap();

            CreateMap<CreatePartnerRequest, Partner>().ReverseMap();
            CreateMap<Partner, PartnerViewModel>().ReverseMap();
            CreateMap<UpdatePartnerRequest, Partner>().ReverseMap();

            CreateMap<Role, RoleViewModel>().ReverseMap();

            CreateMap<UpdateUserRequest, User>().ReverseMap();
        }
    }
}
