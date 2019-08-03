namespace LywGames.Network
{
    public abstract class AbstractNetworkInitializer
    {
        public abstract void Initial(NetworkHandlerPipeline pipeline, ConnectionType cnType);
    }
}
