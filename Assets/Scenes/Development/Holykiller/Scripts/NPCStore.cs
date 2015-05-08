using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NPCStore : MonoBehaviour
{
    [HideInInspector]
    public GameObject Inventory;
    [SerializeField]
    private GameObject m_StoreItemsBotton;
    [SerializeField]
    private GameObject StoreUI;
    [SerializeField]
    private GameObject StoreItemsPanel;

    [SerializeField]
    private ItemAssetDatabase db;

    private int m_storeId;
    public int StoreID { get { return m_storeId; } set { m_storeId = value; } }
    

    private List<InventoryItem> m_storeItems;

    [SerializeField]
    private List<int> m_IDsOfItemsInStore;


    private bool m_canBeDisplay = false;
  
	void Start () 
    {
        if(m_StoreItemsBotton == null)
        {
            Debug.Log("Add Store Item Botton to " + gameObject.name);
            return;
        }
        if(StoreUI == null)
        {
            Debug.Log("Add Store UI GameObject to " + gameObject.name);
            return;
        }
        if (StoreItemsPanel == null)
        {
            Debug.Log("Add Store Items GameObject to " + gameObject.name);
            return;
        }

        
	}
	

	void Update () 
    {
        if(Input.GetKeyDown(KeyCode.E) && m_canBeDisplay == true)
        {
            AddItemsToTheStore();
            Inventory.SetActive(true);
            StoreUI.SetActive(true);

        }
	
	}
    
    private void AddItemsToTheStore()
    {
        if(StoreItemsPanel.transform.childCount > 0)
        {
            int s = StoreItemsPanel.transform.childCount;
            for (int x = 0; x < s; x++)
                DestroyImmediate(StoreItemsPanel.transform.GetChild(0).gameObject);
        }
        for(int asd = 0 ; asd < m_IDsOfItemsInStore.Count;asd++)
        {
            GameObject StoreItem = (GameObject)Instantiate(m_StoreItemsBotton);
            NPCBottons b_info =  StoreItem.GetComponent<NPCBottons>();
            InventoryItem i = db.GetByID(m_IDsOfItemsInStore[asd]);
            b_info.NpcBottonInfo(i.icon, i.Name, i.Description, i.BuyingPrice,i.ID);
            //Bottons b = StoreItem.GetComponent<Bottons>();
            //b.BotonInfo(item.icon, item.Name, item.StacksAtm, item.StacksUpTo, item.Description);
            StoreItem.transform.SetParent(StoreItemsPanel.transform);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
           
            //Debug.Log("Player Enters");
            m_canBeDisplay = true;            

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
           // Debug.Log("Player Exits");
            m_canBeDisplay = false;
            Inventory.SetActive(false);
            StoreUI.SetActive(false);
            
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
