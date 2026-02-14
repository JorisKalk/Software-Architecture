using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    private EnemyData enemyData;
    private Enemy enemy;

    public event Action<Enemy> onEnemyCreated;
    public event Action<Enemy, DamageData> OnHit;

    void Start()
    {
        anim = GetComponent<Animator>();

        enemy = enemyData.CreateEnemy();
        onEnemyCreated?.Invoke(enemy);
    }

    public void GetHit(DamageData damageData)
    {
        enemy.currentHP -= damageData.damage;
        anim.SetTrigger("Hit");
        if (enemy.currentHP < 0)
        {
            enemy.currentHP = 0;
        }

        OnHit?.Invoke(enemy, damageData);
    }

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    public void ReturnAnimation()
    {
        anim.SetTrigger("Return");
    }
}
