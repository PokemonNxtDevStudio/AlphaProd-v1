using System;
using UnityEngine;

    public class PokeballBehavior :ItemBehavior
{
    //FUNC
    public Action<int> customMethod;
        private TrainerController trainer;
        private int i = 0;

    void Start()
    {

        trainer = target.GetComponent<TrainerController>();
        customMethod = GetComponent<_delete2>().SpeedUP;
        
        DoParamMethod("InvokeCustomParamater");
    }

        public void DoParamMethod(params object[] items)
        {
            Debug.Log((items[0]));
        }
        public override void DoMethod<T>(T item) 
        {
           TrainerController controller= (TrainerController) Convert.ChangeType(item, typeof(T));
           controller.ReleasePokemon((i=(i+1 )%controller.pokemon.Count));

            //Controller.dothat
            //Cotroller.dothis.

            //SomeoterCOtroller.dothat
           //customMethod.Invoke(10);
    }


        public void ReleasePokemon(TrainerController t)
        {
            
        }
    }

