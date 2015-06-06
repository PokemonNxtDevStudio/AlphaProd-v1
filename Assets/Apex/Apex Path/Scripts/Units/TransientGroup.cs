namespace Apex.Units
{
    using System;
    using System.Collections.Generic;
    using Apex.DataStructures;
    using Apex.PathFinding;
    using Apex.Steering;
    using Apex.Utilities;
    using Apex.WorldGeometry;
    using UnityEngine;

    public abstract class TransientGroup<T> : IMovable, IMovingObject where T : IGroupable<T>
    {
        private IndexableSet<T> _members;

        public TransientGroup(int capacity)
        {
            _members = new IndexableSet<T>(capacity);
        }

        public TransientGroup(T[] members)
        {
            Ensure.ArgumentNotNull(members, "members");

            _members = new IndexableSet<T>(members);
        }

        public TransientGroup(IEnumerable<T> members)
        {
            Ensure.ArgumentNotNull(members, "members");

            _members = new IndexableSet<T>(members);
        }

        public virtual Vector3 centerOfGravity
        {
            get { return GetGroupCenterOfGravity(); }
        }

        public virtual T modelUnit
        {
            get { return _members.count > 0 ? _members[0] : default(T); }
        }

        public int count
        {
            get { return _members.count; }
        }

        public bool hasArrived
        {
            get;
            set;
        }

        public virtual Path currentPath
        {
            get { throw new NotSupportedException(); }
        }

        public virtual IIterable<Vector3> currentWaypoints
        {
            get { throw new NotSupportedException(); }
        }

        public virtual Vector3? finalDestination
        {
            get { throw new NotSupportedException(); }
        }

        public virtual Vector3? nextNodePosition
        {
            get { throw new NotSupportedException(); }
        }

        public virtual bool isGrounded
        {
            get { throw new NotSupportedException(); }
        }

        public virtual Vector3 velocity
        {
            get { throw new NotSupportedException(); }
        }

        public virtual Vector3 actualVelocity
        {
            get { throw new NotSupportedException(); }
        }

        public T this[int index]
        {
            get { return _members[index]; }
        }

        public virtual void Add(T member)
        {
            _members.Add(member);
        }

        public virtual void Remove(T member)
        {
            _members.Remove(member);
            if (object.ReferenceEquals(member.transientGroup, this))
            {
                member.transientGroup = null;
            }
        }

        public virtual void Sort()
        {
            _members.Sort();
        }

        public virtual void Sort(FunctionComparer<T> comparer)
        {
            _members.Sort(comparer);
        }

        public virtual void Sort(IComparer<T> comparer)
        {
            _members.Sort(comparer);
        }

        public virtual void Sort(int index, int length)
        {
            _members.Sort(index, length);
        }

        public virtual void Sort(int index, int length, FunctionComparer<T> comparer)
        {
            _members.Sort(index, length, comparer);
        }

        public virtual void Dissolve()
        {
            foreach (var member in _members)
            {
                if (object.ReferenceEquals(member.transientGroup, this))
                {
                    member.transientGroup = null;
                }
            }

            _members.Clear();
        }

        public virtual Vector3 GetGroupCenterOfGravity()
        {
            Vector3 cog = Vector3.zero;

            int membersCount = _members.count;
            if (membersCount == 0)
            {
                return cog;
            }

            for (int i = 0; i < membersCount; i++)
            {
                var member = _members[i] as IPositioned;
                if (member != null)
                {
                    cog += member.position;
                }
            }

            return cog / membersCount;
        }

        public void MoveTo(Vector3 position, bool append)
        {
            PrepareForAction();
            MoveToInternal(position, append);
        }

        public virtual void MoveAlong(Path path)
        {
            MoveAlong(path, null);
        }

        public void MoveAlong(Path path, ReplanCallback onReplan)
        {
            PrepareForAction();
            MoveAlongInternal(path, onReplan);
        }

        public virtual void SetPreferredSpeed(float speed)
        {
            /* Default NOOP */
        }

        public abstract void Wait(float? seconds);

        public abstract void Resume();

        public abstract void Stop();

        public abstract void EnableMovementOrders();

        public abstract void DisableMovementOrders();

        protected abstract void MoveToInternal(Vector3 position, bool append);

        protected abstract void MoveAlongInternal(Path path, ReplanCallback onReplan);

        protected virtual void PrepareForAction()
        {
            for (int i = 0; i < _members.count; i++)
            {
                var member = _members[i];
                var group = member.transientGroup;
                if (group == null || !object.ReferenceEquals(group, this))
                {
                    if (group != null)
                    {
                        group.Remove(member);
                    }

                    member.transientGroup = this;
                }
            }
        }
    }
}