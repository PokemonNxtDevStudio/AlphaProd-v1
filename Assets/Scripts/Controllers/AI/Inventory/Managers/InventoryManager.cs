using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NXT.Inventory;


namespace NXT
{
    //[RequireComponent(typeof(ItemManager))]
   // [RequireComponent(typeof(InventorySettingsManager))]
    [AddComponentMenu("InventorySystem/Managers/InventoryManager")]
    public partial class InventoryManager : MonoBehaviour
    {
        #region Variables

        //public InventoryLangDatabase lang; // All languages, notifications, stuff like that.


        /// <summary>
        /// The parent holds all collection's objects to keep the scene clean.
        /// </summary>
        public Transform collectionObjectsParent { get; private set; }

        /// <summary>
        /// Collections such as the Inventory are used to loot items.
        /// When an item is picked up the item will be moved to the inventory. You can create multiple Inventories and limit types per inventory.
        /// </summary>
        private static List<CollectionLookup<ItemCollectionBase>> lootToCollections = new List<CollectionLookup<ItemCollectionBase>>();
       // private static List<InventoryCollectionLookup<CharacterUI>> equipToCollections;
        private static List<ItemCollectionBase> bankCollections = new List<ItemCollectionBase>();

        #endregion
    

        private static InventoryManager _instance;


        public static InventoryManager instance {
            get {
                return _instance;
            }
        }
        
        public void Awake()
        {
            _instance = this;

 
        }


        #region Collection stuff

        protected virtual CollectionLookup<ItemCollectionBase> GetBestLootCollectionForItem(InventoryItem item)
        {

            CollectionLookup<ItemCollectionBase> best = null;
            /*
            foreach (var lookup in lootToCollections)
            {
                if (lookup.collection.CanAddItem(item))
                {
                    if (best == null)
                        best = lookup;
                    else if (lookup.priority > best.priority)
                        best = lookup;
                }
            }
            */
            return best;
        }
        protected virtual CollectionLookup<ItemCollectionBase> GetBestLootCollectionForItem(InventoryItem item, bool hasToFitAll)
        {
            if (hasToFitAll)
                return GetBestLootCollectionForItem(item);


            CollectionLookup<ItemCollectionBase> best = null;
/*
            foreach (var lookup in lootToCollections)
            {
                if (lookup.collection.CanAddItemCount(item) > 0)
                {
                    if (best == null)
                        best = lookup;
                    else if (lookup.priority > best.priority)
                        best = lookup;
                }
            }
            */
            return best;
        }


        /// <summary>
        /// Get the item count of all items in the lootable collections.
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns>Item count in all lootable collections.</returns>
        public static uint GetItemCount(uint itemID, bool checkBank)
        {
            uint count = 0;
            /*
            foreach (var collection in lootToCollections)
                count += collection.collection.GetItemCount(itemID);

            if(checkBank)
            {
                foreach (var collection in bankCollections)
                    count += collection.GetItemCount(itemID);
            }
            */
            return count;
        }

        /// <summary>
        /// Get the first item from all lootable collections.
        /// </summary>
        /// <param name="itemID">ID of the object your searching for</param>
        /// <returns></returns>
        public static Item Find(uint itemID, bool checkBank)
        {/*
            foreach (var col in lootToCollections)
            {
                var item = col.collection.Find(itemID);
                if(item != null)
                    return item;   
            }

            if(checkBank)
            {
                foreach (var col in bankCollections)
                {
                    var item = col.Find(itemID);
                    if (item != null)
                        return item;
                }
            }
            */
            return null;
        }

        /// <summary>
        /// Get all items with a given ID
        /// </summary>
        /// <param name="itemID">ID of the object your searching for</param>
        /// <returns></returns>
        public static List<Item> FindAll(uint itemID, bool checkBank)
        {
            
            var list = new List<Item>(8);/*
            foreach (var col in lootToCollections)
            {
                // Linq.Concat doesn't seem to work.. :/
                foreach (var item in col.collection.FindAll(itemID))
                {
                    list.Add(item);
                }
            }
        
            if(checkBank)
            {
                foreach (var col in bankCollections)
                {
                    // Linq.Concat doesn't seem to work.. :/
                    foreach (var item in col.FindAll(itemID))
                    {
                        list.Add(item);
                    }
                }
            }
 */
            return list;
            
        }


        /// <summary>
        /// Add an item to an inventory.
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <param name="storedItems">The items that were stored, item might be broken up into stacks</param>
        /// <returns></returns>
        public static bool AddItem(Item item, ICollection<InventoryItem> storedItems = null, bool repaint = true)
        {
            /*
            if (CanAddItem(item) == false)
            {
                instance.lang.collectionFull.Show(item.name, item.description, instance.inventory.collectionName);
                return false;
            }
            
            //// All items fit in 1 collection
    		var bestCollection1 = instance.GetBestLootCollectionForItem(item, false).collection;
			if (item.currentStackSize <= item.maxStackSize && bestCollection1.CanAddItemCount(item) >= item.currentStackSize)
			{
				bestCollection1.AddItem(item);
			    if(storedItems != null)
                    storedItems.Add(item);
				
                return true; // CanAddItemCount makes sure it can be stored, so AddItem can not have failed.
			}

            // Not all items fit in 1 collection, divide them, grab best collection after each iteration
            // Keep going until stack is divided over collections.
            while (item.currentStackSize > 0)
            {
                var bestCollection = instance.GetBestLootCollectionForItem(item, false).collection;
                uint canStoreInCollection = bestCollection.CanAddItemCount(item);

                var copy = GameObject.Instantiate<InventoryItemBase>(item);
                copy.currentStackSize = (uint)Mathf.Min(Mathf.Min(item.currentStackSize, item.maxStackSize), canStoreInCollection);
                bestCollection.AddItem(copy);

                item.currentStackSize -= copy.currentStackSize;
			    if(storedItems != null)
                    storedItems.Add(copy);

                //item.currentStackSize = (uint)Mathf.Max(item.currentStackSize, 0); // Make sure it's positive
            }

            Destroy(item.gameObject); // Item is divided over collections, no longer need it.
            */
            return true;
        }

        ///// <summary>
        ///// Add items to an inventory.
        ///// </summary>
        ///// <param name="items">The items to add</param>
        ///// <param name="storeAsMuchAsPossible">Store as much as possible, will store as many as possible and discard the rest.</param>
        ///// <param name="storedItems">The items that were stored, item might be broken up into stacks</param>
        ///// <param name="repaint">Should items be repainted? True will be fine in most cases</param>
        ///// <returns></returns>
        //public static bool AddItems(IEnumerable<InventoryItemBase> items, bool storeAsMuchAsPossible, ICollection<InventoryItemBase> storedItems = null, bool repaint = true)
        //{
        //    var toDict = new Dictionary<ItemCollectionBase, List<InventoryItemBase>>();

        //    foreach (var item in items)
        //    {
        //        var best = instance.GetBestLootCollectionForItem(item);
        //        if (best != null)
        //        {
        //            if (toDict.ContainsKey(best.collection) == false)
        //                toDict.Add(best.collection, new List<InventoryItemBase>());

        //            toDict[best.collection].Add(item);
        //        }
        //        else if (storeAsMuchAsPossible == false)
        //        {
        //            instance.lang.collectionFull.Show(item.name, item.description, instance.inventory.collectionName);
        //            return false; // Not all items can be stored.
        //        }
        //    }

        //    // Collection is filled
        //    foreach (var item in toDict)
        //    {
        //        item.Key.AddItems(item.Value, storedItems, repaint);
        //    }
        
        //    return true;
        //}

        /// <summary>
        /// Add an item to an inventory and remove it from the collection it was previously in.
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <param name="storedItems">The items that were stored, item might be broken up into stacks</param>
        /// <returns></returns>
        public static bool AddItemAndRemove(InventoryItem item, ICollection<InventoryItem> storedItems = null, bool repaint = true)
        {
            /*
            var best = instance.GetBestLootCollectionForItem(item);

            if (best != null)
            {
                return best.collection.AddItemAndRemove(item, storedItems, repaint);
            }

            instance.lang.collectionFull.Show(item.name, item.description, instance.inventory.collectionName);
            
             */
            return false;
        }

        public static bool CanAddItem(InventoryItem item)
        {
           // return CanAddItemCount(item) >= item.currentStackSize;
            return true;
        }

        public static uint CanAddItemCount(Item item)
        {
            uint count = 0;
            /*
            foreach (var lookup in lootToCollections)
                count += lookup.collection.CanAddItemCount(item);
            */
            return count;
        }


        /// <summary>
        /// Remove an item from the inventories / bank when checkBank = true.
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="amount"></param>
        /// <param name="checkBank">Also search the bankf or items, bank items take priority over items in the inventories</param>
        public static void RemoveItem(uint itemID, uint amount, bool checkBank)
        {
            /*
            var allItems = GetItemCount(itemID, checkBank); // All the items in all looting collections
            if (allItems < amount)
            {
                Debug.LogWarningFormat("Tried to remove {0} items, only {1} items available, check with FindAll().Count first.", amount, allItems);
                return;
            }

            uint amountToRemove = amount;
            if (checkBank)
            {
                foreach (var bank in bankCollections)
                {
                    if (amountToRemove > 0)
                    {
                        amountToRemove -= bank.RemoveItem(itemID, amountToRemove);
                    }
                    else
                        break;
                }
            }

            foreach (var inventory in lootToCollections)
            {
                //var items = bank.FindAll(itemID);
                if (amountToRemove > 0)
                {
                    amountToRemove -= inventory.collection.RemoveItem(itemID, amountToRemove);
                }
                else
                    break;
            }
            */
        }


        /// <summary>
        /// Add a collection that functions as an Inventory. Items will be looted to this collection.
        /// </summary>
        /// <param name="collection">The collection to add.</param>
        /// <param name="priority">
        /// How important is the collection, if you 2 collections can hold the item, which one should be chosen?
        /// Range of 0 to 100
        /// </param>
        public static void AddInventoryCollection(ItemCollectionBase collection, int priority)
        {
           // lootToCollections.Add(new InventoryCollectionLookup<ItemCollectionBase>(collection, priority));
        }


        public static void RemoveInventoryCollection(ItemCollectionBase collection)
        {
            var found = lootToCollections.FirstOrDefault(o => o.collection == collection);
            if (found != null)
                lootToCollections.Remove(found);

            //lootToCollections.Remove(new InventoryCollectionLookup(collection, priority));
        }


        /// <summary>
        /// Check if a given collection is a loot to collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsInventoryCollection(ItemCollectionBase collection)
        {
            return lootToCollections.Any(col => col.collection == collection);
        }

        /// <summary>
        /// Check if a given collection is a equip to collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsEquipToCollection(ItemCollectionBase collection)
        {
            //return equipToCollections.Any(col => col.collection == collection);
            return true;
        }

        /// <summary>
    



        /// <summary>
        /// Get all bank collections
        /// I casted it to an array (instead of list) to avoid messing with the internal list.
        /// </summary>
        /// <returns></returns>
        public static ItemCollectionBase[] GetBankCollections()
        {
            return bankCollections.ToArray();
        }

        public static ItemCollectionBase[] GetLootToCollections()
        {
            var l = new List<ItemCollectionBase>(lootToCollections.Count);
            foreach (var item in lootToCollections)
                l.Add(item.collection);

            return l.ToArray();
        }

        #endregion




    }
}