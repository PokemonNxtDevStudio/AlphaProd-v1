using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class UIMainMenu : UIWindow 
{
    [SerializeField]
    private LoginControl loginControl;
   // [SerializeField]
   // private OptionsControl optionsControl;

    [SerializeField]
    private GameObject Lobby;

    [SerializeField]
    private GameObject Settings;

    [SerializeField]
    private GameObject Delete;

    
    [SerializeField]
    private GameObject DeleteALL;

    



    //public LoginControl LoginControl { get { return loginControl; } set { loginControl = value; } }
    //public OptionsControl OptionsC { get { return optionsControl; } set { optionsControl = value; } }
    

    void Start()
    {
        if(loginControl == null)
        {
            Debug.Log("No Loging Panel");
        }
       // if(optionsControl == null)
       // {
       //     Debug.Log("No Options Panel");
       // }
        ShowTab("Lobby");
        

    }

    public void ShowTab( string tab)
    {
       
        switch(tab)
        {
            case "Lobby":
                Lobby.gameObject.SetActive(true);
                loginControl.LoginPanel.transform.parent.gameObject.SetActive(false);
                Settings.gameObject.SetActive(false);
                Delete.transform.parent.gameObject.SetActive(false);
                
                break;
            case "Login":
                Lobby.gameObject.SetActive(false);
                loginControl.LoginPanel.transform.parent.gameObject.SetActive(true);
                loginControl.LoginPanel.SetActive(true);
                loginControl.CreatePanel.SetActive(false);
                Settings.gameObject.SetActive(false);
                Delete.transform.parent.gameObject.SetActive(false);                
                break;
            case "Create":
                Lobby.gameObject.SetActive(false);
                loginControl.LoginPanel.transform.parent.gameObject.SetActive(true);
                loginControl.LoginPanel.SetActive(false);
                loginControl.CreatePanel.SetActive(true);
                Settings.gameObject.SetActive(false);
                Delete.transform.parent.gameObject.SetActive(false);
                break;
            case "Settings":
                Lobby.gameObject.SetActive(false);
                loginControl.LoginPanel.transform.parent.gameObject.SetActive(true);
                loginControl.LoginPanel.SetActive(false);
                loginControl.CreatePanel.SetActive(false);
                Settings.gameObject.SetActive(true);
                Delete.transform.parent.gameObject.SetActive(false);
                break;
            case "Delete":
                Lobby.gameObject.SetActive(false);
                loginControl.LoginPanel.transform.parent.gameObject.SetActive(false);
                Settings.gameObject.SetActive(false);
                Delete.transform.parent.gameObject.SetActive(true);
                Delete.gameObject.SetActive(true);
                DeleteALL.gameObject.SetActive(false);
                break;
            case "DeleteAll":
                Lobby.gameObject.SetActive(false);
                loginControl.LoginPanel.transform.parent.gameObject.SetActive(false);
                Settings.gameObject.SetActive(false);
                Delete.transform.parent.gameObject.SetActive(true);
                Delete.gameObject.SetActive(false);
                DeleteALL.gameObject.SetActive(true);
                break;

        }
    }

    public void DeleteAccount()
    {
        loginControl.DeleteAccount();
    }
    public void DeleteAllAcounts()
    {
        loginControl.DeleteAllLocalAccounts();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void OnLoginFail()
    { }
    public void OnLoginSuccesful()
    { }
}

public enum MainMenuTab
{
    Lobby,
    Login,
    Create,
    Settings

}