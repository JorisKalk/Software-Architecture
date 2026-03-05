using UnityEngine;

/// <summary>
/// Class that extends EnemyController to control the boss. It visualizes the boss getting
/// hit in a different way from the normal enemies.
/// </summary>
public class BossEnemyController : EnemyController
{
    [Header("Variables only important for the boss")]
    private bool bossHit = false;
    [SerializeField]
    private Color bossHitColor;
    [SerializeField]
    private Color bossDefaultColor;
    [SerializeField]
    private float hitColorTime;
    private float hitTimer;

    private void Update()
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

    public override void GetHit(DamageData damageData)
    {
        enemy.currentHP -= damageData.damage;
        
        bossHit = true;
        sprite.color = bossHitColor;
        hitTimer = hitColorTime;
        
        if (enemy.currentHP < 0)
        {
            enemy.currentHP = 0;
        }

        CallOnHit(enemy, damageData);
    }
}
