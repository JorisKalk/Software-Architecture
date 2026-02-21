using UnityEngine;

public class MeleeEnemyBehaviour : EnemyBehaviour
{
    [SerializeField]
    private LayerMask playerLayer;

    [SerializeField]
    private float moveDirTime = 2.0f;
    private float moveTimeLeft;

    private float moveDir = 1f;

    [SerializeField]
    private float attackMoveSpeed;
    private bool isAttacking = false;
    private float timeToAttack;

    protected void Start()
    {
        moveTimeLeft = moveDirTime / 2f;
    }

    protected override void MoveBehaviour()
    {
        if (!isAttacking)
        {
            if (moveTimeLeft > 0f)
            {
                rb.linearVelocityX = enemy.Speed * moveDir;
                moveTimeLeft -= Time.deltaTime;
            }
            else
            {
                if (sprite.flipX) sprite.flipX = false;
                else sprite.flipX = true;
                ReverseMoveDir();
            }
        }
    }

    protected override void AttackBehaviour()
    {
        if (!isAttacking)
        {
            if ((sprite.flipX && AttackRayLeft()) || (!sprite.flipX && AttackRayRight()))
            {
                PrepareAttack();
            }
        }
        else
        {
            if (timeToAttack > 0f)
            {
                timeToAttack -= Time.deltaTime;
            }
            else
            {
                rb.linearVelocityX = attackMoveSpeed * moveDir;
            }
        }
    }

    private void PrepareAttack()
    {
        isAttacking = true;
        rb.linearVelocityX = 0;
        timeToAttack = enemy.AttackDelay;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile")) return;

        if (sprite.flipX && HitLeft())
        {
            sprite.flipX = false;
            ReverseMoveDir();
            CheckForAttackEnd(collision);
        }
        else if (!sprite.flipX && HitRight())
        {
            sprite.flipX = true;
            ReverseMoveDir();
            CheckForAttackEnd(collision);
        }
    }

    private void ReverseMoveDir()
    {
        moveDir *= -1f;
        moveTimeLeft = moveDirTime;
    }

    private void CheckForAttackEnd(Collision2D collision)
    {
        if (!isAttacking) return;
        else
        {
            isAttacking = false;
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerModel>().GetHit(damageData);
            }
        }
    }

    private bool HitLeft()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, UnityEngine.Vector2.left, .1f);
    }
    private bool HitRight()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, UnityEngine.Vector2.right, .1f);
    }

    private bool AttackRayLeft()
    {
        return Physics2D.Raycast(col.bounds.center, UnityEngine.Vector2.left, enemy.AttackRange, playerLayer);
    }
    private bool AttackRayRight()
    {
        return Physics2D.Raycast(col.bounds.center, UnityEngine.Vector2.right, enemy.AttackRange, playerLayer);
    }
}
