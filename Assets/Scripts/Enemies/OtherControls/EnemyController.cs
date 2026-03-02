using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField]
    private EnemyData enemyData;
    private Enemy enemy;

    public DamageData dealtDamageData;

    public event Action<Enemy> onEnemyCreated;
    public event Action<Enemy, DamageData> OnHit;

    public bool died = false;

    //only necessary for the boss
    [Header("Variables only important for the boss")]
    private bool bossHit;
    [SerializeField]
    private Color bossHitColor;
    [SerializeField]
    private Color bossDefaultColor;
    [SerializeField]
    private float hitColorTime;
    private float hitTimer;

    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        enemy = enemyData.CreateEnemy();
        onEnemyCreated?.Invoke(enemy);
    }

    public void Update()
    {
        if (bossHit)
        {
            if (hitTimer > 0)
            {
                hitTimer -= Time.deltaTime;
            }
            else
            {
                bossHit = false;
                sprite.color = bossDefaultColor;
            }
        }
    }

    public void GetHit(DamageData damageData)
    {
        enemy.currentHP -= damageData.damage;
        if (enemy.EnemyType != "Boss" && anim != null)
        {
            anim.SetTrigger("Hit");
        }
        else if (sprite != null)
        {
            bossHit = true;
            sprite.color = bossHitColor;
            hitTimer = hitColorTime;
        }

        if (enemy.currentHP < 0)
        {
            enemy.currentHP = 0;
        }

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

    public void EnemyExpired()
    {
        enemy.EnemyExpired(this);
    }
}
