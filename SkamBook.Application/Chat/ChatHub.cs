using System.Collections.Concurrent;

using Microsoft.AspNetCore.SignalR;

using SkamBook.Application.Extensions;
using SkamBook.Core.Entities;
using SkamBook.Core.Interfaces;
using SkamBook.Core.Interfaces.Repositories;

namespace SkamBook.Application.Chat;

public class ChatHub : Hub
{
    private readonly IUserRepository _userRepository;
    private readonly IConversationRepository _conversationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUser _user;
    private static readonly ConcurrentDictionary<string, string> _userConnections = new ConcurrentDictionary<string, string>();

    public ChatHub(
        IUserRepository userRepository, 
        IConversationRepository conversationRepository, 
        IUnitOfWork unitOfWork,
        IUser user)
    {
        _userRepository = userRepository;
        _conversationRepository = conversationRepository;
        _unitOfWork = unitOfWork;
        _user = user;
    }

    public async Task SendPrivateMessage(Guid senderId, Guid receiverId, string message)
    {
        User sender = await _userRepository.GetUserByIdAsync(senderId);
        User receiver = await _userRepository.GetUserByIdAsync(receiverId);

        if (sender != null && receiver != null)
        {
            Conversation conversation = new Conversation
            {
                Timestamp = DateTime.Now,
                Message = message,
                SenderId = senderId,
                Sender = sender,
                ReceiverId = receiverId,
                Receiver = receiver
            };

            await _conversationRepository.AddConversationAsync(conversation);
            await _unitOfWork.Commit();
            
            if (_userConnections.TryGetValue(receiver.Email.Endereco, out string receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", sender.Id, receiver.Id, message);
            }
            
        }
    }

    public override async Task OnConnectedAsync()
    {
        var isAutenticated = _user.EstaAutenticado();

        
        if (!isAutenticated)
        {
            throw new HubException("Token de autorização inválido.");
        }
        
        
        var userId = GetUserEmail();
        _userConnections.TryAdd(userId,Context.ConnectionId);
        await base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userId = GetUserEmail();
        _userConnections.TryRemove(userId, out _);
        await base.OnDisconnectedAsync(exception);
    }

    private string GetUserEmail()
    {
        var userId = _user.ObterUserEmail();

        return userId;
    }
}
