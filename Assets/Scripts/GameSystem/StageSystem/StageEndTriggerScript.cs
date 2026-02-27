using UnityEngine;

public class StageEndTriggerScript : MonoBehaviour
{
    [SerializeField]
    StageSwitchSystem stageSwitchSystem;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stageSwitchSystem.NextStage();
        }
    }
}
