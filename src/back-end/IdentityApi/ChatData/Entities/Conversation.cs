using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ChatData.Entities;

public class Conversation
{
    public Guid Id { get; set; }
    public List<Guid> UserIds { get; set; } = new List<Guid>();
    public List<Message> Messages { get; set; } = new List<Message>();
}