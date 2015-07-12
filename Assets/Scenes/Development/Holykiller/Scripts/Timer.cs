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

    [Header("Other")]
    [SerializeField]
    private Transform sun;
    private Light sunLight;
    private LensFlare sunFlare;

    [SerializeField]
    private float dayCycleInMinutes = 1;
    //[SerializeField]
    //private float StartTimeOfTheDay;   //this time is in military time

    [SerializeField]
    private float sunRise = 0.15f;
    [SerializeField]
    private float sunSet = 0.6f;                    
    [SerializeField]
    public float skyboxBlenModifier = 5;         //speed to blend the skybox


    private const float sec = 1;
    private const float min = 60 * sec;
    private const float hour = 60 * min;
    private const float day = 24 * hour;

    private const float degrees_per_second = 360 / day;

    private float _degreeRotation;
    private float _tOfDay;

    private float _dayCycleInSeconds;


    [SerializeField]
    private float maxLightBrightness = 1.5f;
    [SerializeField]
    private float minLightBrightness = 0;

    [SerializeField]
    private float maxBrightness = 1.0f;
    [SerializeField]
    private float minBrightness = 0;


    private float _morningLength;
    private float _eveningLength;

    public enum TimeOfDay 
    {
        Idle,
        SunRise,
        SunSet

    }

    private TimeOfDay _tod;
    private float _noonTime;

    [SerializeField]
    private Color DayAmbientColor;
    [SerializeField]
    private Color NightAmbientColor;


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

        _dayCycleInSeconds = dayCycleInMinutes * min;
        _tOfDay = 0;
        _degreeRotation = degrees_per_second * day / _dayCycleInSeconds;
        sunLight = sun.GetComponent<Light>();
        sunFlare = sun.GetComponent<LensFlare>();

        RenderSettings.skybox.SetFloat("_Blend", 0);
        RenderSettings.ambientLight = NightAmbientColor;
        _tod = TimeOfDay.Idle;
        sunRise *= _dayCycleInSeconds;
        sunSet *= _dayCycleInSeconds;
        _noonTime = _dayCycleInSeconds / 2;
        sunLight.intensity = minLightBrightness;
        

        _morningLength = _noonTime - sunRise; //in secs
        _eveningLength = sunSet - _noonTime;  //in secs

	}
	

	void Update () 
    {
        _timeOfDay += Time.deltaTime; //Increase time 
        IncreaseTime();

        //other
        sun.Rotate(new Vector3(_degreeRotation,0,0) * Time.deltaTime);
        _tOfDay += Time.deltaTime;
        //Reset cycle
        if (_tOfDay > _dayCycleInSeconds)
        {
            _tOfDay -= _dayCycleInSeconds;
        }
        //Debug.Log(_tOfDay);

        if(_tOfDay > sunRise && _tOfDay < _noonTime)
        {
            AdjustLighting(true);
        }
        else if(_tOfDay > _noonTime && _tOfDay < sunSet)
        {
            AdjustLighting(false);
        }

        if(_tOfDay > sunRise && _tOfDay < sunSet && RenderSettings.skybox.GetFloat("_Blend") < 1)
        {
            _tod = TimeOfDay.SunRise;
            BlendSkybox();
        }
        else if(_tOfDay > sunSet && RenderSettings.skybox.GetFloat("_Blend") > 0)
        {
            _tod = TimeOfDay.SunSet;
            BlendSkybox();
        }
        else
        {
            _tod = TimeOfDay.Idle;
        }

        
        	
	}

    private void BlendSkybox()
    {
        float temp = 0;
        switch(_tod)
        {
            case TimeOfDay.SunRise:
                temp = (_tOfDay - sunRise)  / _dayCycleInSeconds * skyboxBlenModifier;
                break;

            case TimeOfDay.SunSet:
                temp = (_tOfDay - sunSet) / _dayCycleInSeconds * skyboxBlenModifier;
                temp = 1 - temp;
                break;
        }

      //  Debug.Log(temp);
        RenderSettings.skybox.SetFloat("_Blend", temp);
    }
   /* private void SetupLighting()
    {
        sunLight.intensity = minLightBrightness;
    }*/
    private void AdjustLighting(bool brighten)
    {
        float pos = 0;
        if(brighten)
        {
             pos = (_tOfDay - sunRise) / _morningLength; //get the position of the sun in the morning sky
            sunLight.intensity = maxLightBrightness * pos;
          //  Debug.Log(pos);
        }
        else
        {
             pos = (sunSet - _tOfDay) / _eveningLength; //get the position of the sun in the evening sky
            sunLight.intensity = maxLightBrightness * pos;
        }
        RenderSettings.ambientLight = new Color(NightAmbientColor.r + DayAmbientColor.r * pos,
            NightAmbientColor.g + DayAmbientColor.g * pos,
            NightAmbientColor.b + DayAmbientColor.b * pos);
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
