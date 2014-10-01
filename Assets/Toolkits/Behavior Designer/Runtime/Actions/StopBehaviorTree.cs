using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskDescription("Pause or disable a behavior tree and return success after it has been stopped.")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=21")]
    [TaskIcon("{SkinColor}StopBehaviorTreeIcon.png")]
    public class StopBehaviorTree : Action
    {
        [Tooltip("The GameObject of the behavior tree that should be stopped. If null use the current behavior")]
        public GameObject behaviorGameObject;
        [Tooltip("The group of the behavior tree that should be stopped")]
        public int group;
        [Tooltip("Should the behavior be paused or completely disabled")]
        public bool pauseBehavior = false;

        private Behavior behavior;

        public override void OnAwake()
        {
            // If behaviorGameObject is null use the GameObject that this task is attached to.
            if (behaviorGameObject == null) {
                behaviorGameObject = gameObject;
            }
            if (behaviorGameObject != null) {// search for the behavior tree based on the group number
                var behaviorTrees = behaviorGameObject.GetComponents<Behavior>();
                if (behaviorTrees.Length == 1) {
                    behavior = behaviorTrees[0];
                } else {
                    for (int i = 0; i < behaviorTrees.Length; ++i) {
                        if (behaviorTrees[i].Group == group) {
                            behavior = behaviorTrees[i];
                            break;
                        }
                    }
                    // If the group can't be found then use the first behavior tree
                    if (behavior == null) {
                        behavior = behaviorTrees[0];
                    }
                }
            }
        }

        public override TaskStatus OnUpdate()
        {
            // Start the behavior and return success.
            behavior.DisableBehavior(pauseBehavior);
            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            // Reset the properties back to their original values
            behavior = null;
            pauseBehavior = false;
        }
    }
}