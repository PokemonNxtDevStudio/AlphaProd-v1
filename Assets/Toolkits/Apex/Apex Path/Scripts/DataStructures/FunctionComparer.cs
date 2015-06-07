/* Copyright © 2014 Apex Software. All rights reserved. */

namespace Apex.DataStructures
{
    using System;
    using System.Collections.Generic;
    using Apex.Utilities;

    public class FunctionComparer<T> : IComparer<T>
    {
        private Comparison<T> _comparer;

        public FunctionComparer(Comparison<T> comparer)
        {
            Ensure.ArgumentNotNull(comparer, "comparer");

            _comparer = comparer;
        }

        public int Compare(T x, T y)
        {
            return _comparer(x, y);
        }
    }
}