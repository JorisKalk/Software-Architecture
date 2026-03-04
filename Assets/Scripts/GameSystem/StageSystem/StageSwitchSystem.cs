using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that handles the switching between stages. When there is no next stage it reloads the scene.
/// </summary>
public class StageSwitchSystem : MonoBehaviour
{
    [SerializeField]
    private List<StageInitializer> stages = new List<StageInitializer>();

    [Header("Other References")]
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private GameObject player;

    private int currentStageIndex = 0;

    private void Awake()
    {
        if (stages.Count == 0 || stages[0] == null)
        {
            throw new Exception("First stage does not exist");
        }
        if (player == null || !player.gameObject.CompareTag("Player"))
        {
            throw new Exception("No player attached to stage switch system\n" +
                "Does the player object have the 'Player' tag?");
        }
        stages[currentStageIndex].StartStage(player);
    }

    public void NextStage()
    {
        stages[currentStageIndex].EndStage();
        currentStageIndex++;
        if (currentStageIndex >= stages.Count)
        {
            Debug.Log("final stage finished");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }
        if (stages[currentStageIndex] == null)
        {
            throw new Exception("No stage at index: " + currentStageIndex.ToString());
        }
        camera.transform.position += new Vector3(120, 0, 0);
        stages[currentStageIndex].StartStage(player);
    }
}
