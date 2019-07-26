namespace Combat
{
    public interface ISignalListener
    {
        void ReceiveSignal(SignalGenerator generator, int signalType, SignalData signalData);
    }
}
