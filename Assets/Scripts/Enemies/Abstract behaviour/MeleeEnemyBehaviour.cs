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
    private float timeToAttack;

    private Vector2 lookDir;

    private void Start()
    {
        moveTimeLeft = moveDirTime / 2f;
    }

    protected override void ExtraOnEnemyCreated(Enemy enemy) { }

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

        if (sprite.flipX) lookDir = Vector2.left;
        else lookDir = Vector2.right;
    }

    protected override void AttackBehaviour()
    {
        if (!isAttacking)
        {
            if (AttackRay())
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

        if (sprite.flipX && HitObstacle())
        {
            sprite.flipX = false;
            ReverseMoveDir();
            CheckForAttackEnd(collision);
        }
        else if (!sprite.flipX && HitObstacle())
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

    private bool HitObstacle()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, lookDir, .1f);
    }

    private bool AttackRay()
    {
        return Physics2D.Raycast(col.bounds.center, lookDir, enemy.AttackRange, playerLayer);
    }
}
