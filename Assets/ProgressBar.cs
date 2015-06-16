using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour {


    private Text progressLabel;
    private Image progressBar;
    public int maxValue;

    public float targetValue;
    public float currentValue;
    public int minValue;
    
    public bool enableSmoothing;
    public float smoothingFactor= 9;

    private bool isLabel;
   
	// Use this for initialization
	
    void Start () {
        progressBar = GetComponent<Image>();
        isLabel = (progressLabel = GetComponentInChildren<Text>());
	}

    void setMaxValue(float maxValue)
    {
        maxValue = maxValue;
    }
	// Update is called once per frame
	void Update () {

        if (enableSmoothing)
        {
            if (!Mathf.Approximately(targetValue, currentValue))
            {
                currentValue = Mathf.Lerp(currentValue, targetValue, Time.deltaTime * smoothingFactor);
                UpdateProgressBar();
            }
        }
        else
        {
            currentValue = targetValue;
            UpdateProgressBar();
        }
       }


    void UpdateProgressBar()
    {
        progressBar.fillAmount = currentValue / maxValue;
        if(isLabel)
            progressLabel.text = Mathf.RoundToInt(currentValue).ToString() + " / " +  maxValue.ToString();
    }
}
