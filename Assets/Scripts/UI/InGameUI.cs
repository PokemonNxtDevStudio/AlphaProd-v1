using UnityEngine;
using System.Collections;

public class InGameUI : UIWindow
{
    public BattlePanel instance;
    float maxHp = 500;
    float currentHp = 500;
    float maxPp = 500;
    float currentPp = 500;
    float currentExp = 50;
    float maxExp = 500;

	void Start () 
    {
	}
	
	void Update () 
    {
        if (cInput.GetKeyDown("Use"))
        {
            instance.UpdateHP(currentHp, maxHp);
            instance.UpdatePP(currentPp, maxPp);
            instance.UpdateEXP(currentExp, maxExp);
            currentHp -= 20;
            currentPp -= 50;
            currentExp += 50;
        }
	}
}
