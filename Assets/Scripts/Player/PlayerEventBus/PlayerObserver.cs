using UnityEngine;

public abstract class PlayerObserver : MonoBehaviour
{
    [SerializeField]
    protected PlayerModel playerModel;

    protected void OnEnable()
    {
        playerModel.PlayerCreated += OnPlayerCreated;
        playerModel.OnHit += OnPlayerHit;
    }

    protected void OnDisable()
    {
        playerModel.PlayerCreated -= OnPlayerCreated;
        playerModel.OnHit -= OnPlayerHit;
    }

    protected abstract void OnPlayerCreated(Player player);

    protected abstract void OnPlayerHit(Player player, DamageData damageData);
}
