﻿namespace MarketPlace.ChatBlazor.Models.ChatModels;

public class NewMessageModel
{
    public Guid ToUserId { get; set; }
    public  string Text { get; set; }
}