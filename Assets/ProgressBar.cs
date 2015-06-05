using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour {



    public int maxValue;

    public float targetValue;
    public float currentValue;
    public int minValue;
    private Image progressBar;
    public bool enableSmoothing;
    public float smoothingFactor= 9;
	// Use this for initialization
	
    void Start () {
        progressBar = GetComponent<Image>();
	}

    void setMaxValue(float maxValue)
    {
        maxValue = maxValue;
    }
	// Update is called once per frame
	void Update () {

        if (enableSmoothing)
        {
            if (!Mathf.Approximately(targetValue,currentValue))
            {
                currentValue = Mathf.Lerp(currentValue, targetValue, Time.deltaTime * smoothingFactor);
                progressBar.fillAmount = currentValue / maxValue;
            }
            }
        else
            currentValue = targetValue;
	}
}
