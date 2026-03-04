using UnityEngine;

/// <summary>
/// Class that triggers the StageSwitchSystem to go to the next stage.
/// </summary>
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
