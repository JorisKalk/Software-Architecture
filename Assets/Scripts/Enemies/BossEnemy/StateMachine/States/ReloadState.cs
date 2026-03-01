using UnityEngine;

public class ReloadState : State
{
    public bool reloaded;

    public override void Enter()
    {
        base.Enter();

        reloaded = false;
    }

    public bool HasReloaded()
    {
        return reloaded;
    }
}
