using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
    [TaskCategory("Basic/SharedVariable")]
    [TaskDescription("Gets the Transform from the GameObject. Returns Success.")]
    public class SharedGameObjectToTransform : Action
    {
        [Tooltip("The GameObject to get the Transform of")]
        public SharedGameObject sharedGameObject;
        [Tooltip("The Transform to set")]
        public SharedTransform sharedTransform;

        public override TaskStatus OnUpdate()
        {
            sharedTransform.Value = sharedGameObject.Value.GetComponent<Transform>();

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            if (sharedGameObject != null) {
                sharedGameObject.Value = null;
            }
            if (sharedTransform != null) {
                sharedTransform.Value = null;
            }
        }
    }
}