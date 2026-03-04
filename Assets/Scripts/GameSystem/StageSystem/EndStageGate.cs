using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Makes the gate on the last stage disappear when a boss enemy gets killed.
/// </summary>
public class EndStageGate : MonoBehaviour
{
    private TilemapCollider2D col;
    private TilemapRenderer renderer;

    private void Start()
    {
        col = GetComponent<TilemapCollider2D>();
        renderer = GetComponent<TilemapRenderer>();
    }

    public void OnEnemyKilled(EventData eventData)
    {
        EnemyDieEventData enemyDieEvent = (EnemyDieEventData)eventData;
        if (enemyDieEvent.enemy.EnemyType == "Boss")
        {
            col.enabled = false;
            renderer.enabled = false;
        }
    }
}
