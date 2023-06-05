using ChatData.Context;
using ChatData.Entities;
using ChatData.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatData.Managers;

public class ChatManager
{
    private readonly ChatDbContext _context;

    public ChatManager(ChatDbContext context)
    {
        _context = context;
    }

    public async Task SaveMessage(Guid userId, NewMessageModel model)
    {
        var conversation = await _context.Conversations
            .Where(c => c.UserIds.Contains(userId) &&
                        c.UserIds.Contains(model.ToUserId))
            .FirstOrDefaultAsync();


        if (conversation == null)
        {
            conversation = new Conversation()
            {
                UserIds = new List<Guid>() { userId, model.ToUserId }
            };

            await _context.Conversations.AddAsync(conversation);
            await _context.SaveChangesAsync();
        }

        var message = new Message()
        {
            ConversationId = conversation.Id,
            Date = DateTime.Now,
            FromUserId = userId,
            Text = model.Text
        };


        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ConversationModel>> GetConversations(Guid userId)
    {
        var conversations = await _context.Conversations.Where(conversation => conversation.UserIds.Contains(userId))
            .ToListAsync();

        return conversations.Select(conversation => new ConversationModel()
        {
            FromUserId = conversation.UserIds.First(u => u != userId),
            Id = conversation.Id
        }).ToList();
    }


    public async Task<List<MessageModel>> GetConversationMessages(Guid conversationId)
    {
        var messages = await _context.Messages.Where(m => m.ConversationId == conversationId)
            .ToListAsync();

        return messages.Select(message => new MessageModel()
        {
            FromUserId = message.FromUserId,
            CreatedDate = message.Date,
            Id = message.Id,
            Text = message.Text
        }).ToList();
    }
}