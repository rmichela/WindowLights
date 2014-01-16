using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace WindowLights
{
    public class SerialPortWrapper : ISerialPortWrapper
    {
        private SerialPort _port;

        public void Open(string port)
        {
            _port = new SerialPort(port, 9600);
            _port.Open();
        }

        public void Write(byte[] bytes, int offset, int count)
        {
            _port.Write(bytes, offset, count);
        }

        public void Close()
        {
            if (_port.IsOpen)
            {
                _port.Close();
            }
        }

        public void Dispose()
        {
            if (_port != null)
            {
                _port.Dispose();
            }
        }
    }
}
