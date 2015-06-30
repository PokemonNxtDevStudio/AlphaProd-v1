using System;
using TeamName.Inventory;
using TeamName.Inventory.Models;
using UnityEngine;

namespace NXT.Inventory
{
    public class InventoryItem : Entity
    {
       // [SerializeField]
        //private ItemType m_itemtype;

        [SerializeField]
        private String m_Description;
        public String Description { get { return m_Description; } set { m_Description = value; } }

        //public ItemType ItemType { get { return m_itemtype; } set { m_itemtype = value; } }

        [SerializeField]
        private float m_buyingprice;
        public float BuyingPrice { get { return m_buyingprice; } set { m_buyingprice = value; } }

        [SerializeField]
        private float m_sellingprice;
        public float SellingPrice { get { return m_sellingprice; } set { m_sellingprice = value; } }

        [SerializeField]
        private int m_stackupto;
        public int StacksUpTo { get { return m_stackupto; } set { m_stackupto = value; } }  //maxStackCount

        [SerializeField]
        private int m_stacksatm = 1;
        public int StacksAtm { get { return m_stacksatm; } set { m_stacksatm = value; } }   //currentStackCount
  public ItemCollectionBase itemCollection { get; set; }


  public ItemGroup category
  {
      get
      {
          return ItemManager.instance.itemCategories[categoryID];
      }
      set
      {
          categoryID = value.ID;
      }
  }

  [SerializeField]
  private bool _useCategoryCooldown = true;
  public bool useCategoryCooldown
  {
      get
      {
          return _useCategoryCooldown;
      }
      set
      {
          _useCategoryCooldown = value;
      }
  }

  [SerializeField]
  [InventoryStat]
  [Range(0.0f, 999.0f)]
  private float _weight;

  public float Weight
  {
      get
      {
          return _weight;
      }
      set
      {
          _weight = value;
      }
  }



  /// <summary>
  /// Value between 1-100 , 100 being common
  /// 
  /// Example if you have two items, a= 100, b =  0.1
  /// Nothing dropping has value of 100; or double dice
  /// one item will drop 2/3 and other item 1/3
  /// drop rate for item B = 100/250 =1/3
  /// </summary>
  [SerializeField]
  [InventoryStat]
  [Range(1.0f, 100.0f)]
  public float RareValue = 0;



  [SerializeField]
  private float _cooldownTime;
  /// <summary>
  /// The time an item is unusable after it is used.
  /// </summary>
  public float CooldownTime
  {
      get
      {
          return _cooldownTime;
      }
      set
      {
          _cooldownTime = value;
      }
  }


  /// <summary>
  /// Optimization for getting a cooldown without running a coroutine
  /// </summary>
  [NonSerialized]
  private float _lastUsageTime;
  public float lastUsageTime
  {
      get
      {
          return _lastUsageTime;
      }
  }
  public bool isInCooldown
  {
      get
      {
          if (useCategoryCooldown)
          {
              if (category.lastUsageTime == 0.0f)
                  return false;

              if (Time.timeSinceLevelLoad - category.lastUsageTime < category.cooldownTime)
                  return true;

              return false;
          }

          // If the has not been used before
          if (_lastUsageTime == 0.0f)
              return false;

          //Debug.Log("Is it.. ? " + (Time.timeSinceLevelLoad - _lastUsageTime).ToString() + " cooldown is: " + cooldownTime);
          return Time.timeSinceLevelLoad - _lastUsageTime < CooldownTime;
      }
  }


  /// <summary>
  /// returns value between 0 -1, mainly for UI purposes. 
  /// for returning exact value use :  totalCooldowntime * coolDownFactor;
  /// </summary>
  public float cooldownFactor
  {
      get
      {
          if (useCategoryCooldown)
          {
              float e = Time.timeSinceLevelLoad - category.lastUsageTime;
              return e / category.cooldownTime;
          }

          float exp = Time.timeSinceLevelLoad - _lastUsageTime;
          return exp / _cooldownTime;
      }
  }


  public virtual GameObject Drop(Vector3 location)
  {
      return null;
      /*
      if (isDroppable == false || itemCollection.canDropFromCollection == false)
          return null;

      var settings = InventorySettingsManager.instance;
      float droppableFromDistanceUp = 10.0f; // Start at 10.0f
      if (settings.dropItemRaycastToGround)
      {
          // If there is something above the item, we can't move it up to raycast down, as this would place it on the collider above it. So first check how much we can go up...
          RaycastHit aboveHit;
          if (Physics.Raycast(location, Vector3.up, out aboveHit, 10.0f))
          {
              float dist = Vector3.Distance(aboveHit.transform.position, location);
              droppableFromDistanceUp = Mathf.Clamp(dist - 0.1f, 0.1f, 10.0f); // Needs to be at least a little above the ground
          }
      }


      GameObject dropObj = gameObject;
      if (rarity != null && rarity.dropObject != null)
      {
          // Drop a specific item whenever this is dropped
          //rarity.dropObject.CreateCopy<GameObject>();
          dropObj = GameObject.Instantiate<GameObject>(rarity.dropObject);
          var holder = dropObj.AddComponent<ObjectTriggererItemHolder>();
          holder.item = this;

          var triggerer = dropObj.GetComponent<ObjectTriggererItem>();
          if (triggerer == null)
              triggerer = dropObj.AddComponent<ObjectTriggererItem>();

          //var comp = dropObj.AddComponent(this.GetType());
          //comp.CopyValuesFrom(this, this.GetType()); // Special function that ads an item and copies all values via reflection from source.            
      }

      // Drop item into the world
      dropObj.transform.SetParent(null);

      if (settings.dropAtMousePosition)
      {
          dropObj.transform.position = location;
      }
      else
      {
          // Drop according to offset
          dropObj.transform.position = InventoryManager.currentPlayer.transform.position;
          dropObj.transform.rotation = InventoryManager.currentPlayer.transform.rotation;
          dropObj.transform.Translate(settings.dropOffsetVector);
          dropObj.transform.rotation = Quaternion.identity;
      }


      if (settings.dropItemRaycastToGround)
      {
          RaycastHit hit;
          if (Physics.Raycast(location + (Vector3.up * droppableFromDistanceUp), Vector3.down, out hit, 25.0f))
          {
              // place it on the ground
              dropObj.transform.position = hit.point + (Vector3.up * 0.1f); // + a little offset to avoid it falling through the ground
          }
      }


      dropObj.SetActive(true);

      // Clear fold collection
      itemCollection.NotifyItemDropped(this, ID, currentStackSize, dropObj);
      itemCollection.SetItem(index, null);

      // Remove old stuff
      itemCollection = null;
      index = 0;

      //var trigger = dropObj.GetComponent<ObjectTriggererItem>();

      if (OnDroppedItem != null)
          OnDroppedItem(location);

      return dropObj;
       */
  }


  public virtual void Use(UnityEngine.Object obj)
  {
      
  }

  public virtual void Use(UnityEngine.Object obj, UnityEngine.Object obj1)
  {

  }


    }

   



    public enum ItemType
    {

    }
}
