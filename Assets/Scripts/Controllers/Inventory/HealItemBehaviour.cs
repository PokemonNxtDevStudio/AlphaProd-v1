using UnityEngine;


using UnityEngine.Rendering;


public class HealItemBehaviour  : ItemBehavior
    {


         public override void DoMethod<T>(T Object)
        {
            
            Debug.Log("Healing");
        }
    }

