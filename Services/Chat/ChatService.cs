using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using AlumniConnect.Front.Models.Chat;
using AlumniConnect.Front.Services;

namespace AlumniConnect.Front.Services.Chat
{
    public class ChatService : IAsyncDisposable
    {
        private HubConnection? _hubConnection;
        private readonly string _hubUrl;
        private readonly AuthService _authService;
        private readonly IConfiguration _configuration;

        public event Action<ChatMessage>? OnMessageReceived;
        public event Action<string>? OnMessageRead;

        public ChatService(AuthService authService, IConfiguration configuration)
        {
            _configuration = configuration;
            _hubUrl = _configuration["ApiBaseUrl"] + "/chatHub";
            _authService = authService;
        }

        public bool IsConnected => _hubConnection?.State == HubConnectionState.Connected;

        public async Task StartConnection()
        {
            if (_hubConnection?.State == HubConnectionState.Connected)
                return;

            var token = await _authService.GetTokenAsync();
            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("User must be authenticated to use chat");

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_hubUrl, options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(token)!;
                })
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<ChatMessage>("ReceiveMessage", message =>
            {
                OnMessageReceived?.Invoke(message);
            });

            _hubConnection.On<string>("MessageRead", messageId =>
            {
                OnMessageRead?.Invoke(messageId);
            });

            try
            {
                await _hubConnection.StartAsync();
                Console.WriteLine("Connected to chat hub!");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error starting connection: {ex.Message}");
                throw;
            }
        }

        public async Task SendMessage(string receiverId, string content)
        {
            if (_hubConnection?.State != HubConnectionState.Connected)
            {
                Console.Error.WriteLine("Not connected to chat hub");
                await StartConnection();
            }

            try
            {
                await _hubConnection!.InvokeAsync("SendMessage", receiverId, content);
                Console.WriteLine($"Message sent to {receiverId}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error sending message: {ex.Message}");
                throw;
            }
        }

        public async Task MarkAsRead(string messageId)
        {
            if (_hubConnection?.State != HubConnectionState.Connected)
            {
                Console.Error.WriteLine("Not connected to chat hub");
                await StartConnection();
            }

            try
            {
                await _hubConnection!.InvokeAsync("MarkAsRead", messageId);
                Console.WriteLine($"Message {messageId} marked as read");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error marking message as read: {ex.Message}");
                throw;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection != null)
            {
                try
                {
                    await _hubConnection.DisposeAsync();
                    Console.WriteLine("Chat connection disposed");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error disposing chat connection: {ex.Message}");
                }
            }
        }
    }
}
