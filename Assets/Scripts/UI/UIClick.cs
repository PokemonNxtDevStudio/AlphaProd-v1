using UnityEngine;
using System.Collections;

public class UIClick : MonoBehaviour {

    public bool boy;
    public Animator playerAnimator;
    public GameObject instructionText;
    public GameObject confirmationText;
    public GameObject UIPanels;
    public GameObject triggerPanel;
    public GameObject oppositePanel;

    private bool actionAble = true;

    public void ClickUI()
    {
        playerAnimator.SetFloat("Input_Y", 1.0f);
        instructionText.active = false;
        confirmationText.active = true;
        if (boy)
        {
            confirmationText.transform.FindChild("Label_Girl").active = false;
            confirmationText.transform.FindChild("Label_Boy").active = true;
        }
        else if (!boy)
        {
            confirmationText.transform.FindChild("Label_Boy").active = false;
            confirmationText.transform.FindChild("Label_Girl").active = true;
        }   
    }

    public void DeclineGender()
    {
        playerAnimator.SetFloat("Input_Y", 0.0f);
        instructionText.active = true;
        confirmationText.active = false;
    }

    public void EnablePanels()
    {
        UIPanels.transform.FindChild("PanelRight").active = true;
        UIPanels.transform.FindChild("PanelLeft").active = true;
    }

    public void DisablePanel()
    {
        oppositePanel.active = false;
    }

    public void DisableButtons()
    {
        confirmationText.transform.FindChild("Confirm Button").FindChild("Button").active = false;
        confirmationText.transform.FindChild("Decline Button").FindChild("ButtonBoy").active = false;
        confirmationText.transform.FindChild("Decline Button").FindChild("ButtonGirl").active = false;
    }

    public void EnableButtons()
    {
        confirmationText.transform.FindChild("Confirm Button").FindChild("Button").active = true;
        confirmationText.transform.FindChild("Decline Button").FindChild("ButtonBoy").active = true;
        confirmationText.transform.FindChild("Decline Button").FindChild("ButtonGirl").active = true;
    }

    public void EnableTriggerPanel()
    {
        triggerPanel.active = true;
    }

    public void DisableTriggerPanel()
    {
        triggerPanel.active = false;
    }

    public void ChooseGirl()
    {
        confirmationText.transform.FindChild("Decline Button").FindChild("ButtonBoy").active = false;
    }

    public void ChooseBoy()
    {
        confirmationText.transform.FindChild("Decline Button").FindChild("ButtonGirl").active = false;
    }
}
