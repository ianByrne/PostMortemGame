using System;
using System.IO;

namespace IanByrne.ResearchProject.Shared
{
    public interface ITcpClient : IDisposable
    {
        Stream GetStream();
        bool Connected { get; }
        int ReceiveBufferSize { get; }
        void Close();
    }
}
