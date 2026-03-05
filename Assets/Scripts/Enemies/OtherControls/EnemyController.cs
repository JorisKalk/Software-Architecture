using UnityEngine;
using System;

/// <summary>
/// The main controller of enemies. It publishes events when the enemy gets created or gets hit.
/// </summary>
public class EnemyController : MonoBehaviour
{
    protected Animator anim;
    protected SpriteRenderer sprite;

    [SerializeField]
    protected EnemyData enemyData;
    protected Enemy enemy;

    public DamageData dealtDamageData;

    public event Action<Enemy> OnEnemyCreated;
    public event Action<Enemy, DamageData> OnHit;

    public bool died = false;

    protected void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        enemy = enemyData.CreateEnemy();
        OnEnemyCreated?.Invoke(enemy);
    }

    public virtual void GetHit(DamageData damageData)
    {
        enemy.currentHP -= damageData.damage;

        anim.SetTrigger("Hit");

        if (enemy.currentHP < 0)
        {
            enemy.currentHP = 0;
        }

        CallOnHit(enemy, damageData);
    }

    protected void CallOnHit(Enemy enemy, DamageData damageData)
    {
        OnHit?.Invoke(enemy, damageData);
    }

    public Enemy GetEnemy()
    {
        return enemy;
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
