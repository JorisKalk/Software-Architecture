using UnityEngine;

public class EnemyHpBar : EnemyObserver
{
    protected override void OnEnemyCreated(Enemy enemy)
    {
        Debug.Log("Created new enemy with stats:\n" +
            "HP: " + enemy.MaxHP + "\n" +
            "Speed: " + enemy.Speed + "\n" +
            "Damage: " + enemy.DamageDealt + "\n" +
            "Money: " + enemy.MoneyDropped + "\n" +
            "XP: " + enemy.XP);
    }

    protected override void OnEnemyHit(Enemy enemy, DamageData damageData)
    {
        
    }
}
