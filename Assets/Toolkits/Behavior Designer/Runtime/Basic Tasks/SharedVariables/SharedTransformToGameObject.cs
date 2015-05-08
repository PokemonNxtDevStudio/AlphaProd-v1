using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
    [TaskCategory("Basic/SharedVariable")]
    [TaskDescription("Gets the GameObject from the Transform component. Returns Success.")]
    public class SharedTransformToGameObject : Action
    {
        [Tooltip("The Transform component")]
        public SharedTransform sharedTransform;
        [Tooltip("The GameObject to set")]
        public SharedGameObject sharedGameObject;

        public override TaskStatus OnUpdate()
        {
            sharedGameObject.Value = sharedTransform.Value.gameObject;

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            if (sharedTransform != null) {
                sharedTransform.Value = null;
            }
            if (sharedGameObject != null) {
                sharedGameObject.Value = null;
            }
        }
    }
}