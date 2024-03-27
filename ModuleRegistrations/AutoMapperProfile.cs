﻿using AutoMapper;
using InventoryManagement.Domains.Entities;
using InventoryManagement.Models.CategoryModels;
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

        }
    }
}
