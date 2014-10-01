using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
    [TaskCategory("Basic/GameObject")]
    [TaskDescription("Destorys the specified GameObject. Returns Success.")]
    public class Destroy : Action
    {
        [Tooltip("GameObject to destroy")]
        public SharedGameObject destroyGameObject;
        [Tooltip("Time to destroy the GameObject in")]
        public float time;

        public override TaskStatus OnUpdate()
        {
            if (destroyGameObject.Value == null) {
                destroyGameObject.Value = gameObject;
            }
            if (time == 0) {
                GameObject.Destroy(destroyGameObject.Value);
            } else {
                GameObject.Destroy(destroyGameObject.Value, time);
            }

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            if (destroyGameObject != null) {
                destroyGameObject.Value = null;
            }
            time = 0;
        }
    }
}