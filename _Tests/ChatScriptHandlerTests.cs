using IanByrne.ResearchProject.Shared.Models;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace IanByrne.ResearchProject.Shared.Tests
{
    public class ChatScriptHandlerTests
    {
        private SendMessageRequest _sendMessageRequest;
        private SendMessageResponse _responseMessage;

        [SetUp]
        public void Setup()
        {
            _sendMessageRequest = new SendMessageRequest
            {
                UserCookieId = new Guid("96532257-a18b-4ccc-979b-c2c90fd80cfe").ToString(),
                BotName = "testbot",
                Message = "test message"
            };

            _responseMessage = new SendMessageResponse
            {
                Messages = new string[] { "test message received" }
            };
        }

        [Test]
        public void GivenValidHandlers_WhenSendingValidMessageRequest_ReceiveValidMessageResponse()
        {
            var client = GetValidMockTcpClient();

            var handler = new ChatScriptHandler(client.Object);

            client.Verify(client => client.Connected);
            client.Verify(client => client.GetStream());

            var response = handler.SendMessage(_sendMessageRequest, null);

            string prefix = _sendMessageRequest.UserCookieId + char.MinValue + _sendMessageRequest.BotName + char.MinValue;
            string message = prefix + _sendMessageRequest.Message + char.MinValue;
            byte[] sendMessageRequestBytes = Encoding.ASCII.GetBytes(message);

            byte[] responseMessageBytes = Encoding.ASCII.GetBytes(_responseMessage.Messages[0]);

            client.Verify(client => client.GetStream().Write(sendMessageRequestBytes, 0, sendMessageRequestBytes.Length));
            client.Verify(client => client.GetStream().Read(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()));

            Assert.AreEqual(_responseMessage.Messages[0], response.Messages[0]);
        }

        [Test]
        public void GivenValidHandlers_WhenSendingNullMessage_ReceiveValidResponse()
        {
            var client = GetValidMockTcpClient();

            var sendMessageRequest = new SendMessageRequest
            {
                UserCookieId = new Guid("96532257-a18b-4ccc-979b-c2c90fd80cfe").ToString(),
                BotName = "testbot",
                Message = null
            };

            var handler = new ChatScriptHandler(client.Object);

            client.Verify(client => client.Connected);
            client.Verify(client => client.GetStream());

            var response = handler.SendMessage(sendMessageRequest, null);

            string prefix = _sendMessageRequest.UserCookieId + char.MinValue + sendMessageRequest.BotName + char.MinValue;
            string message = prefix + sendMessageRequest.Message + char.MinValue;
            byte[] sendMessageRequestBytes = Encoding.ASCII.GetBytes(message);

            byte[] responseMessageBytes = Encoding.ASCII.GetBytes(_responseMessage.Messages[0]);

            client.Verify(client => client.GetStream().Write(sendMessageRequestBytes, 0, sendMessageRequestBytes.Length));
            client.Verify(client => client.GetStream().Read(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()));

            Assert.AreEqual(_responseMessage.Messages[0], response.Messages[0]);
        }

        [Test]
        public void GivenNullTcpClient_WhenCreatingHandler_FailOnMissingClient()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ChatScriptHandler(null));
            Assert.AreEqual("tcpClient", ex.ParamName);
        }

        [Test]
        public void GivenEmptyTcpClient_WhenCreatingHandler_FailOnDisconnectedClient()
        {
            var client = new TcpClientHandler(new TcpClient());
            var ex = Assert.Throws<InvalidOperationException>(() => new ChatScriptHandler(client));
            Assert.AreEqual("Client not connected", ex.Message);
        }

        [Test]
        public void GivenMissingUser_WhenSendingMessage_FailOnMissingUser()
        {
            var client = GetValidMockTcpClient();

            var handler = new ChatScriptHandler(client.Object);

            var request = new SendMessageRequest
            {
                BotName = "testbot",
                Message = "test message"
            };

            var ex = Assert.Throws<ArgumentNullException>(() => handler.SendMessage(request, null));
            Assert.AreEqual("UserCookieId", ex.ParamName);
        }

        [Test]
        public void GivenMissingSendMessageRequest_WhenSendingMessage_FailOnMissingRequest()
        {
            var client = GetValidMockTcpClient();

            var handler = new ChatScriptHandler(client.Object);

            var ex = Assert.Throws<ArgumentNullException>(() => handler.SendMessage(null, null));
            Assert.AreEqual("request", ex.ParamName);
        }

        [Test]
        public void GivenMessageContainsNullTerminator_WhenSendingMessage_FailOnInvalidMessage()
        {
            var client = GetValidMockTcpClient();

            var handler = new ChatScriptHandler(client.Object);

            var request = new SendMessageRequest
            {
                UserCookieId = new Guid("96532257-a18b-4ccc-979b-c2c90fd80cfe").ToString(),
                BotName = "testbot",
                Message = "test " + char.MinValue + " message"
            };

            var ex = Assert.Throws<InvalidOperationException>(() => handler.SendMessage(request, null));
            Assert.AreEqual("Message contains null terminator", ex.Message);
        }

        private Mock<ITcpClient> GetValidMockTcpClient()
        {
            var mockClient = new Mock<ITcpClient>();
            var mockStream = new Mock<Stream>();

            byte[] responseMessageBytes = Encoding.ASCII.GetBytes(_responseMessage.Messages[0]);

            mockStream.Setup(stream => stream.CanRead).Returns(true);
            mockStream.Setup(stream => stream.Length).Returns(responseMessageBytes.Length);
            mockStream.Setup(stream => stream.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();
            mockStream
                .Setup(stream => stream.Read(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(responseMessageBytes.Length)
                .Callback((byte[] buffer, int offset, int size) => responseMessageBytes.CopyTo(buffer, 0));

            mockClient.Setup(client => client.Connected).Returns(true);
            mockClient.Setup(client => client.GetStream()).Returns(mockStream.Object);
            mockClient.Setup(client => client.ReceiveBufferSize).Returns((int)mockStream.Object.Length);

            return mockClient;
        }
    }
}