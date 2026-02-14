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
        ChangeHpBar(player);
    }

    protected override void OnPlayerHealed(Player player, int healAmount)
    {
        ChangeHpBar(player);
    }

    protected override void OnPlayerMaxHpChanged(Player player)
    {
        ChangeHpBar(player);
    }

    protected void ChangeHpBar(Player player)
    {
        float maxHP = player.MaxHP;
        float currentHP = player.currentHP;
        hpBar.fillAmount = currentHP / maxHP;
    }
}
