using UnityEngine;

public class PierceAttackState : State
{
    public bool attacked;

    public override void Enter()
    {
        base.Enter();

        attacked = false;
    }

    public bool HasAttacked()
    {
        return attacked;
    }
}
