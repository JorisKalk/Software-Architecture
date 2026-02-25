using UnityEngine;

public class OpenMenuButton : MonoBehaviour
{
    [SerializeField]
    private Canvas menu;

    private void Start()
    {
        menu.enabled = false;
    }
    public void OpenMenu()
    {
        if (menu.enabled == false) menu.enabled = true;
        else menu.enabled = false;
    }
}
