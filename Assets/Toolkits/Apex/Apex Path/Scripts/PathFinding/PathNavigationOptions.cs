/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.PathFinding
{
    public class PathNavigationOptions : IPathNavigationOptions
    {
        public PathNavigationOptions()
        {
            this.nextNodeDistance = 1f;
            this.requestNextWaypointDistance = 2f;
            this.replanMode = ReplanMode.Dynamic;
            this.replanInterval = 0.5f;
        }

        public float nextNodeDistance
        {
            get;
            set;
        }

        public float requestNextWaypointDistance
        {
            get;
            set;
        }

        public bool announceAllNodes
        {
            get;
            set;
        }

        public ReplanMode replanMode
        {
            get;
            set;
        }

        public float replanInterval
        {
            get;
            set;
        }
    }
}
