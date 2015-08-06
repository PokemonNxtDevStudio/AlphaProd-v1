using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ErrorsManager : MonoBehaviour 
{
    //private const string ErrorsPath = "UI/Errors/ErrorMessage";

    private  string AccountsLimit = "Cant Create More Accounts";
    private  string AccountTaked = "That Account Name Already Being Used";
    private  string IncorrectPass = "Incorrect Password Try Again";
    private  string AccountDoesNotExist = "Account Is Nonexistent ";
    private  string NoLocalAccounts = "There No Local Accounts";
    private  string AllLocalAccountsDeleted = "All Local Accounts Erased";

    //[SerializeField]
    //private GameObject ErrorOBJ;
    [SerializeField]
    private Image background;
    [SerializeField]
    private Text text;


    public static ErrorsManager instance;
    private bool finishFade = true;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
       // background.gameObject.SetActive(false);
    }

    public void SpawnError(ErrorType errortype)
    {
        
        background.gameObject.SetActive(true);
        if (finishFade)
        {
            StartCoroutine("fade");
        }
        
        string err = "";
        switch(errortype)
        {
            case ErrorType.AccountsAtMaxLimit:
                err = AccountsLimit;
                break;
            case ErrorType.CantUseThatAccount:
                err = AccountTaked;
                break;
            case ErrorType.IncorrectPassword:
                err = IncorrectPass;
                break;
            case ErrorType.NonexistentAccount:
                err = AccountDoesNotExist;
                break;
            case ErrorType.NonexistingLocalAccounts:
                err = NoLocalAccounts;
                break;
            case ErrorType.AllAcountsDeleted:
                err = AllLocalAccountsDeleted;
                break;
        }


        text.text = err;
    }

    IEnumerator fade()
    {
        finishFade = false;
        for (float f = 1f; f > -0.9; f -= 0.05f)
        {
            Color b = background.color;
            b.a = f;
            background.color = b;

            Color t = text.color;
            t.a = f;
            text.color = t;

            yield return new WaitForSeconds(.1f);
        }
        //Debug.Log("Destroy Object");
        finishFade = true;
        StopCoroutine("fade");
    }
    

}

public enum ErrorType
{
    AccountsAtMaxLimit,
    CantUseThatAccount,
    IncorrectPassword,
    NonexistentAccount,
    NonexistingLocalAccounts,
    AllAcountsDeleted,

}
