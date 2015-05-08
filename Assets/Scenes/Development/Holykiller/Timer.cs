using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour
{
    //*********UI Vars********
    [SerializeField]
    private Text HoursText;
    [SerializeField]
    private Text MinsText;
    [SerializeField]
    private Text TimeOftheDayText;

    [SerializeField]
    private GameObject GM_UI;


    //*******TimeVars************
    
    
    //InGameTimer Vars
    private float _timeOfDay; //keeps track of real time
    [SerializeField]
    private bool _youAreAGM = false;
    [SerializeField]
    //RealTime Vars
    private float SpeedOfOneDay = 24; //24 == 1 day is 1 real hour
    [Header( "In Game Time")]
    [SerializeField]
    private float Segs;
    [SerializeField]
    private float Mins;
    [SerializeField]
    private float Hours;
    [SerializeField]
    private float Day;

    private const float DayLength = 24 ;
    private const float SegsLegth = 60;
    private const float MinsLength = 60;
    private const float HoursLength = 60;
    private string TimeOftheDay = "AM";
    
    //Sun rotation depending of the hour of the day
    //private const float DegreesPerSeconds = 360 / DayLength;


	void Start () 
    {
        if(_youAreAGM == true)
        {
            GM_UI.SetActive(true);
        }
        else
        {
            GM_UI.SetActive(false);
        }
        _timeOfDay = 0;//make sure that base time is 0;
        SetUpDisplayTime();
	}
	

	void Update () 
    {
        _timeOfDay += Time.deltaTime; //Increase time 
        IncreaseTime();
        	
	}

    private void IncreaseTime()
    {
        if ((_timeOfDay * SpeedOfOneDay) > SegsLegth)
        {
            Segs += _timeOfDay * SpeedOfOneDay;

            _timeOfDay = 0; ;

            SetUpDisplayTime();

        }
        if (Segs >= MinsLength)
        {
            Mins++;
            Segs -= MinsLength;
            SetUpDisplayTime();


        }
        if (Mins >= HoursLength)
        {
            Hours++;
            Mins -= HoursLength;
            SetUpDisplayTime();
        }
      
        if (Hours >= DayLength)
        {
            Day++;
            Hours -= DayLength;
        }
       
    }
    private void SetUpDisplayTime()
    {
        if(Hours < 10)
        {
            HoursText.text = "0" + Hours.ToString() ;
        }
        else
            HoursText.text = Hours.ToString();
        
        if (Hours > DayLength / 2)
        {

            HoursText.text = (Hours - 12).ToString();
        }
        if(Mins < 10)
        {
            MinsText.text = "0"+Mins.ToString();
        }
        else
            MinsText.text = Mins.ToString();

        if (Hours < DayLength / 2)
        {
            TimeOftheDay = "AM";

        }
        if (Hours >= DayLength / 2)
        {
            TimeOftheDay = "PM";
        }
        TimeOftheDayText.text = TimeOftheDay;
    }
    public void AddHour()
    {
        if(_youAreAGM == true)
        {
            if (Hours == DayLength)
            {
                Hours = 0;
            }
            else
            {
                Hours++;
            }
            SetUpDisplayTime();
        }
     
    }
    public void  LowerHour()
    {
        if(_youAreAGM == true)
        {
            if (Hours > 0)
            {
                Hours--;
            }
            else
            {
                Hours = DayLength - 1;
            }

            SetUpDisplayTime();
        }        
    }
    public void AddMins()
    {
        if(_youAreAGM == true)
        {
            if (Mins == HoursLength)
            {
                Mins = 0;
            }
            else
            {
                Mins++;
            }
            SetUpDisplayTime();
        }
       
    }
    public void LowerMins()
    {
        if (_youAreAGM == true)
        {
            if (Mins == 0)
            {
                Mins = HoursLength - 1;
            }
            else
            {
                Mins--;
            }
            SetUpDisplayTime();
        }

    }

}
