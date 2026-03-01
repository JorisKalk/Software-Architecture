using UnityEngine;

public class BounceAttackState : State
{
    public bool Attacked;

    public override void Enter()
    {
        base.Enter();

        Attacked = false;
    }

    public bool HasAttacked()
    {
        return Attacked;
    }
}
