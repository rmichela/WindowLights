namespace WindowLights
{
    public class RgbLed
    {
        private readonly ITlc5940 _tlc;
        private readonly byte _chanR;
        private readonly byte _chanG;
        private readonly byte _chanB;

        public RgbLed(ITlc5940 tlc, byte chanR, byte chanG, byte chanB)
        {
            _chanR = chanR;
            _chanG = chanG;
            _chanB = chanB;
            _tlc = tlc;
        }
        
        public byte R
        {
            set
            {
                _tlc.Set(_chanR, value);
            }
        }

        public byte G
        {
            set
            {
                _tlc.Set(_chanG, value);
            }
        }

        public byte B
        {
            set
            {
                _tlc.Set(_chanB, value);
            }
        }

    }
}
