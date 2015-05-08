using UnityEngine;
using UnityEngine.UI;

public class Bottons : MonoBehaviour
{
    [SerializeField]
    private Image IconOf;
    [SerializeField]
    private Text NameOf;
    [SerializeField]
    private Text AmountOf;
    [SerializeField]
    private Text Description;

    private int _stacksAtm;
    private int _maxStacks;
    public int StacksAtm { get { return _stacksAtm; } set { _stacksAtm = value; } }
    public int MaxStacks { get { return _maxStacks; } set { _maxStacks = value; } }
 

    void OnEnable()
    {
        BottonsManager.instance.AddBoton(this);        
    }
    void OnDisable()
    {
        BottonsManager.instance.RemoveBoton(this);
    }

    public void IconIs(Sprite s)
    {
        IconOf.sprite = s;
    }
    public void Name(string s)
    {
        NameOf.text = s;
    }
    public string TheName()
    {
        return NameOf.text;
    }
    public void Amount(int stackatm,int maxstack)
    {
        _stacksAtm = stackatm;
        _maxStacks = maxstack;
        AmountOf.text = stackatm + " / " + maxstack;
    }
    public void Descrip(string s)
    {
        Description.text = s;
    }
    public void BotonInfo(Sprite sp,string nam,int satm,int maxst,string des)
    {
        _stacksAtm = satm;
        _maxStacks = maxst;
        IconOf.sprite = sp;
        NameOf.text = nam;
        AmountOf.text = satm + " / " + maxst;
        Description.text = des;
<<<<<<< HEAD:Assets/Scenes/Development/Holykiller/Bottons.cs
=======
        m_itemId = Id;
    }
    public void NpcBottonInfo(Sprite ItemIcon, string ItemName, string ItemDescrip, float BuyPrice,int Id)
    {
        if (IconOf.gameObject.activeSelf == false)
            NPCBootonEnable();
        IconOf.sprite = ItemIcon;
        NameOf.text = ItemName;
        Description.text = ItemDescrip;
        m_buyPrice = BuyPrice;
        AmountOf.text = "Price : $" + m_buyPrice;
        m_itemId = Id;
>>>>>>> 3030ad2c3f654391e94dd701d725f21056143795:Assets/Scenes/Development/Holykiller/Scripts/Bottons.cs
    }
    public void NPCBootonDisable()
    {
        IconOf.gameObject.SetActive(false);
        NameOf.gameObject.SetActive(false);
        Description.gameObject.SetActive(false);
        //m_buyPrice = 0;
        AmountOf.gameObject.SetActive(false);
        SellPriceText.gameObject.SetActive(false);
        m_itemId = 0;
    }
    public void NPCBootonEnable()
    {
        IconOf.gameObject.SetActive(true);
        NameOf.gameObject.SetActive(true);
        Description.gameObject.SetActive(true);
        //m_buyPrice = 0;
        AmountOf.gameObject.SetActive(true);
        SellPriceText.gameObject.SetActive(true);
        m_itemId = 0;
    }
}
