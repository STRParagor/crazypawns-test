namespace CrazyPawns.Pawns
{
    public interface IPawn : IMaterialChanger, IDestroyable
    {
        IPawnView PawnView { get; }
        ISocket[] Sockets { get; }
    }
}