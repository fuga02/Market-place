namespace MarketPlace.ChatData.Models;

public class NewMessageModel
{
    public Guid ToUserId { get; set; }
    public required string Text { get; set; }
}