using UnityEngine;
using System;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int maxHP;
    public float speed;
    public int damageDealt;
    public int moneyMin;
    public int moneyMax;
    public int xp;

    public Enemy CreateEnemy()
    {
        return new Enemy(maxHP, speed, damageDealt, moneyMin, moneyMax, xp);
    }
}

[Serializable]
public class Enemy
{
    public int MaxHP => maxHP;
    private int maxHP;
    public int currentHP;
    public float Speed => speed;
    private float speed;
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

    public Enemy(int pMaxHP, float pSpeed, int pDamageDealt, int pMoneyMin, int pMoneyMax, int pXP)
    {
        maxHP = pMaxHP;
        currentHP = pMaxHP;
        speed = pSpeed;
        damageDealt = pDamageDealt;
        moneyMin = pMoneyMin;
        moneyMax = pMoneyMax;
        moneyDropped = UnityEngine.Random.Range(moneyMin, moneyMax + 1);
        xp = pXP;
    }
}
