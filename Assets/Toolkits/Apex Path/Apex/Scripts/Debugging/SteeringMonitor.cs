﻿/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Debugging
{
    using Apex;
    using Apex.Messages;
    using Apex.Services;
    using UnityEngine;

    /// <summary>
    /// A simple navigation monitor, that will log the various navigation events a moving unit may report to the Unity console.
    /// </summary>
    [AddComponentMenu("Apex/Debugging/Steering Monitor")]
    public class SteeringMonitor : ExtendedMonoBehaviour, IHandleMessage<UnitNavigationEventMessage>
    {
        /// <summary>
        /// The unit filter
        /// </summary>
        public GameObject unitFilter;

        /// <summary>
        /// The event filter
        /// </summary>
        public UnitNavigationEventMessage.Event eventFilter;

        /// <summary>
        /// Called on Start and OnEnable, but only one of the two, i.e. at startup it is only called once.
        /// </summary>
        protected override void OnStartAndEnable()
        {
            GameServices.messageBus.Subscribe(this);
        }

        private void OnDisable()
        {
            GameServices.messageBus.Unsubscribe(this);
        }

        void IHandleMessage<UnitNavigationEventMessage>.Handle(UnitNavigationEventMessage message)
        {
            if (unitFilter != null && !unitFilter.Equals(message.entity))
            {
                return;
            }

            if (eventFilter != UnitNavigationEventMessage.Event.None && message.eventCode != eventFilter)
            {
                return;
            }

            Debug.Log(string.Format("Unit '{0}' ({1}) reports: {2} at position: {3}.", message.entity.name, message.entity.transform.position, message.eventCode, message.destination));
        }
    }
}
