using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : EnemyObserver
{
    [SerializeField]
    private Image hpBar;

    protected override void OnEnemyCreated(Enemy enemy)
    {
        hpBar.fillAmount = 1;

        Debug.Log("Created new enemy with stats:\n" +
            "HP: " + enemy.MaxHP + "\n" +
            "Speed: " + enemy.Speed + "\n" +
            "Damage: " + enemy.DamageDealt + "\n" +
            "Money: " + enemy.MoneyDropped + "\n" +
            "XP: " + enemy.XP);
    }

    protected override void OnEnemyHit(Enemy enemy, DamageData damageData)
    {
        float maxHP = enemy.MaxHP;
        float currentHP = enemy.currentHP;
        if (currentHP == 0) Destroy(this.gameObject);
        hpBar.fillAmount = currentHP / maxHP;
    }
}
