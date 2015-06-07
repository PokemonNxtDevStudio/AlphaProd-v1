namespace Apex.DataStructures
{
    using System.Collections.Generic;

    public class PlaneVectorComparer : IEqualityComparer<PlaneVector>
    {
        public bool Equals(PlaneVector x, PlaneVector y)
        {
            return x == y;
        }

        public int GetHashCode(PlaneVector obj)
        {
            return obj.GetHashCode();
        }
    }
}