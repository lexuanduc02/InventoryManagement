﻿namespace InventoryManagement.Models.OauthModels
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
