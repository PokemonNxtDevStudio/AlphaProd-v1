/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Units
{
    public interface IGroupable<T> where T : IGroupable<T>
    {
        TransientGroup<T> transientGroup { get; set; }

        int formationIndex { get; set; }
    }
}
