using UnityEngine;
using System.Collections;

public class PlayerBattlePanel : MonoBehaviour {

    public UISprite[] icon; // Make this 0-4, 0 is the auto attack icon and the other 4 are the spell icons
    public UILabel Name;

    public UILabel hpLabel;
    public GameObject[] hpDisplay; // Make this 0-2, 0 = Green, 1 = Yellow, 2 = Red
    public UILabel ppLabel;
    public UISprite ppDisplay;
    public UILabel expLabel;
    public UISprite expDisplay;

    private int hpColorSelection = 0;

    void Start()
    {
        UpdateStats(300, 573, 279, 1783, 340, 734); // Temporary setters.  This will be called in a HP/PP/EXP Calculation script once battle mechanics are implemented
        SetName("Fennekin");                       // The name will also be taken from player data
        SetIcon(3, "PLACEHOLDER ICON 3");
    }

    void RefreshHP(float hp, float maxHP)
    {        
        float hpDecimal = hp / maxHP;
        hpLabel.text = (hp.ToString() + "/" + maxHP.ToString());
        
        foreach(GameObject colorDisplay in hpDisplay)          // Disable each display
        {
            colorDisplay.SetActive(false);
        }
        if (hpDecimal <= 0.2) { hpColorSelection = 2; }                          // Select which color display to set the HP bar to based on how much health
        else if (hpDecimal > 0.2 && hpDecimal <= 0.5) { hpColorSelection = 1; }  // the player has
        else if (hpDecimal > 0.5) { hpColorSelection = 0; }

        hpDisplay[hpColorSelection].GetComponent<UISprite>().fillAmount = hpDecimal;
        hpDisplay[hpColorSelection].SetActive(true);       
    }

    void RefreshPP(float pp, float maxPP)
    {
        float ppDecimal = pp / maxPP;
        ppLabel.text = (pp.ToString() + "/" + maxPP.ToString());
        ppDisplay.fillAmount = ppDecimal;
    }

    void RefreshEXP(float exp, float maxEXP)
    {
        float expDecimal = exp / maxEXP;
        expLabel.text = (exp.ToString() + "/" + maxEXP.ToString());
        expDisplay.fillAmount = expDecimal;
    }

    void UpdateStats(float hp, float maxHP, float exp, float maxEXP, float pp, float maxPP)
    {
        RefreshHP(hp, maxHP);
        RefreshEXP(exp, maxEXP);
        RefreshPP(pp, maxPP);
    }

    void SetName(string pokeName)
    {
        Name.text = pokeName;
    }

    void SetIcon(int slot, string iconName)
    {
        icon[slot].spriteName = iconName;
    }

}
