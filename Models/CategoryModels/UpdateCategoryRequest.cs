﻿namespace InventoryManagement.Models.CategoryModels
{
    public class UpdateCategoryRequest
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
