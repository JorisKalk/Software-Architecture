using System;
using UnityEngine;

public enum AttackChoice
{
    BOUNCE = 0,
    PIERCE
}

public class BossStateMachine : FSM
{
    private BossEnemyBehavior bossBehaviour;

    //States
    private IdleState idleState;
    private AttackState attackState;
    private BounceAttackState bounceAttackState;
    private PierceAttackState pierceAttackState;
    private ReloadState reloadState;

    public Action onStartIdling;
    public Action onStartBounceAttack;
    public Action onStartPierceAttack;
    public Action onStartReloading;

    ///<param name="pBossBehaviour">The bossBehaviour component.</param>
    ///<param name="pIdleTimer">Add the attack cooldown here so that the boss idles for that long between attacks.</param>
    ///<param name="pBounceChance">Add the initial bounce attack chance.</param>
    ///<param name="pPierceChance">Add the initial pierce attack chance.</param>
    public BossStateMachine(BossEnemyBehavior pBossBehaviour, float pIdleTimer, int pBounceChance, int pPierceChance)
    {
        bossBehaviour = pBossBehaviour;

        //State setup
        idleState = new IdleState(pIdleTimer);
        attackState = new AttackState(pBounceChance, pPierceChance);
        bounceAttackState = new BounceAttackState();
        pierceAttackState = new PierceAttackState();
        reloadState = new ReloadState();

        //Transition setup
        //IdleState transitions
        idleState.transitions.Add(new Transition(idleState.IdleTimeOver, attackState));

        //AttackState transitions
        attackState.transitions.Add(new Transition(() =>
        {
            return attackState.ChosenAttack() == (int)AttackChoice.BOUNCE;
        }, bounceAttackState));
        attackState.transitions.Add(new Transition(() =>
        {
            return attackState.ChosenAttack() == (int)AttackChoice.PIERCE;
        }, pierceAttackState));

        //BounceAttackState transitions
        bounceAttackState.transitions.Add(new Transition(bounceAttackState.HasAttacked, reloadState));

        //PierceAttackState transitions
        pierceAttackState.transitions.Add(new Transition(pierceAttackState.HasAttacked, reloadState));

        //ReloadState transitions
        reloadState.transitions.Add(new Transition(reloadState.HasReloaded, idleState));

        //Setup event subscriptions
        idleState.onEnter += StartIdling;
        bounceAttackState.onEnter += StartBounceAttack;
        pierceAttackState.onEnter += StartPierceAttack;
        reloadState.onEnter += StartReloading;
    }

    public override void Enter()
    {
        base.Enter();
        currentState = idleState;
        currentState.Enter();
    }

    private void StartIdling()
    {
        onStartIdling?.Invoke();
    }

    private void StartBounceAttack()
    {
        onStartBounceAttack?.Invoke();
    }

    private void StartPierceAttack()
    {
        onStartPierceAttack?.Invoke();
    }

    private void StartReloading()
    {
        onStartReloading?.Invoke();
    }

    public void HasAttacked()
    {
        if (currentState == bounceAttackState)
        {
            bounceAttackState.attacked = true;
        }
        else if (currentState == pierceAttackState)
        {
            pierceAttackState.attacked = true;
        }
    }

    public void Reloaded()
    {
        if (currentState == reloadState)
        {
            reloadState.reloaded = true;
        }
    }
}
