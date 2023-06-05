using ChatApi.Hubs;
using ChatApi.Services;
using ChatData.Managers;
using ChatData.Models;
using IdentityData.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ConversationsController : ControllerBase
{
    private readonly ChatManager _chatManager;
    private readonly UserProvider _userProvider;
    private readonly IHubContext<ConversationHub> _hubContext;
    private readonly UserConnectionIdService _userConnectionIdService;
    public ConversationsController(ChatManager chatManager, UserProvider userProvider, IHubContext<ConversationHub> hubContext, UserConnectionIdService userConnectionIdService)
    {
        _chatManager = chatManager;
        _userProvider = userProvider;
        _hubContext = hubContext;
        _userConnectionIdService = userConnectionIdService;
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
        var connectionId = _userConnectionIdService.ConnectionIds.FirstOrDefault(c => c.Item1 == model.ToUserId)?.Item2;
        if (connectionId != null)
        {
            await _hubContext.Clients.Client(connectionId)
                .SendAsync("NewMessage", model);
        }

        var connectionId1 = _userConnectionIdService.ConnectionIds.FirstOrDefault(c => c.Item1 == _userProvider.UserId)?.Item2;
        if (connectionId1 != null)
        {
            await _hubContext.Clients.Client(connectionId1)
                .SendAsync("NewMessage", new MessageModel()
                {
                    Text = model.Text,
                    FromUserId = _userProvider.UserId,
                    CreatedDate = DateTime.Now
                });
        }
    }
}