using UnityEngine;

public class BossBounceProjectile : Projectile
{
    public int bouncesLeft;
    public float projectileSpeed;

    [SerializeField]
    private LayerMask terrain;
    private Vector3 lastStartPos;

    protected override void Start()
    {
        base.Start();
        lastStartPos = col.bounds.center;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerModel>().GetHit(damageData);
            anim.SetTrigger("BulletExpired");
        }
        else if (bouncesLeft > 0)
        {
            bouncesLeft--;
            Vector3 currentPos = col.bounds.center;
            Vector3 prevDir = currentPos - lastStartPos;
            prevDir = prevDir.normalized;
            ContactPoint2D hit = collision.GetContact(0);
            Vector3 newVelocity = Vector3.Reflect(prevDir, hit.normal);
            newVelocity = newVelocity.normalized * projectileSpeed;
            rb.linearVelocity = newVelocity;
            lastStartPos = currentPos;
        }
        else
        {
            anim.SetTrigger("BulletExpired");
        }

        if (rb.linearVelocityX < 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }
}
