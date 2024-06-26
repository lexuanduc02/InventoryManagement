﻿using InventoryManagement.Commons.Enums;

namespace InventoryManagement.Models.RoleModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }

        public ActiveEnum IsActive { get; set; }

    }
}
