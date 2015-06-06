/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Units
{
    using System;
    using System.Collections.Generic;
    using Apex.DataStructures;
    using Apex.Utilities;

    public class Grouping<T> : IGrouping<T> where T : IGroupable<T>
    {
        private DynamicArray<TransientGroup<T>> _members;

        public Grouping(int capacity)
        {
            _members = new DynamicArray<TransientGroup<T>>(capacity);
        }

        public Grouping(IEnumerable<TransientGroup<T>> members)
        {
            Ensure.ArgumentNotNull(members, "members");

            _members = new DynamicArray<TransientGroup<T>>(members);
        }

        public int groupCount
        {
            get { return _members.count; }
        }

        public int memberCount
        {
            get
            {
                int count = 0;
                var grpCount = _members.count;
                for (int i = 0; i < grpCount; i++)
                {
                    count += _members[i].count;
                }

                return count;
            }
        }

        public TransientGroup<T> this[int index]
        {
            get { return _members[index]; }
        }

        public virtual void Add(TransientGroup<T> group)
        {
            _members.Add(group);
        }

        public virtual void Remove(TransientGroup<T> group)
        {
            _members.Remove(group);
        }

        public void Add(T member)
        {
            var strat = GroupingManager.GetGroupingStrategy<T>();
            if (strat == null)
            {
                throw new InvalidOperationException("No strategy exists for this type of member.");
            }

            var grpCount = _members.count;
            for (int i = 0; i < grpCount; i++)
            {
                var grp = _members[i];
                if (grp.count == 0)
                {
                    continue;
                }

                if (strat.BelongsToSameGroup(grp[0], member))
                {
                    grp.Add(member);
                    return;
                }
            }
        }

        public void Remove(T member)
        {
            var strat = GroupingManager.GetGroupingStrategy<T>();
            if (strat == null)
            {
                throw new InvalidOperationException("No strategy exists for this type of member.");
            }

            var grpCount = _members.count;
            for (int i = 0; i < grpCount; i++)
            {
                var grp = _members[i];
                if (grp.count == 0)
                {
                    continue;
                }

                if (strat.BelongsToSameGroup(grp[0], member))
                {
                    grp.Remove(member);
                    return;
                }
            }
        }
    }
}