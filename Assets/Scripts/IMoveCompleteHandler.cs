using System;

public interface IMoveCompleteHandler
{
    public event Action<Bolt> MoveComplete;
}
