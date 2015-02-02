using UnityEngine;
using System.Collections;

public class EnemyBattlePanel : MonoBehaviour {

    public GameObject name;
    public GameObject icon;
    public GameObject hpLabel;
    public GameObject hpDisplay;

    void Start()
    {
        RefreshHP(375, 912);
        SetName("Wailord");
        SetIcon("PLACEHOLDER ICON 4");
    }

    void RefreshHP(float hp, float maxHP)
    {
        float hpDecimal = hp / maxHP;
        hpLabel.GetComponent<UILabel>().text = (hp.ToString() + "/" + maxHP.ToString());
        hpDisplay.GetComponent<UISprite>().fillAmount = hpDecimal;
    }

    void SetName(string pokeName)
    {
        name.GetComponent<UILabel>().text = pokeName;
    }

    void SetIcon(string iconName)
    {
        icon.GetComponent<UISprite>().spriteName = iconName;
    }
}
