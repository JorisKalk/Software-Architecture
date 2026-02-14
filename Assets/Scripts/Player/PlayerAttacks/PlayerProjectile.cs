using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Animator anim;
    private Collider2D col;
    private Rigidbody2D rb;

    public DamageData damageData;

    private void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyController>().GetHit(damageData);
            }
            rb.linearVelocity = Vector3.zero;
            col.enabled = false;
            anim.SetTrigger("BulletExpired");
        }
    }

    public void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
