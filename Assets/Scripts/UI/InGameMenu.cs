using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour 
{
    [HideInInspector]
    public bool menuEnabled = false;
    public GameObject menuObject;

	void Update () 
    {
        if (cInput.GetKeyDown("Menu"))
        {
            ToggleMenu();
        }
	}

    public void ToggleMenu()
    {
        if (menuEnabled) // Turning menu off
        {
            menuEnabled = false;
            menuObject.SetActive(false);
        }
        else if (!menuEnabled) // Turning menu on
        {
            menuEnabled = true;
            menuObject.SetActive(true);
        }
    }

    public void Exit()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
