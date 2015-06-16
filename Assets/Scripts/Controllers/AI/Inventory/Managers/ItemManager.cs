using UnityEngine;
using UnityEngine.UI;
using System;
using TeamName.Inventory.Models;

namespace NXT.Inventory
{
   public class ItemManager : MonoBehaviour
    {
        public ItemAssetDatabase itemDatabase;

        public ItemGroup[] itemCategories { get { return itemDatabase.itemGroup; } set { itemDatabase.itemGroup = value; } }
        public static ItemManager instance;
    }
}
