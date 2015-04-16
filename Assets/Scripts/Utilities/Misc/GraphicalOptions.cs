using UnityEngine;
using System.Collections;

public class GraphicalOptions : MonoBehaviour {
    
    private int anisotropicFiltering;
    private int antiAliasing;
    private int blendWeights;
    private float lodBias;
    private int textureQuality;
    private int maxLod;
    private int shadowCascades;
    private float shadowDistance;
    private bool softVegetation;
    private int vSync;    
    private int resolutionX;
    private int resolutionY;
    private bool fullscreenEnabled;

    void Start()
    {
        anisotropicFiltering = PlayerPrefs.GetInt("Anisotropic Filtering");
        antiAliasing = PlayerPrefs.GetInt("Anti Aliasing");
        blendWeights = PlayerPrefs.GetInt("Blend Weights");
        lodBias = PlayerPrefs.GetFloat("Level of Detail Bias");
        textureQuality = PlayerPrefs.GetInt("Texture Resolution");
        maxLod = PlayerPrefs.GetInt("Max Level of Detail");
        shadowCascades = PlayerPrefs.GetInt("Shadow Cascades");
        shadowDistance = PlayerPrefs.GetFloat("Shadow Distance");
        softVegetation = (PlayerPrefs.GetInt("Soft Vegetation") != 0);
        vSync = PlayerPrefs.GetInt("vSync");
        resolutionX = PlayerPrefs.GetInt("Resolution X");
        resolutionY = PlayerPrefs.GetInt("Resolution Y");
        fullscreenEnabled = (PlayerPrefs.GetInt("Fullscreen") != 0);


    }

    /// <summary>
    /// Texture correction
    /// </summary>
    /// <param name="filtering"></param>
    public void SetFiltering(int filtering) // Anisotripic Filtering
    {
        // 0 = Off
        // 1 = On
        // 2 = Forced All Textures On
        switch (filtering)
        {
            case 0:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
                break;
            case 1:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
                break;
            case 2:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                break;
        }
        PlayerPrefs.SetInt("Anisotropic Filtering", filtering);
    }

    /// <summary>
    /// Edge Smoothing
    /// </summary>
    /// <param name="aliasing"></param>
    public void SetAliasing(int aliasing) // Anti Aliasing
    {
        // 0 = Off
        // 2 = 2xMSAA
        // 4 = 4xMSAA
        // 6 = 8xMSAA
        QualitySettings.antiAliasing = aliasing;
        PlayerPrefs.SetInt("Anti Aliasing", aliasing);
    }

    /// <summary>
    /// How many bones affect each vertex
    /// </summary>
    /// <param name="weights"></param>
    public void SetBlendWeights(int weights) // Blend Weights
    {
        // 1 = One Bone
        // 2 = Two Bones
        // 4 = Four Bones
        switch (weights)
        {
            case 1:
                QualitySettings.blendWeights = BlendWeights.OneBone;
                break;
            case 2:
                QualitySettings.blendWeights = BlendWeights.TwoBones;
                break;
            case 4:
                QualitySettings.blendWeights = BlendWeights.FourBones;
                break;
        }
        PlayerPrefs.SetInt("Blend Weights", weights);
    }

    /// <summary>
    /// How far away an object needs to be before it changes to a lower level of detail model
    /// </summary>
    /// <param name="bias"></param>
    public void SetLODBias(float bias) // Level of Detail Bias
    {
        QualitySettings.lodBias = bias;
        PlayerPrefs.SetFloat("Level of Detail Bias", bias);
    }

    /// <summary>
    /// Resolution of the textures
    /// </summary>
    /// <param name="quality"></param>
    public void SetTextureQuality(int quality) // Texeture Quality
    {
        // 0 = Full Texture Resolution
        // 1 = Half
        // 2 = Quarter
        // 3 = Eigth
        QualitySettings.masterTextureLimit = quality;
        PlayerPrefs.SetInt("Texture Resolution", quality);
    }

    /// <summary>
    /// Maxiumum level of detail changes
    /// </summary>
    /// <param name="lod"></param>
    public void SetLOD(int lod) // Max Level of Detail
    {
        // 1-7 Max Level of Detail
        QualitySettings.maximumLODLevel = lod;
        PlayerPrefs.SetInt("Max Level of Detail", lod);
    }

    /// <summary>
    /// Shadow edge smoothing
    /// </summary>
    /// <param name="cascades"></param>
    public void SetShadowCascades(int cascades) // Shadow Cascades
    {
        // 0 = Low
        // 1 = Medium
        // 2 = High
        QualitySettings.shadowCascades = cascades;
        PlayerPrefs.SetInt("Shadow Cascades", cascades);
    }

    /// <summary>
    /// How far away the player needs to be until the shadows are not rendered
    /// </summary>
    /// <param name="distance"></param>
    public void SetShadowDistance(float distance) // Shadow Render Distance
    {
        QualitySettings.shadowDistance = distance;
        PlayerPrefs.SetFloat("Shadow Distance", distance);
    }

    /// <summary>
    /// Smooth the edges around terrain vegetation
    /// </summary>
    /// <param name="enabled"></param>
    public void SetVegetation(bool enabled) // Soft Vegetation
    {
        QualitySettings.softVegetation = enabled;
        PlayerPrefs.SetInt("Soft Vegetation", (enabled ? 1 : 0));
    }

    /// <summary>
    /// Lock game frame rate to the user's monitor speed
    /// </summary>
    /// <param name="vSync"></param>
    public void SetvSync(int vSync) // Vertical Synchronization 
    {
        // 0 = vSync off
        // 1 = Sync Monitor
        // 2 = Every 2 Updates (This one will most likely not be used)
        QualitySettings.vSyncCount = vSync;
        PlayerPrefs.SetInt("vSync", vSync);
    }

    /// <summary>
    /// Changes the overall quality
    /// </summary>
    /// <param name="expensiveChanges"></param>
    /// <param name="level"></param>
    public void SetQualityLevel(bool expensiveChanges, int level)
    {
        // 0-5
        QualitySettings.SetQualityLevel(level, expensiveChanges);
    }

    /// <summary>
    /// Set the screen resolution
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SetResolution(int x, int y)
    {
        resolutionX = x;
        resolutionY = y;
        Screen.SetResolution(x, y, fullscreenEnabled);
        PlayerPrefs.SetInt("Resolution X", x);
        PlayerPrefs.SetInt("Resolution Y", y);
    }

    /// <summary>
    /// Change fullscreen mode
    /// </summary>
    /// <param name="fullscreen"></param>
    public void SetFullscreen(bool fullscreen)
    {
        fullscreenEnabled = fullscreen;
        Screen.SetResolution(resolutionX, resolutionY, fullscreen);
        PlayerPrefs.SetInt("Fullscreen", (fullscreen ? 1 : 0));
    }
}
