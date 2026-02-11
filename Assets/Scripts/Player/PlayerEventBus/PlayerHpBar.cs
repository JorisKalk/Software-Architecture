using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : PlayerObserver
{
    [SerializeField]
    private Image hpBar;

    protected override void OnPlayerCreated(Player player)
    {
        hpBar.fillAmount = 1;
    }

    protected override void OnPlayerHit(Player player, DamageData damageData)
    {
        float maxHP = player.MaxHP;
        float currentHP = player.currentHP;
        hpBar.fillAmount = currentHP / maxHP;
    }
}
