/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Units
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class GroupingManager
    {
        private static readonly IDictionary<Type, object> _groupingStrategies = new Dictionary<Type, object>();

        public static void RegisterGroupingStrategy<T>(IGroupingStrategy<T> strat) where T : IGroupable<T>
        {
            _groupingStrategies[typeof(T)] = strat;
        }

        public static IGroupingStrategy<T> GetGroupingStrategy<T>() where T : IGroupable<T>
        {
            object tmp;
            if (_groupingStrategies.TryGetValue(typeof(T), out tmp))
            {
                return (IGroupingStrategy<T>)tmp;
            }

            return null;
        }

        public static IGrouping<T> CreateGrouping<T>(params T[] members) where T : IGroupable<T>
        {
            return CreateGrouping((IEnumerable<T>)members);
        }

        public static IGrouping<T> CreateGrouping<T>(IEnumerable<T> members) where T : IGroupable<T>
        {
            var strat = GetGroupingStrategy<T>();
            if (strat == null)
            {
                return null;
            }

            return strat.CreateGrouping(members);
        }

        public static TransientGroup<T> CreateGroup<T>(int capacity) where T : IGroupable<T>
        {
            var strat = GetGroupingStrategy<T>();
            if (strat == null)
            {
                return null;
            }

            return strat.CreateGroup(capacity);
        }

        public static TransientGroup<T> CreateGroup<T>(IEnumerable<T> members) where T : IGroupable<T>
        {
            var strat = GetGroupingStrategy<T>();
            if (strat == null)
            {
                return null;
            }

            //Enumerate it once
            var memberList = members.ToList();
            var grp = strat.CreateGroup(memberList.Count);
            foreach (var m in memberList)
            {
                grp.Add(m);
            }

            return grp;
        }
    }
}
