using UnityEngine;

public class StageEndTriggerScript : MonoBehaviour
{
    [SerializeField]
    private StageSwitchSystem stageSwitchSystem;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stageSwitchSystem.NextStage();
        }
    }
}
