namespace MarketPlace.ChatBlazor.Models.ChatModels;

public class MessageModel
{
    public Guid Id { get; set; }
    public Guid FromUserId { get; set; }
    public  string Text { get; set; }
    public DateTime CreatedDate { get; set; }
}