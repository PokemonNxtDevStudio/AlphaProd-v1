using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// - Bind this class to a target pokemon
/// listens to events only performed by the trainer
/// 
/// 
/// 1) Import in metals battle UI component into the correct folder. 
/// 2) Create battle panel using images
/// 3) Either Inherit UIWindow or have it as seperate component
/// 4) Have a test script that assigns all the values explicity //Create a test class to take inputs and cause damage or whatever to show on screen
/// Explicit = direct reference and calls (non event)
/// 5) Bind HP/PP/XP bars to pokemon
/// 6) Bind icon to image
/// 7) Add poke title
/// 8) Create move slots and save them as prefabs so they can be reused( not dragable)
/// 9) Once done, we shall try to convert it into implicit
/// 10) Start should be something only to cache in references
/// 11) Polish if you like with tweens
/// Implicit = Battle panel has no connection with any oother class. It just listens to changes around it .(Events)
/// </summary>
public class BattlePanel : MonoBehaviour
{

    public List<MoveSlot> moveSlots;

    public Pokemon target;

    public Image icon;
    public Image hpBar;
    public Text hpText;
    public Image ppBar;
    public Text ppText;
    public Image expBar;
    //Lvl1
    //Exp Bar
    //Pokemon Image
    //BattleSlots(create placeholders)


    private Trainer trainer;

    void Start()
    {
        
    }
    
    public void Init()
    {

    }
    void OnReleasePokemon(Pokemon target)
    {
        this.target = target;
    }
    
    /// <summary>
    /// Update Icon
    /// </summary>
    /// <param name="sprite"></param>
    public void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }

    public void SetMoves(MoveData moveData) // TODO Set move icon based on which move it is
    {

    }

    public void SetItems() // Set items the same as moves ^^
    {

    }

    /// <summary>
    /// Update HP and change the bar's color based on health percentage
    /// </summary>
    /// <param name="newHP"></param>
    /// <param name="maxHP"></param>
    public void UpdateHP(float currentHP, float maxHP)
    {
        float dec = currentHP / maxHP;
        Debug.Log(dec);
        hpBar.fillAmount = dec;
        hpText.text = currentHP + "/" + maxHP;
        if (dec > 0.5f) // Green
        {
            hpBar.color = Color.green;
        }
        else if (dec <= 0.5f && dec > 0.2f) // Yellow
        {
            hpBar.color = Color.yellow;
        }
        else if (dec <= 0.2f) // Red
        {
            hpBar.color = Color.red;
        }
    }

    /// <summary>
    /// Update PP
    /// </summary>
    /// <param name="currentPP"></param>
    /// <param name="maxPP"></param>
    public void UpdatePP(float currentPP, float maxPP)
    {
        float dec = currentPP / maxPP;
        ppBar.fillAmount = dec;
        ppText.text = currentPP + "/" + maxPP;
    }

    /// <summary>
    /// Update Exp
    /// </summary>
    /// <param name="currentExp"></param>
    /// <param name="maxExp"></param>
    public void UpdateEXP(float currentExp, float maxExp)
    {
        float dec = currentExp / maxExp;
        expBar.fillAmount = dec;
    }

    void OnMoveCast(int slotID)
    {
        //TODO for loop to slotID in moveslots and cast useMove
    }

    void Update()
    {
       // if(target!=null)
            //Keeepupdating the values

    }
}
