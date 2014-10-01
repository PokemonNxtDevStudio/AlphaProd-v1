using UnityEngine;
using System.Collections;

namespace PokemonNXT {

    /// <summary>
    /// Dummy interface implemented by both BaseBehavior and BaseNetBehavior
    /// If we need to add a new global method or property for our Behaviors
    /// we first add it here and then we implement it in each base behavior
    /// </summary>
    public interface IBaseBehavior {
        Vector3 pos { set; get; }
        float x { set; get; }
        float y { set; get; }
        float z { set; get; }
        bool IsClientObject { get; }
        bool IsRemoteObject { get; }
        bool IsMasterClientObject { get; }
    }
}
