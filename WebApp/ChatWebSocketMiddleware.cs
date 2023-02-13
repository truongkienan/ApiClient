using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using WebApp.Models;

namespace WebApp
{
    public class ChatWebSocketMiddleware
    {
        class ResponseMessage
        {
            public string R { get; set; }
            public string Mid { get; set; }
            public double Long { get; set; }
            public double Lat { get; set; }
        }
        private static ConcurrentDictionary<string,WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();
        private readonly RequestDelegate _next;
        public ChatWebSocketMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                await _next.Invoke(context);
                return;
            }
            CancellationToken ct = context.RequestAborted;
            WebSocket currentSocket = await context.WebSockets.AcceptWebSocketAsync();
            //var socketId = Guid.NewGuid().ToString();
            var socketId = context.Request.Cookies["AccessToken"];
            Console.WriteLine($"Token: {socketId}");
            _sockets.TryAdd(socketId, currentSocket);
            while (true)
            {
                if (ct.IsCancellationRequested)
                {
                    break;
                }
                var response = await ReceiveStringAsync(currentSocket, ct);
                //Muốn biết user là ai
                if (string.IsNullOrEmpty(response))
                {
                    if (currentSocket.State != WebSocketState.Open)
                    {
                        break;
                    }
                    continue;
                }
                ResponseMessage message=JsonSerializer.Deserialize<ResponseMessage>(response,
                    new JsonSerializerOptions {PropertyNameCaseInsensitive=true });
                using (SiteProvider provider=new SiteProvider())
                {
                    List<MemberAccessToken> list = provider.Member.GetMemberAccessTokens(message.Mid);
                    foreach (var item in list)
                    {
                        Console.WriteLine("Access Token: {0}", item.AccessToken);
                        if (_sockets.ContainsKey(item.AccessToken))
                        {
                            WebSocket socket = _sockets[item.AccessToken];
                            if (socket.State != WebSocketState.Open)
                            {
                                continue;
                            }
                            await SendStringAsync(socket, response, ct);
                        }
                    }
                }

                //Hạn chế mình muốn biết user gửi là ai
                //Gửi broadcast
                //foreach (var socket in _sockets)
                //{
                //    if (socket.Value.State != WebSocketState.Open)
                //    {
                //        continue;
                //    }
                //    await SendStringAsync(socket.Value, response, ct);
                //}
            }
            WebSocket dummy;
            _sockets.TryRemove(socketId, out dummy);
            await currentSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", ct);
            currentSocket.Dispose();
        }
        private static Task SendStringAsync(WebSocket socket, string data, CancellationToken ct = default(CancellationToken))
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            var segment = new ArraySegment<byte>(buffer);
            return socket.SendAsync(segment, WebSocketMessageType.Text, true, ct);
        }
        private static async Task<string> ReceiveStringAsync(WebSocket socket, CancellationToken ct = default(CancellationToken))
        {
            var buffer = new ArraySegment<byte>(new byte[8192]);
            using (var ms = new MemoryStream())
            {
                WebSocketReceiveResult result;
                do
                {
                    ct.ThrowIfCancellationRequested();
                    result = await socket.ReceiveAsync(buffer, ct);
                    ms.Write(buffer.Array, buffer.Offset, result.Count);
                }
                while (!result.EndOfMessage);
                ms.Seek(0, SeekOrigin.Begin);
                if (result.MessageType != WebSocketMessageType.Text)
                {
                    return null;
                }
                // Encoding UTF8: https://tools.ietf.org/html/rfc6455#section-5.6
                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }
    }
}
