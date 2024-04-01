﻿using InventoryManagement.Commons.Enums;

namespace InventoryManagement.Models.CustomerModels
{
    public class CustomerViewModel
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public ActiveEnum IsActive { get; set; }
    }
}
