using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
    [TaskCategory("Basic/GameObject")]
    [TaskDescription("Finds a GameObject by tag. Returns Success.")]
    public class FindWithTag : Action
    {
        [Tooltip("The tag of the GameObject to find")]
        public SharedString tag;
        [Tooltip("The object found by name")]
        public SharedGameObject storeValue;

        public override TaskStatus OnUpdate()
        {
            storeValue.Value = GameObject.FindWithTag(tag.Value);

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            if (tag != null) {
                tag.Value = null;
            }
            if (storeValue != null) {
                storeValue.Value = null;
            }
        }
    }
}