using System;
using System.Collections.Generic;
using UnityEngine;
using NXT;
namespace NXT.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [Serializable]
        public class ItemAmount
        {
            [SerializeField]
            private ItemBaseType m_ItemType;
            [SerializeField]
            private int m_Amount = 1;
            public ItemBaseType ItemType
            {
                get
                {
                    return this.m_ItemType;
                }
            }
            public int Amount
            {
                get
                {
                    return this.m_Amount;
                }
            }
            public ItemAmount(ItemBaseType itemType, int amount)
            {
                this.m_ItemType = itemType;
                this.m_Amount = amount;
            }
        }
        private class ItemInstance
        {
            private ItemBaseType m_ItemType;
            private GameObject m_GameObject;
            private Item m_Item;
            private int m_ItemCount;
            private Inventory.ConsumableItemInstance m_ConsumableItem;
            public ItemBaseType ItemType
            {
                get
                {
                    return this.m_ItemType;
                }
            }
            public GameObject GameObject
            {
                get
                {
                    return this.m_GameObject;
                }
            }
            public Item Item
            {
                get
                {
                    return this.m_Item;
                }
            }
            public int ItemCount
            {
                get
                {
                    return this.m_ItemCount;
                }
                set
                {
                    this.m_ItemCount = Mathf.Max(Mathf.Min(value, this.m_ItemType.GetCapacity()), 0);
                }
            }
            public Inventory.ConsumableItemInstance ConsumableItem
            {
                get
                {
                    return this.m_ConsumableItem;
                }
                set
                {
                    this.m_ConsumableItem = value;
                }
            }
            public ItemInstance(GameObject itemGameObject, ItemBaseType itemBaseType)
            {
                this.m_GameObject = itemGameObject;
                this.m_ItemType = itemBaseType;
                this.SetActive(false);
                this.m_Item = this.m_GameObject.GetComponent<Item>();
                this.m_Item.ItemType = itemBaseType;
            }
            public void SetActive(bool active)
            {
                this.m_GameObject.SetActive(active);
            }
        }
        private class ConsumableItemInstance
        {
            private int m_UnloadedCount;
            private int m_LoadedCount;
            private ConsumableItemType m_Item;
            private PrimaryItemType m_Owner;
            private int m_Capacity;
            public ConsumableItemType Item
            {
                get
                {
                    return this.m_Item;
                }
            }
            public PrimaryItemType Owner
            {
                get
                {
                    return this.m_Owner;
                }
            }
            public int Capacity
            {
                get
                {
                    return this.m_Capacity;
                }
            }
            public int LoadedCount
            {
                get
                {
                    return this.m_LoadedCount;
                }
                set
                {
                    this.m_LoadedCount = Mathf.Max(value, 0);
                }
            }
            public int UnloadedCount
            {
                get
                {
                    return this.m_UnloadedCount;
                }
                set
                {
                    this.m_UnloadedCount = Mathf.Max(Mathf.Min(value, this.m_Capacity - this.m_LoadedCount), 0);
                }
            }
            public ConsumableItemInstance(ConsumableItemType item, int capacity, PrimaryItemType owner)
            {
                this.m_Item = item;
                this.m_Capacity = capacity;
                this.m_Owner = owner;
            }
        }
        [SerializeField]
        private Inventory.ItemAmount[] m_DefaultLoadout;
        [SerializeField]
        private bool m_UnlimitedAmmo;
        [SerializeField]
        private ItemBaseType m_UnequippedItemType;
        private List<Inventory.ItemInstance> m_PrimaryInventory = new List<Inventory.ItemInstance>();
        private List<Inventory.ConsumableItemInstance> m_ConsumableInventory = new List<Inventory.ConsumableItemInstance>();
        private List<Inventory.ItemInstance> m_QuickUseInventory = new List<Inventory.ItemInstance>();
        private Dictionary<ItemBaseType, int> m_ItemIndexMap = new Dictionary<ItemBaseType, int>();
        private int m_CurrentPrimaryIndex = -1;
        private Item m_CurrentPrimaryItem;
        private int m_CurrentQuickUseIndex = -1;
        private Item m_CurrentQuickUseItem;
        private Item m_UnequippedItem;
        private int m_LastEquipedItem = -1;
        private int m_EquipIndex = -1;
        private int m_UnequpIndex = -1;
        private SharedMethod<bool> m_CanInteractItem = null;
        private GameObject m_GameObject = null;
        public Inventory.ItemAmount[] DefaultLoadout
        {
            get
            {
                return this.m_DefaultLoadout;
            }
            set
            {
                this.m_DefaultLoadout = value;
            }
        }
        private Item CurrentPrimaryItem
        {
            set
            {
                this.m_CurrentPrimaryItem = value;
                if (this.m_CurrentPrimaryItem == null)
                {
                    EventHandler.ExecuteEvent<Item>(this.m_GameObject, "OnInventoryPrimaryItemChange", null);
                }
                else
                {
                    EventHandler.ExecuteEvent<Item>(this.m_GameObject, "OnInventoryPrimaryItemChange", this.m_CurrentPrimaryItem);
                }
            }
        }
        private Item SharedProperty_CurrentPrimaryItem
        {
            get
            {
                return this.m_CurrentPrimaryItem;
            }
        }
        private Item SharedProperty_CurrentQuickUseItem
        {
            get
            {
                return this.m_CurrentQuickUseItem;
            }
        }
        private int SharedProperty_LoadedCount
        {
            get
            {
                return this.GetCurrentItemCount(true, true);
            }
        }
        private int SharedProperty_UnloadedCount
        {
            get
            {
                return this.GetCurrentItemCount(true, false);
            }
        }
        private int SharedProperty_ItemCount
        {
            get
            {
                return this.GetCurrentItemCount(false, false);
            }
        }
        private string SharedProperty_ItemName
        {
            get
            {
                if (this.m_CurrentPrimaryIndex != -1)
                {
                    return (this.m_PrimaryInventory[this.m_CurrentPrimaryIndex].ItemType as PrimaryItemType).ItemName;
                }
                if (this.m_UnequippedItem != null)
                {
                    return (this.m_UnequippedItemType as PrimaryItemType).ItemName;
                }
                return "No Item";
            }
        }
        private string SharedProperty_ItemLowerAnimatorUseState
        {
            get
            {
                if (this.m_CurrentPrimaryIndex != -1)
                {
                    return (this.m_PrimaryInventory[this.m_CurrentPrimaryIndex].ItemType as PrimaryItemType).LowerAnimatorUseState;
                }
                if (this.m_UnequippedItem != null)
                {
                    return (this.m_UnequippedItemType as PrimaryItemType).LowerAnimatorUseState;
                }
                return "No Item";
            }
        }
        private void Awake()
        {
            this.m_GameObject = base.gameObject;
            this.EquipUnequpItem(false, -1, true);
            SharedManager.Register(this);
            this.InitInventory(base.GetComponentsInChildren<Item>(true));
        }
        private void InitInventory(Item[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (this.m_UnequippedItemType != null && items[i].ItemType == this.m_UnequippedItemType)
                {
                    this.m_UnequippedItem = items[i];
                }
                else
                {
                    this.AddInventoryItem(items[i].ItemType, items[i].gameObject);
                }
                items[i].Init(this);
            }
        }
        private void AddInventoryItem(ItemBaseType itemBaseType, GameObject itemGameObject)
        {
            if (itemBaseType is QuickUseItemType)
            {
                this.m_ItemIndexMap.Add(itemBaseType, this.m_QuickUseInventory.Count);
                Inventory.ItemInstance item = new Inventory.ItemInstance(itemGameObject, itemBaseType);
                this.m_QuickUseInventory.Add(item);
            }
            else
            {
                PrimaryItemType primaryItemType = itemBaseType as PrimaryItemType;
                this.m_ItemIndexMap.Add(primaryItemType, this.m_PrimaryInventory.Count);
                Inventory.ItemInstance itemInstance = new Inventory.ItemInstance(itemGameObject, primaryItemType);
                this.m_PrimaryInventory.Add(itemInstance);
                if (primaryItemType.ConsumableItem != null && primaryItemType.ConsumableItem.ItemType != null)
                {
                    this.m_ItemIndexMap.Add(primaryItemType.ConsumableItem.ItemType, this.m_ConsumableInventory.Count);
                    Inventory.ConsumableItemInstance consumableItemInstance = new Inventory.ConsumableItemInstance(primaryItemType.ConsumableItem.ItemType, primaryItemType.ConsumableItem.Capacity, primaryItemType);
                    this.m_ConsumableInventory.Add(consumableItemInstance);
                    itemInstance.ConsumableItem = consumableItemInstance;
                }
            }
        }
        private void OnEnable()
        {
            EventHandler.RegisterEvent(this.m_GameObject, "OnAnimatorItemEquipped", new Action(this.OnItemEquipped));
            EventHandler.RegisterEvent(this.m_GameObject, "OnAnimatorItemUnequipped", new Action(this.OnItemUnequipped));
        }
        private void OnDisable()
        {
            EventHandler.UnregisterEvent(this.m_GameObject, "OnAnimatorItemEquipped", new Action(this.OnItemEquipped));
            EventHandler.UnregisterEvent(this.m_GameObject, "OnAnimatorItemUnequipped", new Action(this.OnItemUnequipped));
        }
        private void Start()
        {
            SharedManager.InitializeSharedFields(this.m_GameObject, this);
            this.LoadDefaultLoadout();
            EventHandler.ExecuteEvent(this.m_GameObject, "OnInventoryInitialized");
        }
        public void LoadDefaultLoadout()
        {
            for (int i = 0; i < this.m_DefaultLoadout.Length; i++)
            {
                this.PickupItem(this.m_DefaultLoadout[i].ItemType, this.m_DefaultLoadout[i].Amount, true);
            }
        }
        public void PickupItem(ItemBaseType itemBaseType, int amount, bool immediateActivation)
        {
            int num;
            if (!this.m_ItemIndexMap.TryGetValue(itemBaseType, out num))
            {
                Debug.LogError("Unable to pickup " + itemBaseType + ": has it been added to an item object?");
                return;
            }
            if (amount == 0)
            {
                amount = 1;
            }
            if (!this.m_CanInteractItem.Invoke())
            {
                immediateActivation = true;
            }
            if (itemBaseType is PrimaryItemType)
            {
                if (this.m_PrimaryInventory[num].ItemCount == 0)
                {
                    this.m_PrimaryInventory[num].ItemCount = amount;
                    bool flag = false;
                    if (this.m_CurrentPrimaryIndex != -1)
                    {
                        this.EquipUnequpItem(false, this.m_CurrentPrimaryIndex, immediateActivation);
                        if (immediateActivation)
                        {
                            this.m_CurrentPrimaryIndex = -1;
                        }
                        else
                        {
                            this.m_EquipIndex = num;
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        this.EquipUnequpItem(true, num, immediateActivation);
                    }
                    this.m_CurrentPrimaryIndex = num;
                    this.CurrentPrimaryItem = this.m_PrimaryInventory[this.m_CurrentPrimaryIndex].Item;
                    Inventory.ConsumableItemInstance consumableItem = this.m_PrimaryInventory[num].ConsumableItem;
                    if (consumableItem != null && consumableItem.UnloadedCount > 0)
                    {
                        EventHandler.ExecuteEvent<PrimaryItemType, bool>(this.m_GameObject, "OnInventoryConsumableItemAdded", this.m_CurrentPrimaryItem.ItemType as PrimaryItemType, this.m_ConsumableInventory[num].LoadedCount == 0);
                    }
                }
            }
            else if (itemBaseType is ConsumableItemType)
            {
                this.m_ConsumableInventory[num].UnloadedCount += amount;
                int num2 = this.m_ItemIndexMap[this.m_ConsumableInventory[num].Owner];
                bool flag2 = num2 == this.m_CurrentPrimaryIndex && this.m_EquipIndex == -1;
                EventHandler.ExecuteEvent<PrimaryItemType, bool>(this.m_GameObject, "OnInventoryConsumableItemAdded", this.m_ConsumableInventory[num].Owner, immediateActivation || !flag2);
            }
            else
            {
                this.m_QuickUseInventory[num].ItemCount += amount;
                if (this.m_QuickUseInventory[num].ItemCount <= amount)
                {
                    this.m_CurrentQuickUseIndex = num;
                    this.m_CurrentQuickUseItem = this.m_QuickUseInventory[this.m_CurrentQuickUseIndex].Item;
                }
                EventHandler.ExecuteEvent<int>(this.m_GameObject, "OnInventoryQuickUseItemCountChange", this.m_QuickUseInventory[num].ItemCount);
            }
        }
        public Item GetCurrentItem(bool primaryItem)
        {
            return (!primaryItem) ? this.m_CurrentQuickUseItem : this.m_CurrentPrimaryItem;
        }
        public int GetCurrentItemCount(bool primaryItem, bool loadedCount)
        {
            if (((!primaryItem) ? this.m_CurrentQuickUseIndex : this.m_CurrentPrimaryIndex) == -1)
            {
                return 0;
            }
            ItemBaseType itemType = this.GetCurrentItem(primaryItem).ItemType;
            return this.GetItemCount(itemType, loadedCount);
        }
        public int GetItemCount(ItemBaseType itemBaseType)
        {
            return this.GetItemCount(itemBaseType, true);
        }
        public int GetItemCount(ItemBaseType itemBaseType, bool loadedCount)
        {
            int index;
            if (!this.m_ItemIndexMap.TryGetValue(itemBaseType, out index))
            {
                return 2147483647;
            }
            if (this.m_UnlimitedAmmo)
            {
                return 2147483647;
            }
            if (!(itemBaseType is PrimaryItemType))
            {
                return this.m_QuickUseInventory[index].ItemCount;
            }
            Inventory.ConsumableItemInstance consumableItem = this.m_PrimaryInventory[index].ConsumableItem;
            if (consumableItem == null)
            {
                return 2147483647;
            }
            if (loadedCount)
            {
                return consumableItem.LoadedCount;
            }
            return consumableItem.UnloadedCount;
        }
        public int UseItem(ItemBaseType itemBaseType, int amount)
        {
            int index;
            if (!this.m_ItemIndexMap.TryGetValue(itemBaseType, out index))
            {
                return 0;
            }
            if (this.m_UnlimitedAmmo)
            {
                amount = 0;
            }
            if (itemBaseType is PrimaryItemType)
            {
                Inventory.ConsumableItemInstance consumableItem = this.m_PrimaryInventory[index].ConsumableItem;
                if (consumableItem.LoadedCount == 2147483647)
                {
                    amount = 0;
                }
                consumableItem.LoadedCount -= amount;
                EventHandler.ExecuteEvent<int, int>(this.m_GameObject, "OnInventoryPrimaryItemCountChange", consumableItem.LoadedCount, consumableItem.UnloadedCount);
                return consumableItem.LoadedCount;
            }
            if (this.m_QuickUseInventory[index].ItemCount == 2147483647)
            {
                amount = 0;
            }
            this.m_QuickUseInventory[index].ItemCount -= amount;
            EventHandler.ExecuteEvent<int>(this.m_GameObject, "OnInventoryQuickUseItemCountChange", this.m_QuickUseInventory[index].ItemCount);
            return this.m_QuickUseInventory[index].ItemCount;
        }
        public void ReloadItem(ItemBaseType itemBaseType, int amount)
        {
            int index;
            if (!this.m_ItemIndexMap.TryGetValue(itemBaseType, out index) || !(itemBaseType is PrimaryItemType))
            {
                return;
            }
            Inventory.ConsumableItemInstance consumableItem = this.m_PrimaryInventory[index].ConsumableItem;
            if (amount > consumableItem.UnloadedCount)
            {
                amount = consumableItem.UnloadedCount;
            }
            consumableItem.LoadedCount += amount;
            if (consumableItem.UnloadedCount != 2147483647)
            {
                consumableItem.UnloadedCount -= amount;
            }
            EventHandler.ExecuteEvent<int, int>(this.m_GameObject, "OnInventoryPrimaryItemCountChange", consumableItem.LoadedCount, consumableItem.UnloadedCount);
        }
        public void SwitchItem(bool primaryItem, bool next)
        {
            int num = (!primaryItem) ? this.m_CurrentQuickUseIndex : this.m_CurrentPrimaryIndex;
            int num2 = this.SwitchItem(primaryItem, next, num);
            if (num2 == num)
            {
                return;
            }
            if (primaryItem)
            {
                if (num2 != -1)
                {
                    this.EquipItem(num2);
                }
                else
                {
                    this.CurrentPrimaryItem = null;
                    this.m_CurrentPrimaryIndex = -1;
                    this.EquipUnequpItem(false, -1, false);
                }
            }
            else
            {
                this.m_CurrentQuickUseIndex = num2;
                if (this.m_CurrentQuickUseIndex != -1)
                {
                    this.m_CurrentQuickUseItem = this.m_QuickUseInventory[this.m_CurrentQuickUseIndex].Item;
                }
                else
                {
                    this.m_CurrentQuickUseItem = null;
                }
            }
        }
        private int SwitchItem(bool primaryItem, bool next, int currentItemIndex)
        {
            if (currentItemIndex != -1)
            {
                List<Inventory.ItemInstance> list = (!primaryItem) ? this.m_QuickUseInventory : this.m_PrimaryInventory;
                int num = (currentItemIndex + ((!next) ? -1 : 1)) % list.Count;
                if (num < 0)
                {
                    num = list.Count - 1;
                }
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[num].ItemCount > 0)
                    {
                        return num;
                    }
                    num = (num + ((!next) ? -1 : 1)) % list.Count;
                    if (num < 0)
                    {
                        num = list.Count - 1;
                    }
                }
            }
            return -1;
        }
        public bool SharedMethod_HasItem(ItemBaseType itemBaseType)
        {
            int index;
            if (!this.m_ItemIndexMap.TryGetValue(itemBaseType, out index))
            {
                return this.m_UnequippedItem != null && this.m_UnequippedItem.ItemType.Equals(itemBaseType);
            }
            if (itemBaseType is PrimaryItemType)
            {
                Inventory.ItemInstance itemInstance = this.m_PrimaryInventory[index];
                return itemInstance.ItemCount > 0;
            }
            Inventory.ItemInstance itemInstance2 = this.m_QuickUseInventory[index];
            return itemInstance2.ItemCount > 0;
        }
        public void ToggleEquippedItem()
        {
            if (this.m_CurrentPrimaryIndex == -1 || (this.m_UnequippedItemType != null && this.m_PrimaryInventory[this.m_CurrentPrimaryIndex].ItemType.Equals(this.m_UnequippedItemType)))
            {
                this.EquipItem();
            }
            else
            {
                this.UnequipCurrentItem();
            }
        }
        private void EquipItem()
        {
            if (this.m_LastEquipedItem == -1)
            {
                return;
            }
            this.m_CurrentPrimaryIndex = this.m_LastEquipedItem;
            this.EquipUnequpItem(true, this.m_CurrentPrimaryIndex, false);
            this.CurrentPrimaryItem = this.m_PrimaryInventory[this.m_CurrentPrimaryIndex].Item;
        }
        public void EquipItem(PrimaryItemType primaryItemType)
        {
            int itemIndex;
            if (!this.m_ItemIndexMap.TryGetValue(primaryItemType, out itemIndex))
            {
                if (this.m_UnequippedItem != null && this.m_UnequippedItem.ItemType.Equals(primaryItemType))
                {
                    this.UnequipCurrentItem();
                }
                return;
            }
            this.EquipItem(itemIndex);
        }
        public void EquipItem(int itemIndex)
        {
            if (itemIndex >= this.m_PrimaryInventory.Count || this.m_PrimaryInventory[itemIndex].ItemCount == 0 || this.m_CurrentPrimaryIndex == itemIndex)
            {
                return;
            }
            this.m_CurrentPrimaryIndex = itemIndex;
            this.CurrentPrimaryItem = this.m_PrimaryInventory[this.m_CurrentPrimaryIndex].Item;
            if (this.m_LastEquipedItem != -1)
            {
                this.EquipUnequpItem(false, this.m_LastEquipedItem, false);
                if (this.m_UnequpIndex != -1)
                {
                    this.m_EquipIndex = this.m_CurrentPrimaryIndex;
                }
                else
                {
                    this.EquipUnequpItem(true, itemIndex, true);
                }
            }
            else
            {
                this.EquipUnequpItem(true, itemIndex, false);
            }
        }
        private void UnequipCurrentItem()
        {
            if (this.m_CurrentPrimaryIndex == -1)
            {
                return;
            }
            this.EquipUnequpItem(false, this.m_CurrentPrimaryIndex, false);
            this.CurrentPrimaryItem = this.m_UnequippedItem;
            this.m_CurrentPrimaryIndex = -1;
        }
        private void RemoveItem(ItemBaseType itemBaseType, bool immediateRemoval)
        {
            int num;
            if (this.m_ItemIndexMap.TryGetValue(itemBaseType, out num))
            {
                if (itemBaseType is PrimaryItemType)
                {
                    this.m_PrimaryInventory[num].ItemCount = 0;
                    this.EquipUnequpItem(false, num, immediateRemoval);
                    Inventory.ConsumableItemInstance consumableItem = this.m_PrimaryInventory[num].ConsumableItem;
                    if (consumableItem != null)
                    {
                        Inventory.ConsumableItemInstance arg_5C_0 = consumableItem;
                        int num2 = 0;
                        consumableItem.UnloadedCount = num2;
                        arg_5C_0.LoadedCount = num2;
                    }
                }
                else
                {
                    this.m_QuickUseInventory[num].ItemCount = 0;
                }
            }
        }
        public void RemoveAllItems()
        {
            for (int i = 0; i < this.m_PrimaryInventory.Count; i++)
            {
                if (this.m_PrimaryInventory[i].ItemCount > 0)
                {
                    this.RemoveItem(this.m_PrimaryInventory[i].ItemType, true);
                }
            }
            for (int j = 0; j < this.m_QuickUseInventory.Count; j++)
            {
                if (this.m_QuickUseInventory[j].ItemCount > 0)
                {
                    this.RemoveItem(this.m_QuickUseInventory[j].ItemType, true);
                }
            }
            this.m_CurrentPrimaryIndex = (this.m_CurrentQuickUseIndex = -1);
            this.CurrentPrimaryItem = (this.m_CurrentQuickUseItem = null);
        }
        private void EquipUnequpItem(bool equip, int itemIndex, bool immediate)
        {
            if (this.m_CanInteractItem != null && !this.m_CanInteractItem.Invoke())
            {
                immediate = true;
            }
            if (equip)
            {
                if (immediate)
                {
                    this.m_PrimaryInventory[itemIndex].SetActive(true);
                }
                else
                {
                    this.m_EquipIndex = itemIndex;
                }
                this.m_LastEquipedItem = itemIndex;
            }
            else
            {
                if (immediate)
                {
                    if (itemIndex != -1)
                    {
                        this.m_PrimaryInventory[itemIndex].SetActive(false);
                    }
                }
                else
                {
                    this.m_UnequpIndex = itemIndex;
                }
                this.m_EquipIndex = -1;
            }
            if (!immediate)
            {
                EventHandler.ExecuteEvent<bool>(this.m_GameObject, "OnInventoryItemEquipping", equip);
            }
        }
        private void OnItemEquipped()
        {
            if (this.m_EquipIndex != -1)
            {
                this.m_PrimaryInventory[this.m_EquipIndex].SetActive(true);
                this.m_EquipIndex = -1;
            }
        }
        private void OnItemUnequipped()
        {
            if (this.m_UnequpIndex != -1)
            {
                this.m_PrimaryInventory[this.m_UnequpIndex].SetActive(false);
                this.m_UnequpIndex = -1;
            }
            if (this.m_EquipIndex != -1)
            {
                this.EquipUnequpItem(true, this.m_EquipIndex, true);
            }
        }
    }
}
