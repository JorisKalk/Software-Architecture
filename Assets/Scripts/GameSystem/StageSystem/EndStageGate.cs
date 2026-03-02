using UnityEngine;
using UnityEngine.Tilemaps;

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
