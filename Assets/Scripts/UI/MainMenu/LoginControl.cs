using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LoginControl : MonoBehaviour 
{


    public List<Text> accountsList;
    public List<Text> passwordsTexts;


    private LocalAccounts locals = new LocalAccounts();

    private string curAccount;
    public string CurrentAccount { get { return curAccount; } set { curAccount = value; } }

    [SerializeField]
    private InputField AccountInput;//Input for Account name
    [SerializeField]
    private InputField PasswordInput;//Input for Password
    
    [SerializeField]
    private InputField CreateAccountInput;
    [SerializeField]
    private InputField CreatePasswordInput;
    [SerializeField]
    private InputField AccountToDeleteInput;

    [SerializeField]
    private SceneLoading sceneLoader;
    [SerializeField]
    private GameObject loginPanel;
    public GameObject LoginPanel { get { return loginPanel; } }
    [SerializeField]
    private GameObject createPanel;
    public GameObject CreatePanel { get { return createPanel; } }

   // [SerializeField]
   // private GameObject loginButton;

    private int MaxAccountsLimit = 5;

    
    void Awake()
    {        
        LoadFromPcLocalAccounts();//Loads Existing Local Accounts
    }

    void Start()
    {
        debugAccounts();
      /*  if (File.Exists(Application.persistentDataPath + "/" + "TrainersAccs.dat"))
        {
            loginButton.SetActive(true);
        }
        else
        {
            loginButton.SetActive(false);
        }*/
    }

    private void debugAccounts()
    {
        for (int i = 0; i < locals.Accounts.Count;i++ )
        {
            accountsList[i].text = locals.Accounts[i];
            passwordsTexts[i].text = locals.Passwords[i];
        }
           
                
    }

    /// <summary>
    /// For creating a new account
    /// </summary>
    public void CreateAccount()
    {
        //Loads Accounts
        LoadFromPcLocalAccounts();
        //Checks if the number of accounts havent overpass the limit
        if (locals.Accounts.Count > MaxAccountsLimit)
        {
            Debug.Log("Cant Add More Accounts the limit is " + MaxAccountsLimit);
            ErrorsManager.instance.SpawnError(ErrorType.AccountsAtMaxLimit);
            return;
        }
        bool exist = false;
        //Look if there is a local account with the same name
        for (int i = 0; i < locals.Accounts.Count; i++)
        {
            if (locals.Accounts[i] == CreateAccountInput.text)
            {
                Debug.Log("Account Cant be used ,it already exists");
                ErrorsManager.instance.SpawnError(ErrorType.CantUseThatAccount);
                exist = true;
                return;
            }
        }
        //if there is no local account with the same name,add it and save
        if(!exist)
        {
            locals.Accounts.Add(CreateAccountInput.text);
            locals.Passwords.Add(CreatePasswordInput.text);
            SaveToPcLocalAccounts();

        }

    }
    
    /// <summary>
    /// This is call when the login button is press
    /// </summary>
    public void LoginToAccount()
    {
        curAccount = "";
        LoadFromPcLocalAccounts();
        string namein = AccountInput.text;
        string passwordin = PasswordInput.text;
        bool found = false;
        int index = -1;
        //Look if the account exists in the localaccounts list
        for (int i = 0; i < locals.Accounts.Count; i++)
        {
            if (locals.Accounts[i] == namein)
            {
                Debug.Log("Found Account : " + namein);
                found = true;
                index = i;
            }
        }
        //if it finds the account
        if(found)
        {
            if (index == -1)
                return;

            //Check if the password match the account
            if (locals.Passwords[index] == passwordin)
            {
                //Correct Password
                //Debug.Log("Account and Password match");

                curAccount = namein;
                //TODO Add Load Character Selection Screen***************************************************************
                sceneLoader.LoadSceneAsync();
            }
            else
            {
                //Wrong Password
//                Debug.Log("Incorrect Password");
                ErrorsManager.instance.SpawnError(ErrorType.IncorrectPassword);
            }
            
        }
        else
        {
            //Didnt find Account
           // Debug.Log("That Account is non-existent");
            ErrorsManager.instance.SpawnError(ErrorType.NonexistentAccount);
        }
        
    }

    /// <summary>
    /// Save Accounts and Passwords to a local file
    /// </summary>
    public void SaveToPcLocalAccounts()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/"  + "TrainersAccs.dat");
        LocalAccounts data = new LocalAccounts();


        data = locals;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("The Data for the LocalAccounts has beed Saved");
    }

    /// <summary>
    /// Loads Accounts and Passwords from a local file
    /// </summary>
    public void LoadFromPcLocalAccounts()
    {

        if (File.Exists(Application.persistentDataPath + "/"  + "TrainersAccs.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + "TrainersAccs.dat", FileMode.Open);
            LocalAccounts data = (LocalAccounts)bf.Deserialize(file);
            file.Close();
            //Set the current lists to the lists from the file

            locals = data;
            
            //accountsLocals = data.Accounts;
            //passwordsLocals = data.Passwords;

//            Debug.Log("The Data for the LocalAccounts has beed Loaded");
        }
        else
        {
           // Debug.Log("There no existing Local Accounts");
            ErrorsManager.instance.SpawnError(ErrorType.NonexistingLocalAccounts);
        }
        
    }

    /// <summary>
    /// Delete all the local accounts
    /// </summary>
    public void DeleteAllLocalAccounts()
    {
        if (File.Exists(Application.persistentDataPath + "/" + "TrainersAccs.dat"))
        {
            File.Delete(Application.persistentDataPath + "/" + "TrainersAccs.dat");
            ErrorsManager.instance.SpawnError( ErrorType.AllAcountsDeleted);
        }
        else
        {
            //Debug.Log("There are no existing Accounts");
            ErrorsManager.instance.SpawnError( ErrorType.NonexistingLocalAccounts);
        }
    }
    

    /// <summary>
    /// Delete only one account
    /// </summary>
    public void DeleteAccount()
    {
        string nameofaccount = AccountToDeleteInput.text;
        LoadFromPcLocalAccounts();
        for (int i = 0; i < locals.Accounts.Count; i++)
        {
            if (locals.Accounts[i] == nameofaccount)
            {
                int accountIndex = locals.Accounts.IndexOf(nameofaccount);
                Debug.Log("Index of the account " + nameofaccount + " is " + accountIndex);
                Debug.Log("Password To Remove " + locals.Passwords[accountIndex]);
                locals.Accounts.Remove(nameofaccount);
                locals.Passwords.RemoveAt(accountIndex);
                SaveToPcLocalAccounts();
                Debug.Log("The Account " + nameofaccount + " has been deleted");
                return;
            }
            
        }

        Debug.Log("The Account " + nameofaccount + " does not exists");
    
    }
}

/// <summary>
/// Data to save has to be Serializable
/// </summary>
[Serializable]
class LocalAccounts
{
    private List<string> accs;
    public List<string> Accounts { get { return accs; } set { accs = value; } }
    private List<string> pass;
    public List<string> Passwords { get { return pass; } set { pass = value; } }
    public LocalAccounts()
    {
        accs = new List<string>();
        pass = new List<string>();
    }
}
