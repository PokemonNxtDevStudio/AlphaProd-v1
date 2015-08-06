using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TempQuit : MonoBehaviour 
{
    [SerializeField]
    private GameObject QuitPanel;

    void Start()
    {
        QuitPanel.gameObject.SetActive(false);
    }

	void Update () 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuitPanel.gameObject.SetActive(!QuitPanel.activeSelf);
        }
	
	}
    public void QuitDemo()
    {
        //Debug.Log("Quiting");
        Application.Quit();
    }
    public void CancelQuit()
    {
        //Debug.Log("Cancel");
        QuitPanel.gameObject.SetActive(false);
    }
}
