using ChatData.Managers;
using ChatData.Models;
using IdentityData.Providers;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConversationsController : ControllerBase
{
    private readonly ChatManager _chatManager;
    private readonly UserProvider _userProvider;
    public ConversationsController(ChatManager chatManager, UserProvider userProvider)
    {
        _chatManager = chatManager;
        _userProvider = userProvider;
    }

    [HttpGet]
    public async Task<List<ConversationModel>> GetConversations()
    {
        return await _chatManager.GetConversations(_userProvider.UserId);
    }

    [HttpGet("{conversationId}")]
    public async Task<List<MessageModel>> GetConversationMessages(Guid conversationId)
    {
        return await _chatManager.GetConversationMessages(conversationId);
    }

    [HttpPost]
    public async Task SaveMessage(NewMessageModel model)
    {
        await _chatManager.SaveMessage(_userProvider.UserId, model);
    }
}