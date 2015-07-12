using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGameTimer : MonoBehaviour 
{
    [SerializeField]
    private TOD_Sky gTime;
    [SerializeField]
    private Text hoursText;
    [SerializeField]
    private Text minsText;
    [SerializeField]
    private Text amPm;

    private float hour = 0;
    private float min = 0;

	void Start ()
    {
        Timer();
        InvokeRepeating("Timer", 1, 0.5f);	//call every 1/2 of a sec
	}

    private void Timer()
    {
        hour = gTime.Cycle.Hour;
        
        min = (gTime.Cycle.Hour - (int)gTime.Cycle.Hour) * 60;
        
        if(hour < 13)
        {
            hoursText.text = ((int)hour).ToString();
        }
        else
        {
            hoursText.text = (((int)hour) - 12).ToString();
        }       
        
        if (min < 10)
        {
            minsText.text =  "0" + ((int)min).ToString();
        }
        else
        minsText.text = ((int)min).ToString();


        if(hour < 12)
        {
            amPm.text = "AM";
        }
        else
        {
            amPm.text = "PM";
        }

    }

}
