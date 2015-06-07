namespace Apex.Steering.Messages
{
    using Apex.DataStructures;
    using Apex.Units;
    using Apex.Utilities;

    public class UnitsSelectedMessage
    {
        public UnitsSelectedMessage(IGrouping<IUnitFacade> units)
        {
            Ensure.ArgumentNotNull(units, "units");
            this.selectedUnits = units;
        }

        public IGrouping<IUnitFacade> selectedUnits
        {
            get;
            private set;
        }
    }
}
