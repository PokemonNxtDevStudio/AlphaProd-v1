using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using NXT.Inventory;

public class NPCStore : MonoBehaviour
{
    //[HideInInspector]
    //public GameObject Inventory;
    [SerializeField]
    private GameObject m_StoreItemsBotton;
    [SerializeField]
    private GameObject StoreUI;
    [SerializeField]
    private GameObject StoreItemsPanel;
    [SerializeField]
    private GameObject InteractUI;
   // [SerializeField]
    //private ItemAssetDatabase db;

    private int m_storeId;
    public int StoreID { get { return m_storeId; } set { m_storeId = value; } }


    private List<InventoryItem> m_storeItems;

    [SerializeField]
    private List<int> m_IDsOfItemsInStore;


    private bool m_canBeDisplay = false;

    private List<Bottons> ItemsInStore = new List<Bottons>();
    private List<GameObject> ItemsInStoreGameObject = new List<GameObject>();
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        NXT.EventHandler.RegisterEvent(player, "OnShopRequest", new Action(OnShopRequest));
		//checking to make items are assigned	
        if (m_StoreItemsBotton == null)
        {
            Debug.Log("Add Store Item Botton to " + gameObject.name);
            return;
        }
        if (StoreUI == null)
        {
            Debug.Log("Add Store UI GameObject to " + gameObject.name);
            return;
        }
        if (StoreItemsPanel == null)
        {
            Debug.Log("Add Store Items GameObject to " + gameObject.name);
            return;
        }
        if(InteractUI == null)
        {
            Debug.Log("Add InteractUI GameObject to " + gameObject.name);
            return;
        }

        if(InteractUI.activeSelf) {
            InteractUI.SetActive(false);
        }
        for (int i = 0; i < StoreItemsPanel.transform.childCount; i++)
        {
            ItemsInStore.Add(StoreItemsPanel.transform.GetChild(i).GetComponent<Bottons>());
            ItemsInStoreGameObject.Add(StoreItemsPanel.transform.GetChild(i).transform.gameObject);
        }
    }
    private void OnShopRequest()
    {
        if (m_canBeDisplay == true)
        {
            NxtUiManager.instance.InventoryUI.SetActive(true);
            //HInventory.instance.InventoryUI.SetActive(true);
            //Inventory.SetActive(true);            
            StoreUI.SetActive(!StoreUI.gameObject.activeSelf);
            if (StoreUI.activeSelf == true)
            {
                InteractUI.SetActive(false);
                AddItemsToTheStore();
                NxtUiManager.instance.HideSelectedItemInfo();
                //HInventory.instance.HideSelectedItemInfo();
                NxtUiManager.instance.ShowSellingPrice();
               // HInventory.instance.ShowSellingPrice();
            }
            else
            {
                InteractUI.SetActive(true);
                NxtUiManager.instance.ShowSellingPrice();
              //  HInventory.instance.ShowSellingPrice();
            }
                
        }
    }

    /*
    void Update () 
    {
        if(Input.GetKeyDown(KeyCode.E) && m_canBeDisplay == true)
        {
            AddItemsToTheStore();
            Inventory.SetActive(true);
            StoreUI.SetActive(true);

        }
	
    }*/

    private void AddItemsToTheStore()
    {

        if (StoreItemsPanel.transform.childCount > 0)
        {
            int s = StoreItemsPanel.transform.childCount;
            for (int x = 0; x < s; x++)
            {
                BIDisable(x);
            }

        }
        for (int asd = 0; asd < m_IDsOfItemsInStore.Count; asd++)
        {
            //GameObject StoreItem = (GameObject)Instantiate(m_StoreItemsBotton);
            // NPCBottons b_info =  StoreItem.GetComponent<NPCBottons>();
            BIEnable(asd);
            if(NxtUiManager.instance.ItemsDB.GetByIDInList(m_IDsOfItemsInStore[asd]) == null)
            {
                Debug.Log("Item is Null");
                return;
            }
       //     InventoryItem i = new InventoryItem(NxtUiManager.instance.ItemsDB.GetByIDInList(m_IDsOfItemsInStore[asd]));
           // ItemsInStore[asd].NpcBottonInfo(i.Icon, i.Name, i.Description, i.BuyingPrice, i.SellingPrice, i.ID);
            // StoreItemsPanel.transform.GetChild(asd).GetComponent<NPCBottons>
            //b_info.NpcBottonInfo(i.icon, i.Name, i.Description, i.BuyingPrice,i.ID);
            //Bottons b = StoreItem.GetComponent<Bottons>();
            //b.BotonInfo(item.icon, item.Name, item.StacksAtm, item.StacksUpTo, item.Description);
            //StoreItem.transform.SetParent(StoreItemsPanel.transform);

        }

    }

    private void BIEnable(int index)
    {
        ItemsInStoreGameObject[index].SetActive(true);
    }
    private void BIDisable(int index)
    {
        ItemsInStoreGameObject[index].SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("Player Enters");
            m_canBeDisplay = true;
            InteractUI.SetActive(true);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            // Debug.Log("Player Exits");

            m_canBeDisplay = false;
            //Inventory.SetActive(false);
            StoreUI.SetActive(false);
            //HInventory.instance.SelectedItem.ItemID = 0;
            NxtUiManager.instance.SelectedItem.ItemID = 0;
            //Bottons.instance.ResetItemID();
            InteractUI.SetActive(false);
            NxtUiManager.instance.ShowSellingPrice();
           // HInventory.instance.ShowSellingPrice();
        }
    }
    void OnEnable()
    {
        BottonsManager.instance.AddNPCStore(this);
    }
    void OnDisable()
    {
        BottonsManager.instance.RemoveNPCStore(this);
    }


}
