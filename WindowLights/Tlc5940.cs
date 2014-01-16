using System;

namespace WindowLights
{
    public class Tlc5940 : ITlc5940, IDisposable
    {
        private readonly int _numTlcs;
        internal readonly byte[] TlcGsData;
        private readonly ISerialPortWrapper _port;

        public Tlc5940(string portName, int numTlcs) : this (new SerialPortWrapper(), portName, numTlcs) {}

        public Tlc5940(ISerialPortWrapper port, string portName, int numTlcs)
        {
            _port = port;
            _port.Open(portName);
            _numTlcs = numTlcs;
            TlcGsData = new byte[_numTlcs*24];
        }

        public void Clear()
        {
            Array.Clear(TlcGsData, 0, TlcGsData.Length);   
        }

        public void Set(byte channel, byte value)
        {
            if (channel >= _numTlcs*16)
            {
                throw new ArgumentException("channel out of bounds", "channel");
            }

            int index8 = (_numTlcs*16 - 1) - channel;
            int index12 = (index8*3) >> 1;

            if ((index8 & 1) == 1)
            {
                // starts in the middle
                TlcGsData[index12] = (byte)(value >> 4);
                TlcGsData[++index12] = (byte)(value << 4);
            }
            else
            {
                // starts clea
                TlcGsData[index12] = value;
            }
        }

        public void SetAll(byte value)
        {
            for (byte i = 0; i < _numTlcs*16; i++)
            {
                Set(i, value);
            }
        }

        public void Update()
        {
            _port.Write(TlcGsData, 0, TlcGsData.Length);    
        }

        public void Dispose()
        {
            _port.Close();
            _port.Dispose();
        }
    }
}
