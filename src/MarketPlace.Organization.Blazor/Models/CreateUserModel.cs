﻿using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Organization.Blazor.Models;

public class CreateUserModel
{
    public string Name { get; set; }
    public string Password { get; set; }
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
    public string Username { get; set; }
}