using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
    [TaskCategory("Basic/GameObject")]
    [TaskDescription("Sends a message to the target GameObject. Returns Success.")]
    public class SendMessage : Action
    {
        [Tooltip("The GameObject to send a message to")]
        public SharedGameObject targetObject;
        [Tooltip("The message to send")]
        public SharedString message;

        public override TaskStatus OnUpdate()
        {
            if (targetObject.Value == null) {
                Debug.LogWarning("Target object is null");
                return TaskStatus.Failure;
            }

            targetObject.Value.SendMessage(message.Value);

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            if (targetObject != null) {
                targetObject.Value = null;
            }
            if (message != null) {
                message.Value = "";
            }
        }
    }
}