using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PokePartyFrame : MonoBehaviour
{
    public GameObject partyPanel;
    public GameObject[] partyFrame;
    public GameObject[] partyFrameSelector;
    public GameObject[] partyFrameIcon;
    public GameObject[] partyNameLabel;
    public GameObject[] partyLevelLabel;
    public GameObject[] faintedIconTint;

    //private PokeParty pokeParty;                   ****************************** UNCOMMENT WHEN POKEPARTY IS MOVED INTO THE PRODUCTION REPOSITORY ***************

    //void Start()
    //{
    //    pokeParty = GameObject.Find("PlayerTrainer").GetComponent<Trainer>().pokeParty;
    //    Reload();
    //}
    //void Reload()
    //{
    //    foreach (GameObject frame in partyFrame)
    //    {
    //        frame.SetActive(false);
    //    }
    //    if (pokeParty.HasPokemon())
    //    {

    //        for (int i = 0; i < pokeParty.SlotCount(); i++ )
    //        {
    //            SetPokeFrame(pokeParty.GetPokeSlotName(i), pokeParty.GetPokeSlotIcon(i), pokeParty.GetPokeSlotLevel(i), i);
    //            partyFrame[i].SetActive(true);
    //        }            
    //    }
    //}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { SetSelection(0); }        // To be changed later with cInput
        else if (Input.GetKeyDown(KeyCode.Alpha2)) { SetSelection(1); }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) { SetSelection(2); }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) { SetSelection(3); }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) { SetSelection(4); }
        else if (Input.GetKeyDown(KeyCode.Alpha6)) { SetSelection(5); }

    }

    #region 
    /// <summary>
    /// Sets the visual selector on the party panel
    /// </summary>
    /// <param name="Frame Number (0-5)"></param>
    void SetSelection(int selection)
    {
        if (!faintedIconTint[selection].activeSelf) // If the pokemon is fainted, the slot will not change
        {
            if (selection > -1 && selection < 6)
            {
                foreach (GameObject selectionOverlays in partyFrameSelector)
                {
                    if (selectionOverlays != null)
                    {
                        selectionOverlays.SetActive(false);  // Set all overlays to false before enabling the selected frame's overlay
                    }
                }
                if (partyFrameSelector[selection] != null)
                {
                    partyFrameSelector[selection].SetActive(true); // Enable the overlay
                }
                else
                {
                    Debug.LogError("The array, partyFrameSelector does not have the required " + selection + "th variable, please add this");
                }
            }
            else
            {
                Debug.LogError("PokePartyPanel selection was set to " + selection + ", The panel number must be an integer between 0 and 5");
            }
        }
    }

    /// <summary>
    /// Changes the icon on the party panel
    /// </summary>
    /// <param name="Atlas Sprite Name"></param>
    /// <param name="Frame Number (0-5)"></param>
    void SetIcon(string spriteName, int frame)
    {
        if (partyFrameIcon[frame] != null)
        {
            if (frame > -1 && frame < 6)
            {
//                partyFrameIcon[frame].GetComponent<UISprite>().spriteName = spriteName; // Change the pokemon's icon           
            }
            else
            {
                Debug.LogError("You are trying to change the icon on the panel number " + frame + ", this panel number needs to be an integer between 0 and 5");
            }
        }
    }

    /// <summary>
    /// Changes the name on the party panel
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Frame Number (0-5)"></param>
    void SetName(string name, int frame)
    {
        if (partyNameLabel != null)
        {
            if (frame > -1 && frame < 6)
            {
               // partyNameLabel[frame].GetComponent<UILabel>().text = name; // Change the pokemon's name
            }
            else
            {
                //Debug.LogError("You are trying to change the name on the panel number " + frame + ", this panel number needs to be an integer between 0 and 5");
            }
        }
    }

    /// <summary>
    /// Set the level label on the party panel
    /// </summary>
    /// <param name="Pokemon's Level"></param>
    /// <param name="Frame Number (0-5)"></param>
    void SetLevel(int level, int frame)
    {
        if (partyLevelLabel != null)
        {
            if (frame > -1 && frame < 6)
            {
              //  partyLevelLabel[frame].GetComponent<UILabel>().text = level.ToString();  // Change the pokemon's level
            }
            else
            {
               // Debug.LogError("You are trying to change the Pokemon level on the panel number " + frame + ", this panel number needs to be an integer between 0 and 5");
            }
        }
    }

    /// <summary>
    /// Sets the Pokemon's faint status on the PokeParty
    /// </summary>
    /// <param name="Fainted"></param>
    /// <param name="Frame Number (0-5)"></param>
    void SetFainted(bool fainted, int frame)
    {
        if (faintedIconTint[frame] != null)
        {
            if (partyLevelLabel[frame] != null)
            {
                if (partyNameLabel[frame] != null)
                {
                    if (frame > -1 && frame < 6)
                    {
                        if (fainted)
                        {
                            faintedIconTint[frame].SetActive(true); // Enable the fainted tint
                            partyLevelLabel[frame].SetActive(false); // Disable the level text
                            partyNameLabel[frame].SetActive(false); // Disable the name   TODO:  Make it say "Fainted" instead of completley clearing the label
                        }
                        else
                        {
                            faintedIconTint[frame].SetActive(false); // Disable the fainted tint
                            partyLevelLabel[frame].SetActive(true); // Enable the level text
                            partyNameLabel[frame].SetActive(true); // Enable the name   TODO:  Make it say "Fainted" instead of completley clearing the label
                        }
                    }
                    else
                    {
                        Debug.LogError("You are trying to change the Pokemon's faint status on the panel number " + frame + ", this panel number needs to be an integer between 0 and 5");
                    }
                }
            }
        }
    }

    /// <summary>
    /// Sets multiple parts in the PokeParty
    /// </summary>
    /// <param name="Pokemon Name"></param>
    /// <param name="Pokemon Icon"></param>
    /// <param name="Pokemon Level"></param>
    /// <param name="Frame Number (0-5)"></param>
    void SetPokeFrame(string pokeName, string pokeIcon, int pokeLevel, int frame)
    {
        SetIcon(pokeIcon, frame);
        SetName(pokeName, frame);
        SetLevel(pokeLevel, frame);
    }

    #endregion

    public void Enable(float time)
    {
      //  TweenAlpha.Begin(gameObject, time, 1);  
    }

    public void Disable(float time) 
    {
       // TweenAlpha.Begin(gameObject, time, 0);
    }
}
