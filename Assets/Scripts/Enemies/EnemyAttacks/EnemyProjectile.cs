using UnityEngine;

public class EnemyProjectile : Projectile
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerModel>().GetHit(damageData);
        }
        rb.linearVelocity = Vector3.zero;
        col.enabled = false;
        anim.SetTrigger("BulletExpired");
    }
}
