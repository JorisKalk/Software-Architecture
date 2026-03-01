using UnityEngine;

public class BossPierceProjectile : Projectile
{
    public float lifeTime;


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void Update()
    {
        if(lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }
        else
        {
            DestroyBullet();
        }
    }
}
