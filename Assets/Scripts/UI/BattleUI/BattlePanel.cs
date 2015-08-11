using UnityEngine;
using System.Collections;
using System.Collections.Generic;



/// <summary>
/// - Bind this class to a target pokemon
/// listens to events only performed by the trainer
/// 
/// </summary>
public class BattlePanel : MonoBehaviour
{


    public List<MoveSlot> moveSlots;

    public Pokemon target;

    public ProgressBar hp;
    public ProgressBar pp;



    private Trainer trainer;
    // Use this for initialization
    void Start()
    {

    }

    void OnReleasePokemon(Pokemon target)
    {
        this.target = target;
    }
    

    void OnMoveCast(int slotID)
    {
        //ToDO for loop to slotID in moveslots and cast useMOve
    }
    // Update is called once per frame
    void Update()
    {

    }
}
