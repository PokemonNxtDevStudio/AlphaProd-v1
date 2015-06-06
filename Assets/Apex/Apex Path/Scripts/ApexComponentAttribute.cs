/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex
{
    using System;

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ApexComponentAttribute : Attribute
    {
        public ApexComponentAttribute(string category)
        {
            this.category = category;
        }

        public string category
        {
            get;
            private set;
        }
    }
}
