using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region Variables
    // Information log objects
    public GameObject registerMenuLog;
    public GameObject loginMenuLog;
    public GameObject mainMenuLog;

    // Input field objects & strings
    public GameObject loginUsernameField;
    public GameObject loginPasswordField;
    public GameObject registerUsernameField;
    public GameObject registerEmailField;
    public GameObject registerEmailConfirmField;
    public GameObject registerPasswordField;
    public GameObject registerPasswordConfirmField;

    private string loginUsername; // These are the username and password that the user entered
    private string loginPassword; 
    private string registerUsername; // Use this username when registering an account
    private string registerEmail; // Use this email when registering an account
    private string registerEmailConfirm;
    private string registerPassword; // Use this password when registering an account
    private string registerPasswordConfirm;

    public GameObject loginScreen;
    public GameObject registerScreen;
    public GameObject playScreen;   // Play screen has been renamed to Enter Game Screen in the inspector

    // Not to be confused with the loginUsername and loginPassword, this is for when the player is actually logged in.
    [HideInInspector]   // Made these public in case Reuben needed to use them for server information
    public string username;
    [HideInInspector]
    public string password;
    #endregion

    #region MainMenuScreen
    // Main Menu Screen

    /// <summary>
    /// Display text to the user in the main menu screen
    /// </summary>
    /// <param name="log"></param>
    public void MainMenuScreenWarning(string message)
    {
        mainMenuLog.GetComponent<Text>().text = message;
    }

    /// <summary>
    /// Change the volume of the menu screens
    /// </summary>
    public void SetMusicVolume(float volume)
    {
        // TODO: Change volume based on this value
    }
    #endregion

    #region RegisterScreen

    /// <summary>
    /// Register a new player account
    /// </summary>
    public void Register()
    {
        #region
        registerUsername = registerUsernameField.GetComponent<InputField>().text;
        registerEmail = registerEmailField.GetComponent<InputField>().text;
        registerEmailConfirm = registerEmailConfirmField.GetComponent<InputField>().text;
        registerPassword = registerPasswordField.GetComponent<InputField>().text;
        registerPasswordConfirm = registerPasswordConfirmField.GetComponent<InputField>().text;
        #endregion

        // TODO: Create an account for the user
        if (registerUsername != "" && registerEmail != "" && registerEmailConfirm != "" && registerPassword != "" && registerPasswordConfirm != "")
        { 
            if (registerEmail == registerEmailConfirm)
            {
                if (registerPassword == registerPasswordConfirm)
                {
                    if (registerPassword.Length > 3)
                    {
                        // TODO: Create account and check for validation
                        RegisterScreenWarning(""); // Clear any screen warnings         
                        loginScreen.SetActive(true); // Switch to the login screen 
                        registerScreen.SetActive(false);
                        loginUsernameField.GetComponent<InputField>().text = registerUsername; // Automatically set the user's username once they register
                        Debug.Log(registerUsername + "'s account has been created with the email '" + registerEmail + "' and the password '" + registerPassword + "'");
                    }
                    else
                    {
                        RegisterScreenWarning("Password must be longer than 2 characters");
                    }
                }
                else if (registerPassword != registerPasswordConfirm)
                {
                    RegisterScreenWarning("Passwords do not match");
                }
            }
            else if (registerEmail != registerEmailConfirm)
            {
                RegisterScreenWarning("Emails do not match");
            }
        }

        else
        {
            RegisterScreenWarning("Some fields are invalid");
        }
    }

    /// <summary>
    /// Display text to the user in the register screen
    /// </summary>
    /// <param name="log"></param>
    public void RegisterScreenWarning(string message)
    {
        registerMenuLog.GetComponent<Text>().text = message;
    }
    #endregion

    #region LoginScreen
    // Login Screen

    /// <summary>
    /// Log in the player
    /// </summary>
    public void Login()
    {
        loginUsername = loginUsernameField.GetComponent<InputField>().text;
        loginPassword = loginPasswordField.GetComponent<InputField>().text;
        // TODO: Log the player into their account
        if (loginUsername != "" && loginPassword != "")
        {
            // Do stuff to make player enter the game here
            playScreen.SetActive(true);
            loginScreen.SetActive(false);
            LoginScreenWarning("");

            Debug.Log("Logging in with the username '" + username + "' and password set to '" + password + "'");
        }
        else
        {
            LoginScreenWarning("Some fields are invalid");
        }
    }

    /// <summary>
    /// Display text to the user in the login screen
    /// </summary>
    /// <param name="log"></param>
    public void LoginScreenWarning(string message)
    {
        loginMenuLog.GetComponent<Text>().text = message;
    }

    // Play Screen

    /// <summary>
    /// Starts the game
    /// </summary>
    public void Play()
    {
        // TODO: Enter the player into the game with the set information
        Debug.Log("Starting Game");
    }

    /// <summary>
    /// Log the player out and take them back into the login screen
    /// </summary>
    public void Logout()
    {
        loginPasswordField.GetComponent<InputField>().text = "";
    }

    public void EnterDevMode()
    {
        username = "DevTest01";
        password = "Test01";
        loginUsernameField.GetComponent<InputField>().text = username;
        loginPasswordField.GetComponent<InputField>().text = password;
        Debug.Log("Username set to '" + username + "' and password set to '" + password + "'");
    }

    #endregion

    #region SettingScreen

    public void SetAudioVolume()
    {

    }

    #endregion

    #region Credits

    #endregion

    public void Exit() // Put stuff in here for when player quits 
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

}