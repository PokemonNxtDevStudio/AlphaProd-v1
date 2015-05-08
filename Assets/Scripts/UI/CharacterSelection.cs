using UnityEngine;
using System.Collections;

public class CharacterSelection : MonoBehaviour {

    public bool boy;
    public GameObject instructionText;
    public GameObject confirmationText;
    public GameObject UIPanels;
    public GameObject triggerPanel;
    public GameObject oppositePanel;

    public void ClickUI()
    {
        instructionText.SetActive(false);
        confirmationText.SetActive(true);
        if (boy)
        {
            confirmationText.transform.FindChild("Label_Girl").gameObject.SetActive(false);
            confirmationText.transform.FindChild("Label_Boy").gameObject.SetActive(true);
        }
        else if (!boy)
        {
            confirmationText.transform.FindChild("Label_Boy").gameObject.SetActive(false);
            confirmationText.transform.FindChild("Label_Girl").gameObject.SetActive(true);
        }   
    }

    public void DeclineGender()
    {
        instructionText.SetActive(true);
        confirmationText.SetActive(false);
    }

    public void EnablePanels()
    {
        UIPanels.transform.FindChild("PanelRight").gameObject.SetActive(true);
        UIPanels.transform.FindChild("PanelLeft").gameObject.SetActive(true);
        oppositePanel.SetActive(true);
        triggerPanel.SetActive(true);
    }

    public void DisablePanel()
    {
        oppositePanel.SetActive(false);
    }

    public void EnablePanel()
    {
        oppositePanel.SetActive(false);
        triggerPanel.SetActive(true);
    }

    public void DisableButtons()
    {
        confirmationText.transform.FindChild("Decline Button").FindChild("ButtonBoy").gameObject.SetActive(false);
        confirmationText.transform.FindChild("Decline Button").FindChild("ButtonGirl").gameObject.SetActive(false);
    }

    public void EnableButtons()
    {
        confirmationText.transform.FindChild("Decline Button").FindChild("ButtonBoy").gameObject.SetActive(true);
        confirmationText.transform.FindChild("Decline Button").FindChild("ButtonGirl").gameObject.SetActive(true);
    }

    public void EnableTriggerPanel()
    {
        triggerPanel.SetActive(true);
    }

    public void DisableTriggerPanel()
    {
        triggerPanel.SetActive(false);
    }

    public void ChooseGirl()
    {
        confirmationText.transform.FindChild("Decline Button").FindChild("ButtonBoy").gameObject.SetActive(false);
        confirmationText.transform.FindChild("Decline Button").FindChild("ButtonGirl").gameObject.SetActive(true);
    }

    public void ChooseBoy()
    {
        confirmationText.transform.FindChild("Decline Button").FindChild("ButtonGirl").gameObject.SetActive(false);
        confirmationText.transform.FindChild("Decline Button").FindChild("ButtonBoy").gameObject.SetActive(true);
    }
}
