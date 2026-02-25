using UnityEngine;
using System;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyType;
    public int maxHP;
    public float speed;
    public float attackRange;
    public float attackDelay;
    public int damageDealt;
    public int moneyMin;
    public int moneyMax;
    public int xp;
    public int potionsDropped;
    public string[] potionTypes;
    public int[] potionChances;

    public Enemy CreateEnemy()
    {
        return new Enemy(enemyType, maxHP, speed, attackRange, attackDelay, damageDealt, moneyMin, moneyMax,
            xp, potionsDropped, potionTypes, potionChances);
    }
}

[Serializable]
public class Enemy
{
    public string EnemyType => enemyType;
    private string enemyType;
    public int MaxHP => maxHP;
    private int maxHP;
    public int currentHP;
    public float Speed => speed;
    private float speed;
    public float AttackRange => attackRange;
    private float attackRange;
    public float AttackDelay => attackDelay;
    private float attackDelay;
    public int DamageDealt => damageDealt;
    private int damageDealt;
    public int MoneyMin => moneyMin;
    private int moneyMin;
    public int MoneyMax => moneyMax;
    private int moneyMax;
    public int MoneyDropped => moneyDropped;
    private int moneyDropped;
    public int XP => xp;
    private int xp;
    public int PotionsDropped => potionsDropped;
    private int potionsDropped;
    public string PotionTypeDropped => potionTypeDropped;
    private string potionTypeDropped;

    public Enemy(string pEnemyType, int pMaxHP, float pSpeed, float pAttackRange, float pAttackDelay,
        int pDamageDealt, int pMoneyMin, int pMoneyMax, int pXP, int pPotionsDropped,
        string[] pPotionTypes, int[] pPotionTypeOdds)
    {
        enemyType = pEnemyType;
        maxHP = pMaxHP;
        currentHP = pMaxHP;
        speed = pSpeed;
        attackRange = pAttackRange;
        attackDelay = pAttackDelay;
        damageDealt = pDamageDealt;
        moneyMin = pMoneyMin;
        moneyMax = pMoneyMax;
        moneyDropped = UnityEngine.Random.Range(moneyMin, moneyMax + 1);
        xp = pXP;
        potionsDropped = pPotionsDropped;
        if (potionsDropped > 0)
        {
            potionTypeDropped = DroppedPotionType(pPotionTypes, pPotionTypeOdds);
        }
        else
        {
            potionTypeDropped = "none";
        }
    }

    private string DroppedPotionType(string[] pPotionTypes, int[] pPotionTypeOdds)
    {
        int totalOdds = 0;
        foreach (int odds in pPotionTypeOdds) totalOdds += odds;
        if (totalOdds == 0)
        {
            potionsDropped = 0;
            return "none";
        }
        int randomInt = UnityEngine.Random.Range(1, totalOdds + 1);
        int currentOdds = 0;
        for (int i = 0; i < pPotionTypeOdds.Length; i++)
        {
            currentOdds += pPotionTypeOdds[i];
            if (randomInt <= currentOdds)
            {
                return pPotionTypes[i];
            }
        }
        potionsDropped = 0;
        return "none";
    }
}
