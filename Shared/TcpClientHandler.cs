using System;
using System.IO;
using System.Net.Sockets;

namespace IanByrne.ResearchProject.Shared
{
    public class TcpClientHandler : ITcpClient
    {
        private readonly TcpClient _tcpClient;

        public TcpClientHandler(TcpClient tcpClient)
        {
            if(tcpClient == null)
            {
                throw new ArgumentNullException(nameof(tcpClient));
            }

            _tcpClient = tcpClient;
        }

        public bool Connected
        {
            get
            {
                return _tcpClient.Connected;
            }
        }

        public int ReceiveBufferSize
        {
            get
            {
                return _tcpClient.ReceiveBufferSize;
            }
        }

        public Stream GetStream()
        {
            return _tcpClient.GetStream();
        }

        public void Close()
        {
            _tcpClient.Close();
        }

        public void Dispose()
        {
            _tcpClient.Dispose();
        }
    }
}
