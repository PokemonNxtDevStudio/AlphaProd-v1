using UnityEngine;
using System.Collections;

public class Underwater : MonoBehaviour 
{
	public Color fogColor;
	public float maxfogdensity = 0.1f;
	
	
	private bool savedfogEnableFlag;
	private Color savedFogColor;
	private float savedFogdensty;
	private Material savedSkyboxMaterial;
	
	private bool isUnderWater = false;
	
    private GameObject Water;
	
	void Start () 
	{
		GetComponent<Camera>().backgroundColor = fogColor;
		
		savedfogEnableFlag = RenderSettings.fog;
		savedFogColor = RenderSettings.fogColor;
		savedFogdensty = RenderSettings.fogDensity;
		savedSkyboxMaterial = RenderSettings.skybox;
		
		
	
	}
	
	
	void Update () 
	{
		if(Water != null && isUnderWater == false)
		{
            
			RenderSettings.fog = true;
			RenderSettings.fogColor = fogColor;
			RenderSettings.fogDensity = maxfogdensity;
			RenderSettings.skybox = null;
			isUnderWater = true;
			
			
		}
		if(Water == null && isUnderWater == true)
		{
            
			RenderSettings.fog = savedfogEnableFlag;
			RenderSettings.fogColor = savedFogColor;
			RenderSettings.fogDensity = savedFogdensty;
			RenderSettings.skybox = savedSkyboxMaterial;
				
			isUnderWater = false;
			
		}
	
	}

    void OnTriggerEnter(Collider other)
    {
    
        if(other.tag == "Water")
        {
            Water = other.gameObject;            
        }
    }
   
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Water")
        {
            Water = null;
        }
    }
}
