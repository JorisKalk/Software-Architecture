using UnityEngine;

public class AttackState : State
{
    private int bounceChance;
    private int pierceChance;

    private int totalAttackChance;

    private int randomInt;

    public AttackState(int pBounceChance, int pPierceChance)
    {
        bounceChance = pBounceChance;
        pierceChance = pPierceChance;
        totalAttackChance = bounceChance + pierceChance;
    }

    public override void Enter()
    {
        base.Enter();

        randomInt = UnityEngine.Random.Range(1, totalAttackChance + 1);
    }

    public int ChosenAttack()
    {
        if (randomInt <= bounceChance)
        {
            bounceChance--;
            pierceChance++;
            return 0;
        }
        else
        {
            bounceChance++;
            pierceChance--;
            return 1;
        }
    }
}
