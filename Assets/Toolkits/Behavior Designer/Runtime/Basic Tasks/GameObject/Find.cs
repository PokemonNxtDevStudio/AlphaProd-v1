using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
    [TaskCategory("Basic/GameObject")]
    [TaskDescription("Finds a GameObject by name. Returns Success.")]
    public class Find : Action
    {
        [Tooltip("The GameObject name to find")]
        public SharedString gameObjectName;
        [Tooltip("The object found by name")]
        public SharedGameObject storeValue;

        public override TaskStatus OnUpdate()
        {
            storeValue.Value = GameObject.Find(gameObjectName.Value);

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            if (gameObjectName != null) {
                gameObjectName.Value = null;
            }
            if (storeValue != null) {
                storeValue.Value = null;
            }
        }
    }
}