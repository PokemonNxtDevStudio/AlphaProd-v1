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
    private int m_itemId;
    public int StacksAtm { get { return _stacksAtm; } set { _stacksAtm = value; } }
    public int MaxStacks { get { return _maxStacks; } set { _maxStacks = value; } }
    public float BuyingPrice { get { return m_buyPrice; } set { m_buyPrice = value; } }
    public float SellingPrice { get { return m_sellPrice; } set { m_sellPrice = value; } }

    public int ItemID { get { return m_itemId; } set { m_itemId = value; } }
    [SerializeField]
    protected Bottons SelectedItemBotton;

    public static Bottons instance;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        for (int i = 0; i < BottonsManager.instance._bottons.Count; i++)
        {
            if (BottonsManager.instance._bottons[i].gameObject.name == "NPCItemSelected")
            {
                SelectedItemBotton = BottonsManager.instance._bottons[i];
                SelectedItemBotton.BDisable();
                return;
            }
        }

        //NPCBottons = transform.parent.parent.parent.GetComponent<HInventory>();
        /*  if(m_SelectedItem == null)
          {
              gameObject.transform.parent.GetChild(1).GetComponent<NPCBottons>();
          }*/
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
        _stacksAtm = satm;
        _maxStacks = maxst;
        IconOf.sprite = sp;
        NameOf.text = nam;
        AmountOf.text = satm + " / " + maxst;
        Description.text = des;
        m_itemId = Id;
    }
    public void NpcBottonInfo(Sprite ItemIcon, string ItemName, string ItemDescrip, float BuyPrice, float SellPrice, int Id)
    {
        if (IconOf.transform.gameObject.activeSelf == false)
            BEnable();
        IconOf.sprite = ItemIcon;
        NameOf.text = ItemName;
        Description.text = ItemDescrip;
        m_buyPrice = BuyPrice;
        AmountOf.text = "Price : $" + m_buyPrice;
        m_itemId = Id;
        m_sellPrice = SellPrice;
        SellPriceText.text = "Sell : $" + m_sellPrice;
    }
    public void BDisable()
    {
        IconOf.gameObject.SetActive(false);
        NameOf.gameObject.SetActive(false);
        Description.gameObject.SetActive(false);
        AmountOf.gameObject.SetActive(false);
        SellPriceText.gameObject.SetActive(false);
        m_itemId = 0;
    }
    private void BEnable()
    {
        IconOf.gameObject.SetActive(true);
        NameOf.gameObject.SetActive(true);
        Description.gameObject.SetActive(true);
        AmountOf.gameObject.SetActive(true);
        SellPriceText.gameObject.SetActive(true);
    }
    public void SelectedItemHideInfo()
    {
        SelectedItemBotton.BDisable();
    }
    public void SetValuesForSelectedItem()
    {
        SelectedItemBotton.NpcBottonInfo(this.IconOf.sprite, this.NameOf.text, this.Description.text, this.BuyingPrice, this.SellingPrice, this.ItemID);
    }
}
