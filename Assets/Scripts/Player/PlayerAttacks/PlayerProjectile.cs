using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public DamageData damageData;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyController>().GetHit(damageData);
            }

            Destroy(this.gameObject);
        }
    }
}
