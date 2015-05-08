using UnityEngine;
using UnityEngine.UI;

public class Bottons : MonoBehaviour
{
    [SerializeField]
    protected Image IconOf;
    [SerializeField]
    protected Text NameOf;
    [SerializeField]
    protected Text AmountOf;
    [SerializeField]
    protected Text Description;
    [SerializeField]
    private Text SellPriceText;

    private int _stacksAtm;
    private int _maxStacks;
    private float m_buyPrice;
    private int m_itemId;
    public int StacksAtm { get { return _stacksAtm; } set { _stacksAtm = value; } }
    public int MaxStacks { get { return _maxStacks; } set { _maxStacks = value; } }
    public float BuyingPrice { get { return m_buyPrice; } set { m_buyPrice = value; } }

    public int ItemID { get { return m_itemId; } set { m_itemId = value; } }

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
    public void BotonInfo(Sprite sp,string nam,int satm,int maxst,string des,int Id)
    {
        _stacksAtm = satm;
        _maxStacks = maxst;
        IconOf.sprite = sp;
        NameOf.text = nam;
        AmountOf.text = satm + " / " + maxst;
        Description.text = des;
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
