using System;
namespace NXT.Inventory
{
    public class ConsumableItemType : ItemBaseType
    {
        public override int GetCapacity()
        {
            return 2147483647;
        }
    }
}
