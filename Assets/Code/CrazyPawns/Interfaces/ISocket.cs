namespace CrazyPawns.Pawns
{
    public interface ISocket : IMaterialChanger
    {
        ISocketView SocketView { get; }
        IPawn Owner { get; }
        bool HasConnection { get; }
        void SetConnectionState(bool isConnected);
    }
}