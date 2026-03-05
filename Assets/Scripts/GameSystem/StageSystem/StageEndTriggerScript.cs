using UnityEngine;
using System;

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
            if (stageSwitchSystem == null) throw new Exception("No StageSwitchSystem attached to the StageEndTrigger");
            stageSwitchSystem.NextStage();
        }
    }
}
