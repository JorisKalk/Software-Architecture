using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyBehaviour : EnemyBehaviour
{
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
            if (anim.GetBool("Dash")) anim.SetBool("Dash", false);
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
                anim.SetBool("Dash", true);
                rb.linearVelocityX = attackMoveSpeed * moveDir;
            }
        }
    }

    private void PrepareAttack()
    {
        if (anim.GetBool("HitWall")) anim.SetBool("HitWall", false);
        isAttacking = true;
        rb.linearVelocityX = 0;
        timeToAttack = enemy.AttackDelay;
        anim.SetTrigger("StartDash");
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
            anim.SetBool("Dash", false);
            anim.SetBool("HitWall", true);
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerModel>().GetHit(damageData);
            }
        }
    }

    private bool HitObstacle()
    {
        bool result = false;
        List<RaycastHit2D> results = new List<RaycastHit2D>();
        if (col.Cast(lookDir, results, 0.1f) > 0) result = true;
        return result;
        //return Physics2D.Raycast(col.bounds.center, lookDir, (col.size.x / 2) + 0.1f);
        //return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, lookDir, .1f);
    }

    private bool AttackRay()
    {
        return Physics2D.Raycast(col.bounds.center, lookDir, enemy.AttackRange, playerLayer);
    }
}
