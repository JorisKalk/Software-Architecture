using UnityEngine;

public abstract class PlayerObserver : MonoBehaviour
{
    [SerializeField]
    protected PlayerModel playerModel;

    protected void OnEnable()
    {
        playerModel.PlayerCreated += OnPlayerCreated;
        playerModel.OnHit += OnPlayerHit;
        playerModel.OnHeal += OnPlayerHealed;
        playerModel.OnMaxHpChanged += OnPlayerMaxHpChanged;
    }

    protected void OnDisable()
    {
        playerModel.PlayerCreated -= OnPlayerCreated;
        playerModel.OnHit -= OnPlayerHit;
        playerModel.OnHeal -= OnPlayerHealed;
        playerModel.OnMaxHpChanged -= OnPlayerMaxHpChanged;
    }

    protected abstract void OnPlayerCreated(Player player);

    protected abstract void OnPlayerHit(Player player, DamageData damageData);

    protected abstract void OnPlayerHealed(Player player, int healAmount);

    protected abstract void OnPlayerMaxHpChanged(Player player);
}
