﻿using System.ComponentModel.DataAnnotations;

namespace MarketPlace.ChatBlazor.Models.IdentityModels;

public class CreateUserModel
{
    public string Name { get; set; }
    public string Password { get; set; }
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
    public string Username { get; set; }
}