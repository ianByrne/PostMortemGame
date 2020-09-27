using System;
using System.IO;
using System.Linq;
using System.Threading;
using IanByrne.ResearchProject.Database;
using IanByrne.ResearchProject.Shared.Models;

namespace IanByrne.ResearchProject.Shared
{
    public class ChatScriptHandler
    {
        private readonly ITcpClient _tcpClient;
        private readonly Stream _stream;

        public ChatScriptHandler(ITcpClient tcpClient)
        {
            if (tcpClient == null)
            {
                throw new ArgumentNullException(nameof(tcpClient));
            }

            if (!tcpClient.Connected)
            {
                throw new InvalidOperationException("Client not connected");
            }

            _tcpClient = tcpClient;
            _stream = _tcpClient.GetStream();
        }

        public SendMessageResponse SendMessage(SendMessageRequest request, PostMortemContext context)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrEmpty(request.UserCookieId))
            {
                throw new ArgumentNullException(nameof(request.UserCookieId));
            }

            if (request.Message?.IndexOf(char.MinValue) > -1)
            {
                throw new InvalidOperationException("Message contains null terminator");
            }

            LogMessage(context, request.BotName, request.UserCookieId, TranscriptMessageDirection.Outbound, request.Message);

            string prefix = request.UserCookieId + char.MinValue + request.BotName + char.MinValue;
            string message = request.Message == null ? request.Message : "[ " + request.InputData + " ] " + request.Message;
            string sendDataStr = prefix + message + char.MinValue;

            var sendData = System.Text.Encoding.ASCII.GetBytes(sendDataStr);

            if (_tcpClient.Connected)
            {
                _stream.Write(sendData, 0, sendData.Length);

                var response = GetResponse();

                LogMessage(context, request.BotName, request.UserCookieId, TranscriptMessageDirection.Inbound, string.Join("\n", response.Messages));

                return response;
            }
            else
            {
                throw new InvalidOperationException("Client not connected");
            }
        }

        private SendMessageResponse GetResponse()
        {
            while (true)
            {
                if (_tcpClient.Connected && _tcpClient.ReceiveBufferSize > 0)
                {
                    using (var reader = new StreamReader(_stream))
                    {
                        string response = "";

                        while (reader.Peek() >= 0)
                        {
                            response += (char)reader.Read();
                        }

                        return new SendMessageResponse(response);
                    }
                }

                Thread.Sleep(100);
            }
        }

        private void LogMessage(PostMortemContext context, string botName, string userCookieId, TranscriptMessageDirection direction, string message)
        {
            try
            {
                var bot = context.Bots.Single(x => x.Name == botName);
                var user = context.Users.Single(x => x.CookieId == new Guid(userCookieId));

                context.Transcripts.Add(new Transcript()
                {
                    DateTime = DateTime.UtcNow,
                    Bot = bot,
                    User = user,
                    Direction = direction,
                    Message = message
                });

                context.SaveChanges();
            }
            catch
            {
                //
            }
        }
    }
}
