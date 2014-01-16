namespace WindowLights
{
    public interface ITlc5940
    {
        void Clear();
        void Set(byte channel, byte value);
        void SetAll(byte value);
        void Update();
    }
}