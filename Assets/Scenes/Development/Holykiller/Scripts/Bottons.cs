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
    private float m_sellPrice;
    private int m_itemId = 0;
    public int StacksAtm { get { return _stacksAtm; } set { _stacksAtm = value; } }
    public int MaxStacks { get { return _maxStacks; } set { _maxStacks = value; } }
    public float BuyingPrice { get { return m_buyPrice; } set { m_buyPrice = value; } }
    public float SellingPrice { get { return m_sellPrice; } set { m_sellPrice = value; } }

    public int ItemID { get { return m_itemId; } set { m_itemId = value; } }
    
    public static Bottons instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

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
    public void Amount(int stackatm, int maxstack)
    {
        _stacksAtm = stackatm;
        _maxStacks = maxstack;
        AmountOf.text = stackatm + " / " + maxstack;
    }
    public void Descrip(string s)
    {
        Description.text = s;
    }
    public void BotonInfo(Sprite sp, string nam, int satm, int maxst, string des, int Id)
    {
        this._stacksAtm = satm;
        this._maxStacks = maxst;
        this.IconOf.sprite = sp;
        this.NameOf.text = nam;
        this.AmountOf.text = satm + " / " + maxst;
        this.Description.text = des;
        this.m_itemId = Id;
    }
    public void NpcBottonInfo(Sprite ItemIcon, string ItemName, string ItemDescrip, float BuyPrice, float SellPrice, int Id)
    {
        if (IconOf.transform.gameObject.activeSelf == false)
            BEnable();
        this.IconOf.sprite = ItemIcon;
        this.NameOf.text = ItemName;
        this.Description.text = ItemDescrip;
        this.m_buyPrice = BuyPrice;
        this.AmountOf.text = "Price : $" + m_buyPrice;
        this.m_itemId = Id;
        this.m_sellPrice = SellPrice;
        this.SellPriceText.text = "Sell : $" + m_sellPrice;
    }
    public void BDisable()
    {
        this.IconOf.gameObject.SetActive(false);
        this.NameOf.gameObject.SetActive(false);
        this.Description.gameObject.SetActive(false);
        this.AmountOf.gameObject.SetActive(false);
        this.SellPriceText.gameObject.SetActive(false);
        
    }
    private void BEnable()
    {
        this.IconOf.gameObject.SetActive(true);
        this.NameOf.gameObject.SetActive(true);
        this.Description.gameObject.SetActive(true);
        this.AmountOf.gameObject.SetActive(true);
        this.SellPriceText.gameObject.SetActive(true);
    }
    public void ResetItemID()
    {
        HInventory.instance.SelectedItem.ItemID = 0;
    }
    
    public void SetValuesForSelectedItem()
    {
        HInventory.instance.SelectedItem.NpcBottonInfo(this.IconOf.sprite, this.NameOf.text, this.Description.text, this.BuyingPrice, this.SellingPrice, this.ItemID);
    }
   
}
