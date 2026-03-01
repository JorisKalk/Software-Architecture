using UnityEngine;

public class PierceAttackState : State
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
