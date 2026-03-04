using UnityEngine;

/// <summary>
/// This is the base class for all projectiles. It has a base method for collisions and can let an animation
/// destroy the projectile at the end.
/// </summary>
public abstract class Projectile : MonoBehaviour
{
    protected Animator anim;
    protected Collider2D col;
    protected Rigidbody2D rb;
    protected SpriteRenderer sprite;

    public DamageData damageData;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    protected abstract void OnCollisionEnter2D(Collision2D collision);

    public void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
