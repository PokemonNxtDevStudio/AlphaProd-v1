using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
    [TaskCategory("Basic/Rigidbody")]
    [TaskDescription("Returns Success if the Rigidbody is using gravity, otherwise Failure.")]
    public class UseGravity : Conditional
    {
        public override TaskStatus OnUpdate()
        {
            if (rigidbody == null) {
                Debug.LogWarning("Rigidbody is null");
                return TaskStatus.Failure;
            }

            return rigidbody.useGravity ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}