using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyData enemyData;
    private Enemy enemy;

    public event Action<Enemy> onEnemyCreated;
    public event Action<Enemy, DamageData> OnHit;

    void Start()
    {
        enemy = enemyData.CreateEnemy();
        onEnemyCreated?.Invoke(enemy);
    }

    public void GetHit(DamageData damageData)
    {
        enemy.currentHP -= damageData.damage;
        if (enemy.currentHP < 0)
        {
            enemy.currentHP = 0;
        }

        OnHit?.Invoke(enemy, damageData);
    }


}
