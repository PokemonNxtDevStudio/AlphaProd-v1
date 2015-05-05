using UnityEngine;
using UnityEngine.UI;

public class Botons : MonoBehaviour
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
        BotonsManager.instance.AddBoton(this);
    }
    void OnDisable()
    {
        BotonsManager.instance.RemoveBoton(this);
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
    }
}
