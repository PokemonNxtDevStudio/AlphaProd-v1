using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCBottons : Bottons
{
    //[SerializeField]
    //private Text BuyPrice;
    [SerializeField]
    private NPCBottons MainInventory;

    void Start()
    {
        MainInventory = transform.parent.parent.transform.GetChild(1).GetComponent<NPCBottons>();
      /*  if(m_SelectedItem == null)
        {
            gameObject.transform.parent.GetChild(1).GetComponent<NPCBottons>();
        }*/
    }   

    public void SetValuesForSelectedItem()
    {
        //if (IconOf.gameObject.activeSelf == false)
            //NPCBootonEnable();
        MainInventory.NpcBottonInfo(this.IconOf.sprite, this.NameOf.text, this.Description.text, this.BuyingPrice, this.ItemID);
    }




}
