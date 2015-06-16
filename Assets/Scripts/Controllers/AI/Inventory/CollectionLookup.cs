using NXT.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NXT.Inventory
{

    public partial class CollectionLookup<T> where T : ItemCollectionBase
    {
        public T collection { get; set; }

        /// <summary>
        /// Range of 0-100
        /// </summary>
        public int priority { get; set; }


        public CollectionLookup(T collection, int priority)
        {
            this.collection = collection;
            this.priority = priority;
        }
    }
}