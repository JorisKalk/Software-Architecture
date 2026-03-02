using UnityEngine;

public class AttackState : State
{
    private int bounceChance;
    private int pierceChance;

    private int totalAttackChance;

    private int randomInt;
    private int toReturn;

    private float waitTime = 0.1f;
    private float startTime;

    public AttackState(int pBounceChance, int pPierceChance)
    {
        bounceChance = pBounceChance;
        pierceChance = pPierceChance;
        totalAttackChance = bounceChance + pierceChance;
    }

    public override void Enter()
    {
        base.Enter();
        startTime = Time.time;
        randomInt = UnityEngine.Random.Range(1, totalAttackChance + 1);
        if (randomInt <= bounceChance)
        {
            bounceChance--;
            pierceChance++;
            toReturn = 0;
        }
        else
        {
            bounceChance++;
            pierceChance--;
            toReturn = 1;
        }
    }

    public int ChosenAttack()
    {
        if (Time.time >= startTime + waitTime)
        {
            return toReturn;
        }
        else return -1;
    }
}
