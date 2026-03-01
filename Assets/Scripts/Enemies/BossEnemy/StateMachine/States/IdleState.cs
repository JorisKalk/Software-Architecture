using UnityEngine;

public class IdleState : State
{
    private float startTime;
    private float idleTime;

    public IdleState(float pIdleTime)
    {
        idleTime = pIdleTime;
    }

    public override void Enter()
    {
        base.Enter();
        startTime = Time.time;
    }

    public bool IdleTimeOver()
    {
        return Time.time >= startTime + idleTime;
    }
}
