using UnityEngine;

public class EndStageGate : MonoBehaviour
{
    public void OnBossKilled(EventData eventData)
    {
        Destroy(gameObject);
    }
}
