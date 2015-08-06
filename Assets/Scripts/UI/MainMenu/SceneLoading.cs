using UnityEngine;
using System.Collections;
using UnityEngine.UI;
enum ProgressBarType
{
    Spinner,
    Bar
}
public class SceneLoading : MonoBehaviour
{

    //public GameObject LoadingPrefab;
    public string scene;
    public bool showLoadingScreen;
    public delegate void OnSceneLoadFinish();
    public event OnSceneLoadFinish onSceneLoadFinish;
    [SerializeField]
    private float loadValue;
    public Image progressBar;

    [SerializeField]
    private Text Amount;

    void Start()
    {
        Amount.transform.parent.gameObject.SetActive(false);
    }
   // void OnMouseDown()
   //{
   //    Application.LoadLevel(scene);
    //}
    ///Use this for only loading small scenes that have under 2 seconds load time
    public void LoadSceneImmediate()
    {
    }
    public void LoadSceneAsync(string scene)
    {
        this.scene = scene;
        if (showLoadingScreen)
        {
            StartCoroutine(DisplayLoadingScreen());
        }
    }
    //Load level in background
    public void LoadSceneAsync()
    {
        if (showLoadingScreen)
        {
            StartCoroutine(DisplayLoadingScreen());
        }
    }
    IEnumerator DisplayLoadingScreen()
    {
        //Transform go = LoadingPrefab.transform.Find ("ProgressBar");
        //Debug.Log (go.gameObject.name)
        Amount.transform.parent.gameObject.SetActive(true); 
        progressBar.fillAmount = 0;
        AsyncOperation async = Application.LoadLevelAsync(scene);
        while (!async.isDone)
        {
            loadValue = async.progress;
            Amount.text = async.progress.ToString() + " %";
            progressBar.fillAmount = loadValue;
            yield return null;
        }
        progressBar.fillAmount = 1;
    }
}
