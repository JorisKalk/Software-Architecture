using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected Animator anim;
    protected Collider2D col;
    protected Rigidbody2D rb;

    public DamageData damageData;

    protected void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected abstract void OnCollisionEnter2D(Collision2D collision);

    public void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
