using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
    [TaskCategory("Basic/GameObject")]
    [TaskDescription("Destorys the specified GameObject immediately. Returns Success.")]
    public class DestroyImmediate : Action
    {
        [Tooltip("GameObject to destroy")]
        public SharedGameObject destroyGameObject;

        public override TaskStatus OnUpdate()
        {
            if (destroyGameObject.Value == null) {
                destroyGameObject.Value = gameObject;
            }

            GameObject.DestroyImmediate(destroyGameObject.Value);

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            if (destroyGameObject != null) {
                destroyGameObject.Value = null;
            }
        }
    }
}