using UnityEngine;
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
/// Explecit = direct reference and calls (non event)
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

    public ProgressBar hp;
    public ProgressBar pp;
    //Lvl1
    //Exp Bar
    //Pokemon Image
    //BattleSlots(create placeholders)


    private Trainer trainer;
    // Use this for initialization
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
    
    void SetIcon()
    {

    }

    void SetProgressBars()


    }


    void OnMoveCast(int slotID)
    {
        //ToDO for loop to slotID in moveslots and cast useMOve
    }
    // Update is called once per frame
    void Update()
    {
       // if(target!=null)
            //Keeepupdating the values

    }
}
