namespace CrazyPawns.Pawns
{
    public interface IConnection : IDestroyable
    {
        ISocket FirstSocket { get; }
        ISocket SecondSocket { get; }
        void UpdateConnection();
    }
}