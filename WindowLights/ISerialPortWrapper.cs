using System;

namespace WindowLights
{
    public interface ISerialPortWrapper : IDisposable
    {
        void Open(string port);
        void Write(byte[] bytes, int offset, int count);
        void Close();
    }
}