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
        //NPCBottons = transform.parent.parent.parent.GetComponent<HInventory>();
      /*  if(m_SelectedItem == null)
        {
            gameObject.transform.parent.GetChild(1).GetComponent<NPCBottons>();
        }*/
    }   

    public void SetValuesForSelectedItem()
    {
        //MainInventory.SelectedItem.NpcBottonInfo(this.IconOf.sprite, this.NameOf.text, this.Description.text, this.BuyingPrice, this.ItemID);
    }




}
