using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GPokeUi : MonoBehaviour
{
    [SerializeField]
    private GameObject SelectedUI;
    [SerializeField]
    private Text m_hp;
    [SerializeField]
    private Text m_pp;
    [SerializeField]
    private Image m_sprite;
	
	void Start () 
    {/*
        m_sprite = gameObject.transform.GetComponent<Image>();
        SelectedUI = gameObject.transform.GetChild(0).gameObject;
        m_hp = gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        m_pp = gameObject.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();*/
        SelectedUI.SetActive(false);
	}
	
	public void SelectedisThis(bool itsthis)
    {
        if(itsthis == true)
        {
            SelectedUI.SetActive(true);
        }
        else
        {
            SelectedUI.SetActive(false);
        }
    }
    public void SetValues(Pokemon poke)
    {
        if(poke == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            m_hp.text = "Hp: " + poke.CurrentHealth.ToString();
            m_pp.text = "PP: " + poke.currentPP.ToString();
            m_sprite.sprite = poke.Icon;
        }
    }
}
