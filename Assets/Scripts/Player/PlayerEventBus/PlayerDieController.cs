using UnityEngine;

public class PlayerDieController : PlayerObserver
{
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D col;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    protected override void OnPlayerCreated(Player player)
    {
        
    }

    protected override void OnPlayerHit(Player player, DamageData damageData)
    {
        if (player.currentHP == 0)
        {
            rb.bodyType = RigidbodyType2D.Static;
            col.enabled = false;
            anim.SetTrigger("Die");
        }
    }

    protected override void OnPlayerHealed(Player player, int healAmount)
    {
        
    }

    protected override void OnPlayerMaxHpChanged(Player player)
    {
        
    }
}
