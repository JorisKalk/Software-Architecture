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

    public Enemy CreateEnemy()
    {
        return new Enemy(enemyType, maxHP, speed, attackRange, attackDelay, damageDealt, moneyMin, moneyMax, xp, potionsDropped);
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

    public Enemy(string pEnemyType, int pMaxHP, float pSpeed, float pAttackRange, float pAttackDelay, int pDamageDealt, int pMoneyMin, int pMoneyMax, int pXP, int pPotionsDropped)
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
    }
}
