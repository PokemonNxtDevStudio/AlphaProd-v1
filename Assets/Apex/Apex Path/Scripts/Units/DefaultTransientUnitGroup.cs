/* Copyright © 2014 Apex Software. All rights reserved. */

namespace Apex.Units
{
    using System.Collections.Generic;
    using Apex.DataStructures;
    using Apex.PathFinding;
    using UnityEngine;

    public class DefaultTransientUnitGroup : TransientGroup<IUnitFacade>, IGrouping<IUnitFacade>
    {
        public DefaultTransientUnitGroup(int capacity)
            : base(capacity)
        {
        }

        public DefaultTransientUnitGroup(IUnitFacade[] members)
            : base(members)
        {
        }

        public DefaultTransientUnitGroup(IEnumerable<IUnitFacade> members)
            : base(members)
        {
        }

        public override Vector3 velocity
        {
            get
            {
                var mu = this.modelUnit;
                return mu != null ? mu.velocity : Vector3.zero;
            }
        }

        public override Vector3 actualVelocity
        {
            get
            {
                var mu = this.modelUnit;
                return mu != null ? mu.actualVelocity : Vector3.zero;
            }
        }

        public override Path currentPath
        {
            get
            {
                var mu = this.modelUnit;
                return mu != null ? mu.currentPath : null;
            }
        }

        public override IIterable<Vector3> currentWaypoints
        {
            get
            {
                var mu = this.modelUnit;
                return mu != null ? mu.currentWaypoints : null;
            }
        }

        public override Vector3? finalDestination
        {
            get
            {
                var mu = this.modelUnit;
                return mu != null ? mu.finalDestination : null;
            }
        }

        public override Vector3? nextNodePosition
        {
            get
            {
                var mu = this.modelUnit;
                return mu != null ? mu.nextNodePosition : null;
            }
        }

        public override bool isGrounded
        {
            get { return modelUnit.isGrounded; }
        }

        int IGrouping<IUnitFacade>.groupCount
        {
            get { return 1; }
        }

        int IGrouping<IUnitFacade>.memberCount
        {
            get { return this.count; }
        }

        TransientGroup<IUnitFacade> IGrouping<IUnitFacade>.this[int index]
        {
            get { return this; }
        }

        public override void Wait(float? seconds)
        {
            for (int i = 0; i < this.count; i++)
            {
                this[i].Wait(seconds);
            }
        }

        public override void Resume()
        {
            for (int i = 0; i < this.count; i++)
            {
                this[i].Resume();
            }
        }

        public override void Stop()
        {
            for (int i = 0; i < this.count; i++)
            {
                this[i].Stop();
            }
        }

        public override void EnableMovementOrders()
        {
            for (int i = 0; i < this.count; i++)
            {
                this[i].EnableMovementOrders();
            }
        }

        public override void DisableMovementOrders()
        {
            for (int i = 0; i < this.count; i++)
            {
                this[i].DisableMovementOrders();
            }
        }

        public override void SetPreferredSpeed(float speed)
        {
            var gcount = this.count;
            for (int i = 0; i < gcount; i++)
            {
                this[i].SetPreferredSpeed(speed);
            }

            var mu = this.modelUnit;
            if (mu != null)
            {
                mu.SetPreferredSpeed(speed);
            }
        }

        void IGrouping<IUnitFacade>.Remove(IUnitFacade member)
        {
            this.Remove(member);
        }

        void IGrouping<IUnitFacade>.Add(IUnitFacade member)
        {
            this.Add(member);
        }

        protected override void MoveToInternal(Vector3 position, bool append)
        {
            for (int i = 0; i < this.count; i++)
            {
                this[i].MoveTo(position, append);
            }
        }

        protected override void MoveAlongInternal(Path path, ReplanCallback onReplan)
        {
            for (int i = 0; i < this.count; i++)
            {
                var clone = path.Clone();
                this[i].MoveAlong(clone, onReplan);
            }
        }
    }
}
